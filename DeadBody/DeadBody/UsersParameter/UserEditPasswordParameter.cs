using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeadBody.Parameter
{
    public class UserEditPasswordParameter
    {      
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string NewPasswordAgain{ get; set; }
    }
}
