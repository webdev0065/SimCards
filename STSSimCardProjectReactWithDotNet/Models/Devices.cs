using System.ComponentModel.DataAnnotations.Schema;

namespace STSSimCardProjectReactWithDotNet.Models
{
    public class Devices
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }


        [ForeignKey("SimCardId")]
        public int SimIDNumber { get; set; }

    }
}
