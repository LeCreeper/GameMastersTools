using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Newtonsoft.Json;

namespace NoteMVVM
{
    //Json.Net er downloaded til projektet via NuGet: Højreklik på projektet -> Manage NuGet Package

    class PersistencyService
    {
        private static string JsonFileName = "Notes.json";

        public static async void SaveNotesAsJsonAsync(ObservableCollection<string> notes)
        {
            string notesJsonString = JsonConvert.SerializeObject(notes);
            SerializeNotesFileAsync(notesJsonString, JsonFileName);
        }

        public static async Task<List<string>> LoadNotesFromJsonAsync()
        {
            string notesJsonString = await DeserializeNotesFileAsync(JsonFileName);
            if (notesJsonString != null)
                return (List<string>) JsonConvert.DeserializeObject(notesJsonString, typeof (List<string>));
            return null;
        }

       

        private static async void SerializeNotesFileAsync(string notesJsonString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, notesJsonString);
        }

        
        private static async Task<string> DeserializeNotesFileAsync(string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException ex)
            {
                MessageDialogHelper.Show("Loading for the first time? - Try Add and Save some Notes before trying to Save for the first time", "File not Found");
                return null;
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