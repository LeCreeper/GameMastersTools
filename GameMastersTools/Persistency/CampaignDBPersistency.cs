using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using GameMastersTools.Model;
using GameMastersTools.ViewModel;

namespace GameMastersTools.Persistency
{
    class CampaignDBPersistency
    {
        const string serverUrl = "https://gamemasterstoolsweb.azurewebsites.net";
        static HttpClientHandler handler = new HttpClientHandler();
        private const string api = "api/Campaigns";


        #region Load Methods

        /// <summary>
        /// This returns a list of specified objects from the database and adds them to a list. 
        /// </summary>
        /// <returns></returns>

        //TODO Make Load-function only load campaigns for a logged in user
        public static async Task<List<Campaign>> LoadCampaigns()
        {
            HttpClientHandler handler2 = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler2))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = client.GetAsync(api).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var campaigns = response.Content.ReadAsAsync<IEnumerable<Campaign>>().Result;

                        List<Campaign> usersCampaigns = new List<Campaign>();

                        foreach (var campaign in campaigns)
                        {
                            if (campaign.UserId == UserViewModel.LoggedInUserId)
                            {
                                usersCampaigns.Add(campaign);
                            }
                        }

                        return usersCampaigns.ToList();

                    }
                    else
                    {
                        new MessageDialog("Could not load campaigns").ShowAsync();
                        return new List<Campaign>();
                    }

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
                    var response = client.GetAsync(api + campaingId).Result;

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
            HttpClientHandler handler2 = new HttpClientHandler();
            handler2.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler2))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    await client.PostAsJsonAsync(api, campaign);

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
                    await client.PutAsJsonAsync(api + campaign.CampaignId, campaign);

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
            HttpClientHandler handler2 = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler2))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    await client.DeleteAsync(api + campaign.CampaignId);

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
