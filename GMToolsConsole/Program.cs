using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GMToolsWeb.Models;

namespace GMToolsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //PostUsers(new UserTable(){UserName = "Hans", UserPassword = "12345678"});
            //PutUsers(new UserTable() {UserId = 3, UserName = "Ibsen", UserPassword = "12345678"}, 3); 
            //DeleteUsers(3);
            //GetSingleUser(2);
            GetUsers();


            Console.ReadKey();

        }

        public static void GetUsers()
        {
            const string serverUrl = "https://gmtoolsweb.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();

            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {



                    var response = client.GetAsync("api/usertables").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var users = response.Content.ReadAsAsync<IEnumerable<UserTable>>().Result;

                        foreach (var user in users)
                        {
                            Console.WriteLine(user);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
            }
        }

        public static void GetSingleUser(int userId)
        {
            const string serverUrl = "https://gmtoolsweb.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();

            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {



                    var response = client.GetAsync("api/usertables/" + userId).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var user = response.Content.ReadAsAsync<UserTable>().Result;
                        Console.WriteLine(user);
            
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
            }
        }

        public async static void PostUsers(UserTable user)
        {
            const string serverUrl = "https://gmtoolsweb.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();

            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response =  await client.PostAsJsonAsync("api/usertables",user);
                    var returnedUser = response.Content.ReadAsAsync<UserTable>().Result;

                    Console.WriteLine(returnedUser);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
            }
        }

        public static void PutUsers(UserTable user, int userId)
        {
            const string serverUrl = "https://gmtoolsweb.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();

            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response =  client.PutAsJsonAsync("api/usertables/" + user.UserId, user).Result;

                   

                    Console.WriteLine(response);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
            }
        }

        public static void DeleteUsers(int userId)
        {
            const string serverUrl = "https://gmtoolsweb.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();

            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);

                try
                {
                    var response = client.DeleteAsync("api/usertables/" + userId);
                    Console.WriteLine(response.Result);


                    Console.WriteLine(response);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
            }
        }


    }
}
