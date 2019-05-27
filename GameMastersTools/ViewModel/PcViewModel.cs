using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameMastersTools.Annotations;
using GameMastersTools.Common;
using GameMastersTools.Handler;
using GameMastersTools.Model;
using GameMastersTools.Persistency;
using GameMastersTools.Singleton;

namespace GameMastersTools.ViewModel
{
    public class PcViewModel : INotifyPropertyChanged
    {
        // Fields
        private ObservableCollection<PC> _userPcs;
        private string _filterText;
        private ICommand _createPcCommand;
        private ICommand _selectedPcCommand;
        private ICommand _deletePcCommand;
        private ICommand _updatePcCommand;
        private ICommand _timerCommand;
        private string _errorMessage;


        // Properties
        public PcSingleton PcSingleton { get; set; }

        public List<PC> ppp;

        public ObservableCollection<PC> UserPcs
        {
            get { return _userPcs; }
            set
            {
                _userPcs = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }

        }

        //public ObservableCollection<string> Avatars { get; set; }

        public static PC SelectedPc { get; set; }
        public PcHandler PcHandler { get; set; }

        public string PcName { get; set; }
        public string PcDescription { get; set; }
        public int UserId { get; set; }

        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (_filterText != null)
                {
                    _filterText = value;
                    OnPropertyChanged();
                }
                FilterPc();
            }
        }

        public string SaveMessage { get; set; }

        // ICommands
        public ICommand SelectedPcCommand
        {
            get { return _selectedPcCommand ?? (_selectedPcCommand = new RelayArgCommand<PC>(pc => PcHandler.SetSelectedPc(pc))); }
            set { _selectedPcCommand = value; }
        }

        public ICommand CreatePcCommand
        {
            get { return _createPcCommand ?? (_createPcCommand = new RelayCommand(PcHandler.CreatePc)); }
            set { _createPcCommand = value; }
        }

        public ICommand DeletePcCommand
        {
            get { return _deletePcCommand ?? (_deletePcCommand = new RelayCommand(PcHandler.DeletePc)); }
            set { _deletePcCommand = value; }
        }

        public ICommand UpdatePcCommand
        {
            get { return _updatePcCommand ?? (_updatePcCommand = new RelayCommand(PcHandler.UpdatePc)); }
            set { _updatePcCommand = value; }
        }

        //public ICommand TimerCommand
        //{
        //    get { return _timerCommand ?? (_timerCommand = new RelayCommand(PcHandler.Timer)); }
        //    set { _timerCommand = value; }
        //}

        // Constructor
        public PcViewModel()
        {
            PcSingleton = PcSingleton.Instance;
            PcHandler = new PcHandler(this);

            // Having all the Players in the database loaded into the list from the start, then we sort it later, so it fits the logged in user
            UserPcs = PcSingleton.Pcs;

            SortPc();

            ErrorMessage = "";

        }

        public void SortPc()
        {
            UserPcs = new ObservableCollection<PC>(PcSingleton.Pcs.Where(e => e.UserId == UserViewModel.LoggedInUserId).OrderBy(e => e.PcName));

            //var userPcs = from pc in PcSingleton.Pcs
            //    where pc.UserId == UserViewModel.LoggedInUserId
            //    select pc;


            //foreach (var pc in userPcs)
            //{
            //    UserPcs.Add(pc);
            //}

        }


        /// <summary>
        /// Making sure the TextBox property _filterText isn't null, then getting all the players that belong to the User loaded in, then updating the list based on input from the filters
        /// </summary>
        public void FilterPc()
        {
            if (_filterText == null) _filterText = "";

            UserPcs = new ObservableCollection<PC>(PcSingleton.Instance.Pcs.Where(
                e =>
                    e.UserId == UserViewModel.LoggedInUserId &&
                    (e.PcName.ToLower().Contains(FilterText.ToLower()) ||
                    e.PcDescription.ToLower().Contains(FilterText.ToLower()))
                ).OrderBy(e => e.PcName));
        }


        // More testing needed - Can this change the frame in frame where it's needed in PlayerDetailsPage
        public void GoBack()
        {
            Frame newFrame = Window.Current.Content as Frame;
            if (newFrame != null)
            {
                if (newFrame.CanGoBack)
                {
                    newFrame.GoBack();
                }
            }
        }



        public int PcCount()
        {
            return UserPcs.Count();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
