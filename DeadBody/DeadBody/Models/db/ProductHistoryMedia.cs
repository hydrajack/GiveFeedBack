using System;
using System.Collections.Generic;

#nullable disable

namespace DeadBody.Models.db
{
    public partial class ProductHistoryMedia
    {
        public int ProductHistoryMediaId { get; set; }
        public string ProductHistoryMediaPathOne { get; set; }
        public string ProductHistoryMediaVideoPath { get; set; }
        public string ProductHistoryMediaPathTwo { get; set; }
        public string ProductHistoryMediaPathThree { get; set; }
        public string ProductHistoryMediaPathFour { get; set; }
        public string ProductHistoryMediaPathFive { get; set; }
    }
}
