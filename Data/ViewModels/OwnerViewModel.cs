using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class OwnerViewModel
    {
        //public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public IEnumerable<AccountViewModel> Accounts { get; set; }
    }
}
