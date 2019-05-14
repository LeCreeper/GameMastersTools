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
    class ChapterPersistency
    {
        #region consts og statics

        const string serverUrl = "https://gamemasterstoolsweb2.azurewebsites.net";
        private const string GetAndPostApi = "api/Chapters";
        private const string DeleteAndPutApi = "api/Chapters/";

        #endregion

        #region Load Methods

        /// <summary>
        /// This returns a list of specified objects from the database and adds them to a list. 
        /// </summary>
        /// <returns></returns>

        public static async Task<List<Chapter>> LoadChapters()
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
                        var chapters = response.Content.ReadAsAsync<IEnumerable<Chapter>>().Result;

                        List<Chapter> selectedCampaignChapters = new List<Chapter>();

                        foreach (var chapter in chapters)
                        {
                            if (chapter.CampaignId == CampaignVM.SelectedCampaignId)
                            {
                                selectedCampaignChapters.Add(chapter);
                            }
                        }

                        return selectedCampaignChapters.ToList();

                    }
                    else
                    {
                        await new MessageDialog("Could not load chapters").ShowAsync();
                        return new List<Chapter>();
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
        /// <param name="chapterId"></param>
        /// <returns></returns>
        public static Chapter GetSingleChapter(int chapterId)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = client.GetAsync(DeleteAndPutApi + chapterId).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var chapter = response.Content.ReadAsAsync<Chapter>().Result;
                        return chapter;

                    }
                    return null;
                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message);
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
        /// <param name="chapter"></param>
        public async static void PostChapter(Chapter chapter)
        {
            HttpClientHandler handler2 = new HttpClientHandler();
            handler2.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler2))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    await client.PostAsJsonAsync(GetAndPostApi, chapter);

                }
                catch (Exception e)
                {
                    await new MessageDialog(e.Message).ShowAsync();


                }
            }
        }

        #endregion

        #region Update Method

        /// <summary>
        /// This edits a specified object in the database.
        /// </summary>
        /// <param name="chapter"></param>
        public static async void PutChapters(Chapter chapter)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    await client.PutAsJsonAsync(DeleteAndPutApi + chapter.ChapterId, chapter);

                }
                catch (Exception e)
                {
                    await new MessageDialog(e.Message).ShowAsync();



                }
            }
        }

        #endregion

        #region DeleteMethod

        /// <summary>
        /// This removes a specified object from the database, but leaves the ID taken.
        /// </summary>
        /// <param name="chapterId"></param>
        public static async void DeleteChapter(Chapter chapter)
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
                    var response = await client.DeleteAsync(DeleteAndPutApi + chapter.ChapterId);
                    if (!response.IsSuccessStatusCode)
                    {
                        new MessageDialog(response.ToString()).ShowAsync();
                    }
                }
                catch (Exception e)
                {
                    await new MessageDialog(e.Message).ShowAsync();

                }
            }
        }

        #endregion
    }
}
