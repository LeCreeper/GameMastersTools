using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameMastersTools.Model;
using GameMastersTools.View;

namespace GameMastersTools.Persistency
{
    class DatabasePersistency
    {

        const string serverUrl = "https://gamemasterstoolsweb2.azurewebsites.net/";



        /// <summary>
        /// This returns a list of specified objects from the database and adds them to a list. 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<User>> LoadUsers()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {

                    var response = client.GetAsync("api/Users").Result;


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
            HttpClientHandler handler = new HttpClientHandler();

            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {

                    var response = client.GetAsync("api/Users/" + userId).Result;


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
            HttpClientHandler handler = new HttpClientHandler();

            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {

                    await client.PostAsJsonAsync("api/Users", user);

                    
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
            HttpClientHandler handler = new HttpClientHandler();


            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    //TODO check if UserId works

                     await client.PutAsJsonAsync("api/Users/" + user.UserId, user);

                    
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
            HttpClientHandler handler = new HttpClientHandler();

            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {

                    await client.DeleteAsync("api/Users/" + user.UserId);

                   
                }
                catch (Exception e)
                {
                    new MessageDialog(e.Message).ShowAsync();

                }
            }
        }

        /// <summary>
        /// Checks if username already exists, if it does, returns null. Otherwise, go and create the user in the database and navigate to login page
        /// </summary>
        /// <param name="user"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<List<User>> CheckThenPost(User user, string name)
        {

            HttpClientHandler handler = new HttpClientHandler();

            using (HttpClient client = new HttpClient(handler))
            {
                handler.UseDefaultCredentials = true;
                client.BaseAddress = new Uri(serverUrl);

                try
                {

                    var response = await client.GetAsync("api/Users");


                    if (response.IsSuccessStatusCode)
                    {
                        // Get all users
                        var users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;

                        // Does users contain anything?
                        if (users != null)
                        {
                            // Then check if name exists already
                            foreach (var u in users)
                            {
                                if (name == u.UserName)
                                {
                                    // If name already exist, inform the user then return null 
                                    //usm.UserErrorMessage = $"Name {name} is already in use. ";
                                    await new MessageDialog($"Name {name} is already in use.").ShowAsync();
                                    return null;
                                }
                            }

                            var checkSuccesful = await client.PostAsJsonAsync("api/Users", user);


                            if (checkSuccesful.IsSuccessStatusCode)
                            {
                                // Navigating to LoginPage if successful
                                if (Window.Current.Content is Frame frame) frame.Navigate(typeof(LoginPage));

                                // Or like this:
                                //Frame newFrame = Window.Current.Content as Frame;
                                //if (newFrame != null) newFrame.Navigate(typeof(LoginPage));


                                // Showing a message dialog if system is successful in adding the user to the database
                                await new MessageDialog("Added " + user.UserName + " to the database. You can now log in.").ShowAsync();
                                
                            }
                        }
                        return users.ToList();
                    }
                    return null;
                }
                catch (Exception e)
                {
                    await new MessageDialog(e.Message).ShowAsync();
                    //TODO remember to catch
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
