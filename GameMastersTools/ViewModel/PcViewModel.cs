using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GameMastersTools.Common;
using GameMastersTools.Handler;
using GameMastersTools.Singleton;

namespace GameMastersTools.ViewModel
{
    class PcViewModel
    {
        // Fields
        private ICommand _createPcCommand;

        // Properties
        public PcSingleton PcSingleton { get; set; }
        public PcHandler PcHandler { get; set; }
        public string  PcName { get; set; }
        public string PcDescription { get; set; }
        public int UserId { get; set; }

        public ICommand CreaterPcCommand
        {
            get { return _createPcCommand ?? (_createPcCommand = new RelayCommand(PcHandler.CreatePc)); }
            set { _createPcCommand = value; }
        }

        // Constructor
        public PcViewModel()
        {
            PcSingleton = PcSingleton.Instance;
            PcHandler = new PcHandler(this);
        }
    }
}
