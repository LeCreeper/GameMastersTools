using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using GameMastersTools.Annotations;
using GameMastersTools.Common;
using GameMastersTools.Handler;
using GameMastersTools.Model;
using GameMastersTools.Persistency;
using GameMastersTools.Singleton;

namespace GameMastersTools.ViewModel
{
    class PcViewModel : INotifyPropertyChanged
    {
        // Fields
        private ObservableCollection<PC> _userPcs;

        private ICommand _createPcCommand;

        private ICommand _selectedPcCommand;

        private ICommand _deletePcCommand;
        // Properties

        public PcSingleton PcSingleton { get; set; }
    
        public ObservableCollection<PC> UserPcs
        {
            get { return _userPcs; }
            set
            {
                _userPcs = value;
                OnPropertyChanged();
            }

        }

        //public ObservableCollection<string> Avatars { get; set; }

        public PC SelectedPc { get; set; }
        public PcHandler PcHandler { get; set; }
        public string  PcName { get; set; }
        public string PcDescription { get; set; }
        public int UserId { get; set; }
        
        public ICommand SelectedPcCommand
        {
            get { return _selectedPcCommand ?? (_selectedPcCommand = new RelayArgCommand<PC>(pc => PcHandler.SetSelectedPc(pc))); }
            set { _selectedPcCommand = value; }
        }

        public ICommand CreatePcCommand
        {
            get { return _createPcCommand ?? (_createPcCommand = new RelayCommand(PcHandler.CreatePc)); }
            set
            {
                _createPcCommand = value;
            }
        }
        
        public ICommand DeletePcCommand
        {
            get { return _deletePcCommand ?? (_deletePcCommand = new RelayCommand(PcHandler.DeletePc)); }
            set
            {
                _deletePcCommand = value;
            }
        }

        // Constructor
        public PcViewModel()
        {
            PcSingleton = PcSingleton.Instance;
            PcHandler = new PcHandler(this);


            UserPcs = PcSingleton.Pcs;
            //Avatars = new ObservableCollection<string>();
            
            SortPc();
            
        }

        public void SortPc()
        {
            UserPcs = new ObservableCollection<PC>(PcSingleton.Pcs.Where(e => e.UserId == UserViewModel.LoggedInUserId));
            
            //var userPcs = from pc in PcSingleton.Pcs
            //    where pc.UserId == UserViewModel.LoggedInUserId
            //    select pc;
            

            //foreach (var pc in userPcs)
            //{
            //    UserPcs.Add(pc);
            //}
          
        }



        //public void LoadAvatars()
        //{
        //    Avatars.Add("../Assets/Blue.jpg");
        //    Avatars.Add("../Assets/Red.jpg");
        //    Avatars.Add("../Assets/Brown.jpg");
        //    Avatars.Add("../Assets/Purple.jpg");
        //    Avatars.Add("../Assets/Green.jpg");
        //}
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
