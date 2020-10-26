using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("Account")]
    public class Account
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Date created is required")]
        public DateTime DateCreated { get; set; }
        
        [Required(ErrorMessage = "Account type is required")]
        public string AccountType { get; set; }
        public Guid OwnerId { get; set; }
    }
}
