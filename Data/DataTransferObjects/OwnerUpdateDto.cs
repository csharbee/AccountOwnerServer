using System;
using System.ComponentModel.DataAnnotations;

namespace Data.DataTransferObjects
{
    /// <summary>
    /// It looks like OwnerCreateDto but if you want to change some one update or create, you can change easly this way.
    /// </summary>
    public class OwnerUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address cannot be loner then 100 characters")]
        public string Address { get; set; }
    }
}
