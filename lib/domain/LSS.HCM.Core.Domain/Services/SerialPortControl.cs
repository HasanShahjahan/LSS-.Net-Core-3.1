using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Interfaces;
using System;
using System.IO.Ports;

namespace LSS.HCM.Core.Domain.Services
{
    public class SerialPortControl : ISerialPortControl
    {
        private readonly SerialPort _serialPort = new SerialPort();
        private readonly AppSettings _appSettings;
        public SerialPortControl(AppSettings appSettings) 
        {
            _appSettings = appSettings;
            _serialPort.PortName = _appSettings.Microcontroller.LockControl.Name;
            _serialPort.BaudRate = _appSettings.Microcontroller.LockControl.Baudrate;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = _appSettings.Microcontroller.LockControl.DataBits;
            //_serialPort.StopBits = 0;
            _serialPort.Handshake = Handshake.None;
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
           // Initialize();
        }

        public void Initialize() => _serialPort.Open();
        public void CreateBuffer(string commandHeader, string hexData)
        {
            var cmdData = commandHeader + hexData;
        }

        public void ReadDataFromSerialBuffer()
        {
            throw new NotImplementedException();
        }

        public void WriteBufferThroughSerialPort()
        {
            throw new NotImplementedException();
        }

        public string WriteAndWait(string inputBuffer, int dataLength)
        {
            throw new NotImplementedException();
        }
    }
}
