using LSS.HCM.Core.Common.Exceptions;

namespace LSS.HCM.Core.Validator
{
    public interface ILockerManagementValidator
    {
        (int, ApplicationException) PayloadValidator(string accessToken, string payloadType, string lockerId, string transactionId, string[] compartmentIds, string captureType);
    }
}
