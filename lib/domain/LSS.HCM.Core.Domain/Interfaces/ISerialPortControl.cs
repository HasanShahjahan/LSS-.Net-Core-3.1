using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.Domain.Interfaces
{
    public interface ISerialPortControl
    {
        void Initialize();
        void CreateBuffer(string commandHeader, string hexData);
        void WriteBufferThroughSerialPort();
        void ReadDataFromSerialBuffer();
        List<byte> WriteAndWait(List<byte> inputBuffer, int dataLength);
    }
}
