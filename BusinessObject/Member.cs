using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public partial class Member
    {
        public Member()
        {
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "EMAIL_IN_INTERVAL_0_100")]
        public string Email { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "COMPANY_NAME_IN_INTERVAL_0_40")]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "CITY_IN_INTERVAL_0_15")]
        public string City { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "COUNTRY_IN_INTERVAL_0_15")]
        public string Country { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "PASSWORD_IN_INTERVAL_0_30")]
        public string Password { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Member(int memberId, string email, string companyName, string city, string country, string password, ICollection<Order> orders)
        {
            MemberId = memberId;
            Email = email;
            CompanyName = companyName;
            City = city;
            Country = country;
            Password = password;
            Orders = orders;
        }

        public Member(int memberId, string email, string companyName, string city, string country, string password)
        {
            MemberId = memberId;
            Email = email;
            CompanyName = companyName;
            City = city;
            Country = country;
            Password = password;
        }

        public Member(string email, string companyName, string city, string country, string password)
        {
            Email = email;
            CompanyName = companyName;
            City = city;
            Country = country;
            Password = password;
        }

        public Member(string email, string companyName, string city, string country, string password, ICollection<Order> orders)
        {
            Email = email;
            CompanyName = companyName;
            City = city;
            Country = country;
            Password = password;
            Orders = orders;
        }
    }
}
