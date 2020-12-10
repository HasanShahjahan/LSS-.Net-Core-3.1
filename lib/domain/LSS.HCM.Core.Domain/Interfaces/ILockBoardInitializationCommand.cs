using System;

namespace LSS.HCM.Core.Domain.Interfaces
{
    public interface ILockBoardInitializationCommand
    {
        string ExecuteHexData(string commandType, string inputData);
        string CompiledCommandResponse(string bufferResponse);
        string GenerateCommandBuffer(string commandName, string commandData);
    }
}
