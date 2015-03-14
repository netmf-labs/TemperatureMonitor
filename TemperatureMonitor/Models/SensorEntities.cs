using System.Data.Entity;

namespace TemperatureMonitor.Models
{
    public class SensorEntities : DbContext
    {
        public DbSet<TemperatureSensor> TemperatureSensors { get; set; }
        public DbSet<Temperature> Temperatures { get; set; }
    }
}