using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LSS.HCM.Core.Domain.Services
{
    public class LockBoardInitializationCommand : ILockBoardInitializationCommand
    {
        private readonly AppSettings _appSettings;
        public LockBoardInitializationCommand(AppSettings appSettings) 
        {
            _appSettings = appSettings;
        }
        public Dictionary<string, string> ExecuteCommandResponse(string commandType, List<byte> bufferResponse)
        {
            byte Length = bufferResponse.ElementAt(3);
            byte Command = bufferResponse.ElementAt(4);
            List<byte> Data = bufferResponse.GetRange(5, Length - 1);

            Dictionary<string, string> compiledData = new Dictionary<string, string>();
            switch (commandType)
            {
                case CommandType.DoorOpen:
                    //executeDoorOpen(Data);
                    byte moduleNo = Data.ElementAt(0);
                    byte doorNo = Data.ElementAt(1);

                    string openingStatus = "";
                    if (Data.ElementAt(2) == 0xFF)
                    {
                        openingStatus = true.ToString();
                    }
                    else
                    {
                        openingStatus = false.ToString();
                    }
                    compiledData.Add("moduleNo", moduleNo.ToString());
                    compiledData.Add("doorNo", doorNo.ToString());
                    compiledData.Add("DoorOpen", openingStatus);
                    break;
                case CommandType.DoorStatus:
                    //executeDoorOpen(Data);
                    string statusArray1_8 = Convert.ToString(Data.ElementAt(2), 2).PadLeft(8, '0');
                    string statusArray9_16 = Convert.ToString(Data.ElementAt(3), 2).PadLeft(8, '0');
                    string statusArray17_24 = Convert.ToString(Data.ElementAt(4), 2).PadLeft(8, '0');

                    List<char> statusAry = (statusArray1_8 + statusArray9_16 + statusArray17_24).ToList();

                    compiledData.Add("statusAry", statusAry.ToString());
                    break;
                case CommandType.ItemDetection:
                    //executeDoorOpen(Data);
                    break;

            }

            return compiledData;




            /*
             
              var commandName = inputdata['command'];
              var hexDataBuffer = inputdata['data'];
              var ret = { "command": commandName, "data": {} };
              if(commandName == "doorstatus") {
              }
              else if(commandName == "opendoor") {
              }
              else if(commandName == "detectitem") {
                var detectionArray1_8Hex = hexDataBuffer.slice(3, 4).toString("hex").toUpperCase();
                var detectionArray9_16Hex = hexDataBuffer.slice(4, 5).toString("hex").toUpperCase();
                var detectionArray17_24Hex = hexDataBuffer.slice(5, 6).toString("hex").toUpperCase();
                var detectionArray25_32Hex = hexDataBuffer.slice(6, 7).toString("hex").toUpperCase();
                var detectionArray1_8 = (parseInt(detectionArray1_8Hex, 16)).toString(2).padStart(8, '0').split('');
                var detectionArray9_16 = (parseInt(detectionArray9_16Hex, 16)).toString(2).padStart(8, '0').split('');
                var detectionArray17_24 = (parseInt(detectionArray17_24Hex, 16)).toString(2).padStart(8, '0').split('');
                var detectionArray25_32 = (parseInt(detectionArray25_32Hex, 16)).toString(2).padStart(8, '0').split('');
                var detectionAry = detectionArray1_8.concat(detectionArray9_16.concat(detectionArray17_24.concat(detectionArray25_32)));

                ret['data'] = {
                  detection: detectionAry
                };

              }
              else {
                logger.operationERRlog('Serial Hex Data Extraction', '[Unknown Command Response][' + hexDataBuffer + ']');
              }
             */
        }

        public List<byte> GenerateCommandBuffer(string commandName, List<byte> commandData)
        {
            // Assign a Header of command
            List<byte> commandHeader = SplitStringToByte(_appSettings.Microcontroller.CommandHeader);
            //List<byte> commandHeader = new List<byte>() { 0x88, 0x30, 0x9D };

            // Assign command idcode and response length
            byte commandDataLenght = 0, commandIDcode = 0;
            if (commandName == CommandType.DoorOpen)
            {
                commandDataLenght = Convert.ToByte(_appSettings.Microcontroller.Commands.OpenDoor.Length, 16);
                commandIDcode = Convert.ToByte(_appSettings.Microcontroller.Commands.OpenDoor.Code, 16);
            }
            else if (commandName == CommandType.DoorStatus)
            {
                commandDataLenght = Convert.ToByte(_appSettings.Microcontroller.Commands.DoorStatus.Length, 16);
                commandIDcode = Convert.ToByte(_appSettings.Microcontroller.Commands.DoorStatus.Code, 16);
            }
            else if (commandName == CommandType.ItemDetection)
            {
                commandDataLenght = Convert.ToByte(_appSettings.Microcontroller.Commands.DetectItem.Length, 16);
                commandIDcode = Convert.ToByte(_appSettings.Microcontroller.Commands.DetectItem.Code, 16);
            }

            // Write value to command byte List
            List<byte> commandByte = new List<byte>();
            commandByte.AddRange(commandHeader);
            commandByte.Add(commandDataLenght);
            commandByte.Add(commandIDcode);
            commandByte.AddRange(commandData);

            // Caculate checksum CRC16 MODBUS
            List<byte> commandChecksum = GenerateCRC16CheckSum(commandByte);
            commandByte.AddRange(commandChecksum);

            return commandByte;

        }
        private List<byte> SplitStringToByte(string strInput)
        {
            int chunkSize = 2;
            List<byte> hexByteArray = new List<byte>();
            int stringLength = strInput.Length;
            for (int i = 0; stringLength > 0; i += chunkSize)
            {
                string subStringInput = strInput.Substring(i, chunkSize);
                hexByteArray.Add((byte)Convert.ToInt16(subStringInput, 16));
                //hexByteArray = hexByteArray.AddRange(commandDataLenght).ToArray();
                stringLength -= 2;
            }
            return hexByteArray;
        }

        private List<byte> GenerateCRC16CheckSum(List<byte> byteCommand)
        {
            uint crc16 = 0xffff;
            uint temp;
            uint flag;

            for (int i = 0; i < byteCommand.Count; i++)
            {
                temp = (uint)byteCommand[i];
                temp &= 0x00ff; // MSB Marks
                crc16 = crc16 ^ temp; //crc16 XOR with temp 
                for (uint c = 0; c < 8; c++)
                {
                    flag = crc16 & 0x01;
                    crc16 = crc16 >> 1;
                    if (flag != 0)
                        crc16 = crc16 ^ 0x0a001;
                }
            }
            List<byte> crc16modbusArray = new List<byte>();
            crc16modbusArray.Add(((byte)(crc16 >> 8)));
            crc16modbusArray.Add((byte)(crc16 >> 0));

            return crc16modbusArray;
        }

        private string ConvertStatusListToString(List<char> statusList)
        {
            string statusString = String.Join(" ", statusList);
            return statusString;
        }
    }
}

