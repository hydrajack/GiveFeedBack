using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeadBody.DiscusstipnCommentsParameter
{
    public class DiscusstionCommentsPost
    {
        public int DiscusstionId { get; set; }
        public int UserId { get; set; }
        public string DiscusstionCommentContent { get; set; }
    }
}
