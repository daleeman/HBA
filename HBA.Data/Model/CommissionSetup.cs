namespace HBA.Data.Model
{
    public class CommissionSetup
    {
        public int Id { get; set; }
        public decimal FromAmount { get; set; }
        public decimal? ToAmount { get; set; }
        public decimal CommissionValue { get; set; }
    }
}
