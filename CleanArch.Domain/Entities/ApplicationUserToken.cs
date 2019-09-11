using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Entities
{
    public class ApplicationUserToken : IdentityUserToken<int> //using identity framework
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ValidityDate { get; set; }
    }
}
