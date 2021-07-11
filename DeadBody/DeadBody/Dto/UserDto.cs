using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeadBody.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserAccount { get; set; }       
        public string UserRealName { get; set; }
        public string UserHeadShotPath { get; set; }
        public string UserIntroduction { get; set; }
        public string UserBackgroundColor { get; set; }
        public string UserEmail { get; set; }
        public DateTime UserDateTime { get; set; }
    }
}
