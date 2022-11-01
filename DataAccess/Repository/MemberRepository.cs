using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public Member GetMemberById(int memberId) => MemberDAO.Instance.GetMemberById(memberId);
        public IEnumerable<Member> GetMembers() => MemberDAO.Instance.GetMembersList();
        public void InsertMember(Member member) => MemberDAO.Instance.AddNew(member);
        public void UpdateMember(Member member) => MemberDAO.Instance.UpdateMember(member);
        public void DeleteMember(Member member) => MemberDAO.Instance.DeleteMember(member);
    }
}
