using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TemperatureMonitor.Models;

namespace TemperatureMonitor.Utility
{

    public class SerialPortUtility
    {
        SensorEntities _context = new SensorEntities();        

        private SerialPort _serialPort;

        public void StartMonitoring()
        {
            OpenSerialPort();
        }

        private void OpenSerialPort()
        {
            _serialPort = new SerialPort {Parity = Parity.None, DataBits = 8, StopBits = StopBits.Two};
            string[] serialPorts = SerialPort.GetPortNames();
            _serialPort.PortName = serialPorts[0];
            _serialPort.BaudRate = 9600;
            _serialPort.DataReceived += PortDataReceived;
            _serialPort.Open();
            Debug.WriteLine("Port is open: " + _serialPort.PortName);
        }

        private void CloseSerialPort()
        {
            _serialPort.Close();
        }

        private void PortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                string indata = sp.ReadExisting();
                Console.WriteLine("Data Received:");
                Console.Write(indata);
            }
            catch (Exception ex)
            {
                // Handle exception
                Debug.WriteLine("PortDataReceived(): something went wrong");
                Debug.WriteLine(ex.ToString());
            }
        }


    }

}