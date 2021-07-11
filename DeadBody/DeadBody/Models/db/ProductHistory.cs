using System;
using System.Collections.Generic;

#nullable disable

namespace DeadBody.Models.db
{
    public partial class ProductHistory
    {
        public int ProductHistoryId { get; set; }
        public int ProductId { get; set; }
        public string ProductHistoryTitle { get; set; }
        public string ProductHistoryHeadShot { get; set; }
        public int ProductHistoryMediaId { get; set; }
        public string ProductHistoryContent { get; set; }
        public string ProductHistoryVersion { get; set; }
        public DateTime ProductHistorytDateTime { get; set; }
    }
}
