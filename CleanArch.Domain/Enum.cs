using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain
{
    public enum UserRoles : byte
    {
        SystemAdmin = 1,
        Teacher = 2,
        Student = 3,
        User = 4,
    }
}
