using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using GameMastersTools.Model;
using GameMastersTools.ViewModel;

namespace GameMastersTools.Persistency
{
    class CampaignDBPersistency
    {
        #region consts og statics

        const string serverUrl = "https://gamemasterstoolsweb2.azurewebsites.net";
        private const string GetAndPostApi = "api/Campaigns";
        private const string DeleteAndPutApi = "api/Campaigns/";

        #endregion

        #region Load Methods

        /// <summary>
        /// This returns a list of specified objects from the database and adds them to a list. 
        /// </summary>
        /// <returns></returns>

        //TODO Make Load-function only load campaigns for a logged in user
        public static async Task<List<Campaign>> LoadCampaigns()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = client.GetAsync(GetAndPostApi).Result;

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
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = client.GetAsync(DeleteAndPutApi + campaingId).Result;

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

        #region Create Method

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
                    await client.PostAsJsonAsync(GetAndPostApi, campaign);

                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();


                }
            }
        }

        #endregion

        #region Update Method

        /// <summary>
        /// This edits a specified object in the database.
        /// </summary>
        /// <param name="campaign"></param>
        /// <param name="campaignId"></param>
        public static async void PutCampaigns(Campaign campaign)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    await client.PutAsJsonAsync(DeleteAndPutApi + campaign.CampaignId, campaign);

                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();



                }
            }
        }

        #endregion

        #region DeleteMethod

        /// <summary>
        /// This removes a specified object from the database, but leaves the ID taken.
        /// </summary>
        /// <param name="CampaignId"></param>
        public static async void DeleteCampaign(Campaign campaign)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                try
                {
                    var response = await client.DeleteAsync(DeleteAndPutApi + campaign.CampaignId);
                    if (!response.IsSuccessStatusCode)
                    {
                        new MessageDialog(response.ToString()).ShowAsync();
                    }
                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();

                }
            }
        }

        #endregion

        #region MessageDiaglogHelper

        private class MessageDialogHelper
        {
            public static async void Show(string content, string title)
            {
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
            }
        }

        #endregion
    }
}
