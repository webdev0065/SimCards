using System.ComponentModel.DataAnnotations.Schema;

namespace STSSimCardProjectReactWithDotNet.Models
{
    public class SimCard
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public int ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; }
        public string IsActiveUser { get; set; }
        public int DevicesId { get; set; }
        [ForeignKey("DevicesId")]
        public Devices Devices { get; set; }

    }
}

