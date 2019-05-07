using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using GameMastersTools.Model;

namespace GameMastersTools.Persistency
{
    class CampaignDBPersistency
    {
        const string serverUrl = "https://gmtoolsweb.azurewebsites.net/";
        static HttpClientHandler handler = new HttpClientHandler();


        #region Load Methods

        /// <summary>
        /// This returns a list of specified objects from the database and adds them to a list. 
        /// </summary>
        /// <returns></returns>

        //TODO Make Load-function only load campaigns for a logged in user
        public static async Task<List<Campaign>> LoadCampaigns()
        {

            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = client.GetAsync("api/CampaignTables/").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var campaigns = response.Content.ReadAsAsync<IEnumerable<Campaign>>().Result;

                        return campaigns.ToList();


                    }
                    return null;
                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();
                    //TODO remember to catch
                    throw;
                }
            }
        }

        /// <summary>
        /// This returns a single specified object from the database.
        /// </summary>
        /// <param name="campaingId"></param>
        /// <returns></returns>
        public static Campaign GetSingleCampaign(int campaingId)
        {
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = client.GetAsync("api/CampaignTables/" + campaingId).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var campaign = response.Content.ReadAsAsync<Campaign>().Result;
                        return campaign;

                    }
                    return null;
                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();
                    //TODO remember to catch
                    throw;

                }
            }
        }

        #endregion

        /// <summary>
        /// This adds a specified object to the database
        /// </summary>
        /// <param name="campaign"></param>
        public async static void PostCampaigns(Campaign campaign)
        {
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    await client.PostAsJsonAsync("api/CampaignTables", campaign);

                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();


                }
            }
        }

        /// <summary>
        /// This edits a specified object in the database.
        /// </summary>
        /// <param name="campaign"></param>
        /// <param name="campaignId"></param>
        public static async void PutCampaigns(Campaign campaign)
        {


            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    await client.PutAsJsonAsync("api/CampaignTables/" + campaign.CampaignId, campaign);

                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();



                }
            }
        }

        /// <summary>
        /// This removes a specified object from the database, but leaves the ID taken.
        /// </summary>
        /// <param name="CampaignId"></param>
        public static async void DeleteCampaign(Campaign campaign)
        {
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    await client.DeleteAsync("api/CampaignTables/" + campaign.CampaignId);

                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();

                }
            }
        }



        private class MessageDialogHelper
        {
            public static async void Show(string content, string title)
            {
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
            }
        }
    }
}
