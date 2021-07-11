using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeadBody.ProductCommentsParameter
{
    public class ProductCommentReturn
    {
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string UserAccount { get; set; }
        public string UserRealName { get; set; }
        public string UserHeadShotPath { get; set; }
        public string UserIntroduction { get; set; }
        public string UserBackgroundColor { get; set; }
        public string UserEmail { get; set; }
        public DateTime UserDateTime { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentDateTime { get; set; }
    }
}
