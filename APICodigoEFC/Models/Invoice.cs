namespace APICodigoEFC.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
