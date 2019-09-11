using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int> //using identity framework
    {
        public string FullName { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string PhotoUrl { get; set; }
        public string Title { get; set; }
        public bool IsDeactivated { get; set; }
    }
}
