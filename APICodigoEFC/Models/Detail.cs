namespace APICodigoEFC.Models
{
    public class Detail
    {
        public int DetailID { get; set; }

        public bool IsActive {  get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int InvoiceID { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
