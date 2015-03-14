using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TemperatureMonitor.Models
{
    [Bind(Exclude = "TemperatureId")]
    public partial class Temperature
    {
        [Key]
        public int TemperatureId { get; set; }
        public long Time { get; set; } // SQL CE 4.0 does not support TimeSpan
        public double Value { get; set; }
        public virtual TemperatureSensor Sensor { get; set; }
    }
}