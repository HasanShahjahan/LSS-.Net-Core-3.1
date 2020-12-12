using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
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

        public List<byte> WriteAndWait(List<byte> inputBuffer, int dataLength)
        {
            List<byte> commandResponseByte = new List<byte>();
            _serialPort.Write(inputBuffer.ToArray(), 0, inputBuffer.Count);
            bool _continue = true;

            while (_continue)
            {
                try
                {
                    if (_serialPort.BytesToRead > 0)
                    {
                        int currentBufferLength = _serialPort.BytesToRead;
                        for (UInt16 i = 0; i < _serialPort.BytesToRead; i++)
                        {
                            byte byteBuffer = (byte)_serialPort.ReadByte();
                            commandResponseByte.Add(byteBuffer);
                        }
                        if (commandResponseByte.Count < dataLength)
                        {
                            _continue = true;
                        }
                        else
                        {
                            _continue = false;
                        }
                    }
                }
                catch (TimeoutException) { }
            }

            return commandResponseByte;
        }
    }
}
