using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SalesWPFApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand MemberWindowCommand { get; set; }
        public ICommand OrderWindowCommand { get; set; }
        public ICommand ProductWindowCommand { get; set; }


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

            MemberWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { WindowMembers wd = new WindowMembers(); wd.ShowDialog(); });
            OrderWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { WindowOrders wd = new WindowOrders(); wd.ShowDialog(); });
            ProductWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { WindowProducts wd = new WindowProducts(); wd.ShowDialog(); });
        }
    }
}
