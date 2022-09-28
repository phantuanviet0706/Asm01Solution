using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Member> GetMembersList()
        {
            List<Member> membersList;
            try
            {
                var asm1DB = new Assignment1Context();
                membersList = asm1DB.Members.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return membersList;
        }

        public Member GetMemberById(int? id)
        {
            Member member = null;
            try
            {
                var asm1DB = new Assignment1Context();
                member = asm1DB.Members.SingleOrDefault(member => member.MemberId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        public void AddNew(Member member)
        {
            try
            {
                Member _member = GetMemberById(member.MemberId);
                if (_member == null)
                {
                    var asm1DB = new Assignment1Context();
                    asm1DB.Members.Add(member);
                    asm1DB.SaveChanges();
                }
                else
                {
                    throw new Exception("This member is already existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateMember(Member member)
        {
            try
            {
                Member _member = GetMemberById(member.MemberId);
                if (member != null)
                {
                    var asm1DB = new Assignment1Context();
                    asm1DB.Entry(member).State = EntityState.Modified;
                    asm1DB.SaveChanges();
                }
                else
                {
                    throw new Exception("This member isn't existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteMember(Member member)
        {
            try
            {
                Member _member = GetMemberById(member.MemberId);
                if (_member != null)
                {
                    var asm1DB = new Assignment1Context();
                    asm1DB.Members.Remove(member);
                    asm1DB.SaveChanges();
                }
                else
                {
                    throw new Exception("This member isn't existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
