using HBA.Data.Model;

namespace HBA.Web.Utility
{
    public static class CommissionHelper
    {
        public static decimal CalculateCommission(decimal price,IEnumerable<CommissionSetup> commissionSetups)
        {
            var slab = commissionSetups.FirstOrDefault(x => price >= x.FromAmount &&
            (x.ToAmount == null || price <= x.ToAmount));

            if (slab == null)
                return 0M;

            return price * (slab.CommissionValue / 100) ;
        }
    }
}
