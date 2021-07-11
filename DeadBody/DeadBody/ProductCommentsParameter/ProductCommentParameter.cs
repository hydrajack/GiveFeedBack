using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeadBody.ProductCommentsParameter
{
    public class ProductCommentParameter
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string CommentContent { get; set; }
    }
}
