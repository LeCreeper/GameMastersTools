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
    class GenericDbPersistency<T>
    {
        private const string serverUrl = "https://gamemasterstoolsweb3.azurewebsites.net";

        public static async Task<List<T>> GetObj(string api)
        {
            
            HttpClientHandler handler = new HttpClientHandler();

            handler.UseDefaultCredentials = true;

            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = client.GetAsync(api).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var obj = await response.Content.ReadAsAsync<IEnumerable<T>>();
                        return obj.ToList();

                    }

                    return null;
                }
                catch (Exception e)
                {
                    MessageDialogHelper.Show("ERROR, ERROR, ERORr", "Roor\n\n" + e.Message);
                    throw;
                }
            }
        }


        public static async void PostObj(T obj, string api)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = await client.PostAsJsonAsync(api, obj);
                }
                catch (Exception e)
                {
                    MessageDialogHelper.Show("Hej med", "dig!" + e.Message);
                    
                }
            }
        }


        public static async void DeleteObj(string api, int id )
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = await client.DeleteAsync(api + id);


                }
                catch (Exception e)
                {
                    MessageDialogHelper.Show("Hej med", "dig!" + e.Message);
                  
                }
            }
        }

        public static async void UpdateObj(T obj, string api)
        {
            HttpClientHandler handler = new HttpClientHandler();

            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = await client.PutAsJsonAsync(api, obj);
                }
                catch (Exception e)
                {
                    MessageDialogHelper.Show("Hej med", "dig!" + e.Message);
                    throw;
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
