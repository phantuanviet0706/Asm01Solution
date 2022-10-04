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
        [Required(AllowEmptyStrings = false ,ErrorMessage = "EMAIL_EMPTY")]
        [StringLength(100, ErrorMessage = "EMAIL_IN_INTERVAL_0_100")]
        public string Email { get; set; } = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "COMPANY_NAME_EMPTY")]
        [StringLength(40, ErrorMessage = "COMPANY_NAME_IN_INTERVAL_0_40")]
        public string CompanyName { get; set; } = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "CITY_EMPTY")]
        [StringLength(15, ErrorMessage = "CITY_IN_INTERVAL_0_15")]
        public string City { get; set; } = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "COUNTRY_EMPTY")]
        [StringLength(15, ErrorMessage = "COUNTRY_IN_INTERVAL_0_15")]
        public string Country { get; set; } = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "PASSWORD_EMPTY")]
        [StringLength(30, ErrorMessage = "PASSWORD_IN_INTERVAL_0_30")]
        public string Password { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
