﻿namespace APICodigoEFC.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public bool IsActive { get; set; }
    }
}
