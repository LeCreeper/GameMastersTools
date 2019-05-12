using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using GameMastersTools.Model;
using GameMastersTools.ViewModel;

namespace GameMastersTools.Handler
{
    class PcHandler
    {
        public PcViewModel PcViewModel { get; set; }

        // Making sure the PcHandler is instantiated with the correct viewModel
        public PcHandler(PcViewModel pcViewModel)
        {
            PcViewModel = pcViewModel;
        }

        public void CreatePc()
        {
            PcViewModel.PcSingleton.PostPc(
                PcViewModel.PcName,
                PcViewModel.PcDescription);
            
            // Making sure we update the sorted list whenever
            // an object is added or deleted from the main observable collection
            PC pc = new PC(PcViewModel.PcName,PcViewModel.PcDescription, UserViewModel.LoggedInUserId);
            PcViewModel.UserPcs.Add(pc);

        }

        public void SetSelectedPc(PC selectedPc)
        {
            PcViewModel.SelectedPc = selectedPc;
        }

        public async void DeletePc()
        {
            if (PcViewModel.SelectedPc != null)
            {
                var messageDialog = new MessageDialog("Er Du sikker på du vil slette den her spiller: " + PcViewModel.SelectedPc.PcName + "?");

                // If yes, run the UICommand: CommandInvokedHandler 
                messageDialog.Commands.Add(new UICommand("Ret sikker", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                messageDialog.Commands.Add(new UICommand("Please god no", null));

                // Set the command that will be invoked by default
                messageDialog.DefaultCommandIndex = 0;

                // Set the command to be invoked when escape is pressed
                messageDialog.CancelCommandIndex = 1;

                // Show the message dialog
                await messageDialog.ShowAsync();
            }
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            PcViewModel.PcSingleton.DeletePc(PcViewModel.SelectedPc);


            // Making sure we update the sorted list whenever an object is added or deleted to the main observable collection
            PcViewModel.UserPcs.Remove(PcViewModel.SelectedPc);
        }

        public void UpdatePc()
        {
            PcViewModel.PcSingleton.UpdatePc(PcViewModel.SelectedPc);
        }


    }
}
