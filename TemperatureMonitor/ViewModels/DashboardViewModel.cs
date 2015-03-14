using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TemperatureMonitor.Models;

namespace TemperatureMonitor.ViewModels
{
    public class DashboardViewModel
    {
        public List<TemperatureSensor> TemperatureSensors { get; set; }
        public double[] TemperatureValues { get; set; }
    }
}