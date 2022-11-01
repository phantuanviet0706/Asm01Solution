using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SalesWPFApp.ViewModels
{
    public class MemberViewModel : BaseViewModel
    {
        private string _email;
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }

        private string _companyName;
        public string CompanyName { get => _companyName; set { _companyName = value; OnPropertyChanged(); } }

        private string _city;
        public string City { get => _city; set { _city = value; OnPropertyChanged(); } }

        private string _country;
        public string Country { get => _country; set { _country = value; OnPropertyChanged(); } }

        private string _password;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }


        MemberRepository memberRepository;
        public ICommand AddMemberCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ObservableCollection<Member> memberList { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        private Member _selectedItem;
        public Member SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value; 
                OnPropertyChanged();
                if(SelectedItem != null)
                {
                    Email = SelectedItem.Email;
                    CompanyName = SelectedItem.CompanyName;
                    City = SelectedItem.City;
                    Country = SelectedItem.Country;
                }
            }
        }

        public MemberViewModel()
        {
            this.memberRepository = new MemberRepository();
            memberList = new ObservableCollection<Member>();

            loadMemberList();
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });

            AddMemberCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p == null)
                {
                    return;
                }

                if (Password == null)
                {
                    MessageBox.Show("Mật khẩu không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string passEncode = MD5Hash(Base64Encode(Password));
                Member member = new Member(Email, CompanyName, City, Country, Password);
                try
                {
                    memberRepository.InsertMember(member);
                    MessageBox.Show("Thêm thành viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.None);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                loadMemberList();
            });

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p == null)
                {
                    return;
                }
                p.Close();
            });
        }

        public void loadMemberList()
        {
            memberList.Clear();
            List<Member> members = (List<Member>)memberRepository.GetMembers();
            foreach (Member member in members)
            {
                memberList.Add(member);
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] bytes = mD5CryptoServiceProvider.ComputeHash(new UTF8Encoding().GetBytes(input));
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
