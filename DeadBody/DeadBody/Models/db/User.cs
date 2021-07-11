using System;
using System.Collections.Generic;

#nullable disable

namespace DeadBody.Models.db
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserAccount { get; set; }
        public string UserPassword { get; set; }
        public string UserRealName { get; set; }
        public string UserHeadShotPath { get; set; }
        public string UserIntroduction { get; set; }
        public string UserBackgroundColor { get; set; }
        public string UserEmail { get; set; }
        public DateTime UserDateTime { get; set; }
    }
}
