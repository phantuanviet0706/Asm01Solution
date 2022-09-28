using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    internal interface IMemberRepository
    {
        IEnumerable<Member> GetMembers();
        Member GetMemberById(int memberId);
        void InsertMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(Member member);
    }
}
