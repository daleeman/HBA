using HBA.Web.Utility;
using HBA.Data.Model;

public class CommissionHelperTests
{
    [Fact]
    public void CalculateCommission_WithValidSlab_ReturnsCorrectCommission()
    {
        // Arrange
        var price = 1000m;
        var commissionSetups = new List<CommissionSetup>
        {
            new CommissionSetup { FromAmount = 0, ToAmount = 2000, CommissionValue = 10 } // 10%
        };

        // Act
        var result = CommissionHelper.CalculateCommission(price, commissionSetups);

        // Assert
        Assert.Equal(100m, result); // 10% of 1000 = 100
    }

    [Fact]
    public void CalculateCommission_WithNoMatchingSlab_ReturnsZero()
    {
        // Arrange
        var price = 5000m;
        var commissionSetups = new List<CommissionSetup>
        {
            new CommissionSetup { FromAmount = 0, ToAmount = 4000, CommissionValue = 10 }
        };

        // Act
        var result = CommissionHelper.CalculateCommission(price, commissionSetups);

        // Assert
        Assert.Equal(0m, result); // No matching slab
    }

    [Fact]
    public void CalculateCommission_WithNullToAmount_ReturnsCorrectCommission()
    {
        // Arrange
        var price = 6000m;
        var commissionSetups = new List<CommissionSetup>
        {
            new CommissionSetup { FromAmount = 5000, ToAmount = null, CommissionValue = 5 }
        };

        // Act
        var result = CommissionHelper.CalculateCommission(price, commissionSetups);

        // Assert
        Assert.Equal(300m, result); // 5% of 6000 = 300
    }

    [Fact]
    public void CalculateCommission_WithEmptyCommissionSetup_ReturnsZero()
    {
        // Arrange
        var price = 1000m;
        var commissionSetups = new List<CommissionSetup>(); // No slabs

        // Act
        var result = CommissionHelper.CalculateCommission(price, commissionSetups);

        // Assert
        Assert.Equal(0m, result);
    }
}
