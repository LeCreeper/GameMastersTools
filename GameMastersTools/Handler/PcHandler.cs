using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                PcViewModel.PcDescription, 
                PcViewModel.UserId
                );
        }
    }
}
