﻿namespace Cafe_Management.Core.Entities
{
    public class HistoryDisscount
    {
        public int History_ID { get; set; }
        public int Customer_ID { get; set; }
        public int Cuppon_ID { get; set; }
        public int PriceDisscount { get; set; }
        public int ReceiptDetail_ID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}