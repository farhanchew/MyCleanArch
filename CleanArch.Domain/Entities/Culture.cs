using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Entities
{

    public class Culture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Resource> Resources { get; set; }
    }
}
