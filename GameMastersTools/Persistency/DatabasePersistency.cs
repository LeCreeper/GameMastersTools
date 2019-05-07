﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameMastersTools.Model;
using GameMastersTools.View;

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

        /// <summary>
        /// Checks if username already exists, if it does, returns null. Otherwise, go and create the user in the database and navigate to login page
        /// </summary>
        /// <param name="user"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<List<User>> CheckThenPost(User user, string name)
        {

            handler.UseDefaultCredentials = true;

            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = await client.GetAsync("api/usertables");

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
                            var checkSuccesful = await client.PostAsJsonAsync("api/usertables", user);

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
                    new MessageDialog(e.Message).ShowAsync();
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