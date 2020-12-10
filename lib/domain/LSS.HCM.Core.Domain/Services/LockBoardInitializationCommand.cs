using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Interfaces;
using System;

namespace LSS.HCM.Core.Domain.Services
{
    public class LockBoardInitializationCommand : ILockBoardInitializationCommand
    {
        private readonly AppSettings _appSettings;
        public LockBoardInitializationCommand(AppSettings appSettings) 
        {
            _appSettings = appSettings;
        }
        public string CompiledCommandResponse(string bufferResponse)
        {
            throw new NotImplementedException();
        }

        public string ExecuteHexData(string commandType, string inputData)
        {
            switch (commandType)
            {
                case CommandType.DoorOpen:
                    break;
                case CommandType.DoorStatus:
                    break;
                case CommandType.ItemDetection:
                    break;

            }
            return string.Empty;
        }

        public string GenerateCommandBuffer(string commandName, string commandData)
        {
            string commandHeader = string.Empty;
            if (commandData == CommandType.DoorOpen)
            {
                commandHeader = _appSettings.Microcontroller.CommandHeader + _appSettings.Microcontroller.Commands.OpenDoor.Length + _appSettings.Microcontroller.Commands.OpenDoor.Code;
            }
            else if (commandData == CommandType.DoorStatus) 
            {
                commandHeader = _appSettings.Microcontroller.CommandHeader + _appSettings.Microcontroller.Commands.DoorStatus.Length + _appSettings.Microcontroller.Commands.DoorStatus.Code;
            }
            else if (commandData == CommandType.ItemDetection)
            {
                commandHeader = _appSettings.Microcontroller.CommandHeader + _appSettings.Microcontroller.Commands.DetectItem.Length + _appSettings.Microcontroller.Commands.DetectItem.Code;
            }
            return "";

        }
    }
}

