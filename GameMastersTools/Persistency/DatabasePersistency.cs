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
    class DatabasePersistency
    {
        const string serverUrl = "https://gmtoolsweb.azurewebsites.net/";
        static HttpClientHandler handler = new HttpClientHandler();


        /// <summary>
        /// This returns a list of specified objects from the database and adds them to a list. 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<User>> LoadUsers()
        {

            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = client.GetAsync("api/usertables").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;

                        return users.ToList();

                       
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
        /// <param name="userId"></param>
        /// <returns></returns>
        public static User GetSingleUser(int userId)
        {
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = client.GetAsync("api/usertables/" + userId).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var user = response.Content.ReadAsAsync<User>().Result;
                        return user;

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
        /// This adds a specified object to the database
        /// </summary>
        /// <param name="user"></param>
        public async static void PostUsers(User user)
        {
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    await client.PostAsJsonAsync("api/usertables", user);
                    
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
        /// <param name="user"></param>
        /// <param name="userId"></param>
        public static async void PutUsers(User user)
        {
            

            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    //TODO check if UserId works
                     await client.PutAsJsonAsync("api/usertables/" + user.UserId, user);
                    
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
        /// <param name="userId"></param>
        public static async void DeleteUsers(User user)
        {
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    await client.DeleteAsync("api/usertables/" + user.UserId);
                   
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
