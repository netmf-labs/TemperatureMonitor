using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TemperatureMonitor.Models
{
    public class TemperatureSensor
    {
        [Key]
        public int SensorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Temperature> Temperatures { get; set; }
    }
}