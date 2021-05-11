using System;
using System.Collections.Generic;

namespace LSS.HCM.Core.Domain.Interfaces
{
    public interface ILockBoardInitializationCommand
    {
        Dictionary<string, string> ExecuteCommandResponse(string commandType, List<byte> bufferResponse);
        List<byte> GenerateCommandBuffer(string commandName, List<byte> commandData);
    }
}
