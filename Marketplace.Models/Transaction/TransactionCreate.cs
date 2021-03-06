﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Models.Transaction
{
    public class TransactionCreate
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public int NumItems { get; set; }
        public string ProductName { get; set; }
        public double ProductQuantity { get; set; }
        public int ProductUpc { get; set; }
        public double ProductPrice { get; set; }
        public int CustomerId { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
    }
}
