using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Interfaces;

namespace LSS.HCM.Core.Domain.Services
{
    public class CommunicationPortControl : ICommunicationPortControl
    {
        private readonly ILockBoardInitializationCommand _lockBoardInitializationCommand;
        private readonly AppSettings _appSettings;
        private readonly SerialPortControl lockControlCommunicationPort;
        private readonly SerialPortControl ObjectDetectionCommunicationPort;
        private readonly SerialPortControl ScannerCommunicationPort;
        public CommunicationPortControl(ILockBoardInitializationCommand lockBoardInitializationCommand, AppSettings appSettings)
        {
            _lockBoardInitializationCommand = lockBoardInitializationCommand;
            _appSettings = appSettings;
            lockControlCommunicationPort = new SerialPortControl(_appSettings);
            ObjectDetectionCommunicationPort = new SerialPortControl(_appSettings);
            ScannerCommunicationPort = new SerialPortControl(_appSettings);
        }
        public string SendCommand(string commandName, string commandData)
        {
            var commandString = _lockBoardInitializationCommand.GenerateCommandBuffer(commandName, commandData.ToString());
            SerialPortControl controlModule;
            string responseData = string.Empty;
            string result, compiledData;            
            if (commandName == CommandType.DoorOpen)
            {
                controlModule = lockControlCommunicationPort;
                result = controlModule.WriteAndWait(commandString, _appSettings.Microcontroller.Commands.OpenDoor.ResLen);
                compiledData = _lockBoardInitializationCommand.CompiledCommandResponse(result);
                responseData = _lockBoardInitializationCommand.ExecuteHexData(CommandType.DoorOpen, compiledData);
            }
            else if (commandName == CommandType.DoorStatus)
            {
                controlModule = lockControlCommunicationPort;
                result = controlModule.WriteAndWait(commandString, _appSettings.Microcontroller.Commands.DoorStatus.ResLen);
                compiledData = _lockBoardInitializationCommand.CompiledCommandResponse(result);
                responseData = _lockBoardInitializationCommand.ExecuteHexData(CommandType.DoorStatus, compiledData);
            }
            else if (commandName == CommandType.ItemDetection)
            {
                controlModule = ObjectDetectionCommunicationPort;
                result = controlModule.WriteAndWait(commandString, _appSettings.Microcontroller.Commands.DetectItem.ResLen);
                compiledData = _lockBoardInitializationCommand.CompiledCommandResponse(result);
                responseData = _lockBoardInitializationCommand.ExecuteHexData(CommandType.ItemDetection, compiledData);
            }
            return responseData;
        }
    }
}
