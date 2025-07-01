using System.ComponentModel;

namespace HBA.Data.Model
{
    public class Property
    {
        public int Id { get; set; }
        [DisplayName("Property Name")]
        public string PropertyName { get; set; } = null!;
        [DisplayName("Property Type")]
        public int PropertyTypeId { get; set; }
        public string Location { get; set; } = null!;
        public decimal Price { get; set; }
        public PropertyType? PropertyType { get; set; }
    }
}
