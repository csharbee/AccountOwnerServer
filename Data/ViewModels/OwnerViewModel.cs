﻿using System;
using System.Collections.Generic;

namespace Data.ViewModels
{
    public class OwnerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public IEnumerable<AccountViewModel> Accounts { get; set; }
    }
}
