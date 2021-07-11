using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeadBody.Parameter
{
    public class UserEditParameter
    {
        //本名、個人大頭貼網址、個人訊息、背景色碼、電子郵件帳號
        public string UserRealName { get; set; }
        public IFormFile HeadShotFile { get; set; }
        public string UserIntroduction { get; set; }
        public string UserBackgroundColor { get; set; }
        public string UserEmail { get; set; }

    }
}
