using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SalesWPFApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand AddMemberCommand { get; set; }
        public ICommand LoadedTabCommand { get; set; }

        private int _selected;
        public int Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                IsLoaded = true;
                if (p == null)
                {
                    return;
                }
                p.Hide();
                WindowLogin wdLogin = new WindowLogin();
                wdLogin.ShowDialog();

                if (wdLogin.DataContext == null)
                {
                    return;
                }
                var loginVM = wdLogin.DataContext as LoginViewModel;
                if (loginVM.IsLogin)
                {
                    p.Show();
                }
                else
                {
                    p.Close();
                }
            });

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p == null)
                {
                    return;
                }

                if (MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    p.Close();
                }
            });


            LoadedTabCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                var _index = p.DataContext as MainViewModel;
                MessageBox.Show("Index " + _index.Selected);
            });
        }
    }
}
