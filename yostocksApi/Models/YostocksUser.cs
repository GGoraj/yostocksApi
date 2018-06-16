using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace yostocksApi.Models
{
    public class YostocksUser
    {
        public int Id { get; set; }
        [Required, StringLength(15)]
        public string FirstName { get; set; }
        [Required, StringLength(15)]
        public string LastName { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telephone Number Required")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ImagePath { get; set; }

        //[NotMapped]
        //public string StockFragmentId { get; set; }

        //Navigation Properties
        [DataMember]
        public virtual ICollection<Fragment> Fragments { get; set; }
    }
}