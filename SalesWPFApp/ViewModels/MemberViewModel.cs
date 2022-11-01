using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SalesWPFApp.ViewModels
{
    public class MemberViewModel : BaseViewModel
    {
        MemberRepository memberRepository;
        public ICommand AddMemberCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ObservableCollection<Member> memberList { get; set; }
        public MemberViewModel()
        {
            this.memberRepository = new MemberRepository();
            memberList = new ObservableCollection<Member>();

            loadMemberList();
            AddMemberCommand = new RelayCommand<UIElementCollection>((p) => { return true; }, (p) =>
            {
                string email = null;
                string companyName = null;
                string city = null;
                string country = null;
                string password = null;
                Boolean isAllValid = true;
                try
                {
                    foreach (var i in p)
                    {
                        TextBox tb = i as TextBox;
                        if (tb != null)
                        {

                            switch (tb.Name)
                            {
                                case "email":
                                    email = tb.Text;
                                    break;
                                case "companyName":
                                    companyName = tb.Text;
                                    break;
                                case "city":
                                    city = tb.Text;
                                    break;
                                case "country":
                                    country = tb.Text;
                                    break;
                                case "password":
                                    password = tb.Text;
                                    break;
                            }


                        }

                    }
                }
                catch (Exception ex)
                {
                    isAllValid = false;
                }
                if (isAllValid == true)
                {
                    Member member = new Member(email, companyName, city, country, password);
                    memberRepository.InsertMember(new Member(email, companyName, city, country, password));
                    loadMemberList();
                }
                else
                    return;
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
    }
}
