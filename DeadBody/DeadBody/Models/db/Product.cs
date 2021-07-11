using System;
using System.Collections.Generic;

#nullable disable

namespace DeadBody.Models.db
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductSubtitle { get; set; }
        public string ProductContent { get; set; }
        public string ProductHeadShotPath { get; set; }
        public string ProductUrl { get; set; }
        public int ProductMediaId { get; set; }
        public string ProductCategory { get; set; }
        public DateTime ProductDateTime { get; set; }
    }
}
