using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemperatureMonitor.Models;
using TemperatureMonitor.ViewModels;

namespace TemperatureMonitor.Controllers
{
    public class HomeController : Controller
    {
        SensorEntities context = new SensorEntities();

        public ActionResult Index()
        {
            //LoadDummyData();
            List<TemperatureSensor> temperatureSensors = context.TemperatureSensors.ToList();
            var temperatures = temperatureSensors.First().Temperatures.Select(temperature => temperature.Value).ToList();
            var model = new DashboardViewModel {TemperatureSensors = temperatureSensors, TemperatureValues = temperatures.ToArray()};            
            return View(model);
        }

        public ActionResult Details(int id)
        {
            TemperatureSensor sensor = context.TemperatureSensors.SingleOrDefault(s => s.SensorId == id);

            if (sensor == null)
            {
                return HttpNotFound();
            }
            return View(sensor);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(TemperatureSensor sensor)
        {
            if (ModelState.IsValid)
            {
                context.TemperatureSensors.Add(sensor);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sensor);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            TemperatureSensor sensor = context.TemperatureSensors.Single(p => p.SensorId == id);
            if (sensor == null)
            {
                return HttpNotFound();
            }
            return View(sensor);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id, TemperatureSensor sensor)
        {
            TemperatureSensor _sensor = context.TemperatureSensors.Single(p => p.SensorId == id);

            if (ModelState.IsValid)
            {
                _sensor.Name = sensor.Name;
                _sensor.Description = sensor.Description;

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sensor);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            TemperatureSensor sensor = context.TemperatureSensors.Single(p => p.SensorId == id);
            if (sensor == null)
            {
                return HttpNotFound();
            }
            return View(sensor);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id, TemperatureSensor sensor)
        {
            TemperatureSensor _sensor = context.TemperatureSensors.Single(p => p.SensorId == id);
            context.TemperatureSensors.Remove(_sensor);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }

        [Obsolete]
        private void LoadDummyData()
        {

            var sensor = new TemperatureSensor { Name = "Sensor1", Description = "STM32 dummy sensor" };
            var dt = new DateTime();
            var tempList = new List<Temperature>();
            var r = new Random();
            for (int i = 0; i < 10; i++ )
            {
                tempList.Add(new Temperature {Sensor = sensor, Value = r.NextDouble()*25 , Time = DateTime.Now.AddSeconds(-i).Ticks});
            }
            
            context.TemperatureSensors.Add(sensor);
            foreach (var temperature in tempList)
            {
                context.Temperatures.Add(temperature);
            }
            context.SaveChanges();
        }
    }
}
