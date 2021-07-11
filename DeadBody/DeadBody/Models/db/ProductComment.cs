using System;
using System.Collections.Generic;

#nullable disable

namespace DeadBody.Models.db
{
    public partial class ProductComment
    {
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentDateTime { get; set; }
    }
}
