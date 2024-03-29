﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameMastersTools.Model;
using GameMastersTools.Persistency;
using GameMastersTools.View;
using GameMastersTools.ViewModel;

namespace GameMastersTools.Handler
{
    public class PcHandler
    {
        private Timer timer;

        public PcViewModel PcViewModel { get; set; }
        public bool IsSuccesful = false;
        

        // Making sure the PcHandler is instantiated with the correct viewModel
        public PcHandler(PcViewModel pcViewModel)
        {
            PcViewModel = pcViewModel;
        }

        public async void CreatePc()
        {
            try
            {
                // Checks if playername exists, if yes throw an exception
                PlayerNameExists();
                if (IsSuccesful)
                {
                    PcViewModel.PcSingleton.PostPc(
                        PcViewModel.PcName,
                        PcViewModel.PcDescription);

                    // Making sure we update the sorted list whenever
                    // an object is added or deleted from the main observable collection

                    PC pc = new PC(PcViewModel.PcName, PcViewModel.PcDescription, UserViewModel.LoggedInUserId);
                    PcViewModel.UserPcs.Add(pc);
                }
                    
                

            }
            catch (Exception e)
            {
                // Catching the exception and displays the message to the user
                await new MessageDialog(e.Message).ShowAsync();
            }
        }

        public void SetSelectedPc(PC selectedPc)
        {
            PcViewModel.SelectedPc = selectedPc;
        }

        public async void DeletePc()
        {
            if (PcViewModel.SelectedPc != null)
            {
                PcViewModel.PcSingleton.DeletePc(PcViewModel.SelectedPc);


                // Making sure we update the sorted list whenever an object is added or deleted to the main observable collection
                PcViewModel.UserPcs.Remove(PcViewModel.SelectedPc);

                //var messageDialog = new MessageDialog("Er Du sikker på du vil slette den her spiller: " + PcViewModel.SelectedPc.PcName + "?");

                //// If yes, run the UICommand: CommandInvokedHandler 
                //messageDialog.Commands.Add(new UICommand("Ret sikker", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                //messageDialog.Commands.Add(new UICommand("Please god no", null));

                //// Set the command that will be invoked by default
                //messageDialog.DefaultCommandIndex = 0;

                //// Set the command to be invoked when escape is pressed
                //messageDialog.CancelCommandIndex = 1;

                //// Show the message dialog
                //await messageDialog.ShowAsync();
            }
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            PcViewModel.PcSingleton.DeletePc(PcViewModel.SelectedPc);


            // Making sure we update the sorted list whenever an object is added or deleted to the main observable collection
            PcViewModel.UserPcs.Remove(PcViewModel.SelectedPc);
            
            // Temp solution | navigates to frontpage
            //
            //Frame frame = Window.Current.Content as Frame;
            //frame?.Navigate(typeof(MainPage));

            //if (frame != null && frame.CanGoBack)
            //{
            //    frame.GoBack();
            //}

        }

        public void UpdatePc()
        {
            PcViewModel.PcSingleton.UpdatePc(PcViewModel.SelectedPc);
        }

        public void PlayerNameExists()
        {
            //try
            //{
                PcViewModel.ErrorMessage = "";
;                if (PcViewModel.PcName == null)
                {
                    IsSuccesful = false;
                    PcViewModel.ErrorMessage = "You need a name";
                    return;
                    //  throw new Exception("You need a name");
                }

                if (PcViewModel.PcDescription == null)
                {
                    IsSuccesful = false;
                    PcViewModel.ErrorMessage = "Needs a description";
                    return;
                    //throw new Exception("Needs a description");
                }

                foreach (var player in PcViewModel.UserPcs)
                {
                    if (PcViewModel.PcName == player.PcName)
                    {
                        IsSuccesful = false;
                        PcViewModel.ErrorMessage = "Player name already exists";
                        return;
                        //throw new Exception("Player name already exists");
                    }
                }

            IsSuccesful = true;
            //}

            //catch (Exception e)
            //{
            //    await new MessageDialog(e.Message).ShowAsync();
            //}

            
        }

        



        public void Timer()
        {




            timer = new Timer(SaveObj, null, 3000, 5000);



            ResetTimer();

            //PcViewModel.ErrorMessage = "";

            
            //await Task.Delay(3000);
           

          
            //    PcViewModel.ErrorMessage = "Changes saved";
            //    await Task.Delay(1000);
            //    PcViewModel.ErrorMessage = "";
                
            
            
            
            

        }

 

        public void ResetTimer()
        {
            
            timer.Change(TimeSpan.FromMilliseconds(3000), TimeSpan.FromMilliseconds(5000));
        }

        public async void SaveObj(object state)
        {
            

             await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    async () =>
                {
                    PcViewModel.ErrorMessage = "Saving...";

                    await Task.Delay(1000);

                    PcViewModel.ErrorMessage = "Changes has been saved";

           


                    // await Task.Delay(1000);

                    // PcViewModel.ErrorMessage = "_";


                }
            );
     
        }

    }
}
