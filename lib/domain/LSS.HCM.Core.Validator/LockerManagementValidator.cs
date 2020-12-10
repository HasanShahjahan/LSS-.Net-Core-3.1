using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Common.Exceptions;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Infrastructure.Repository;
using LSS.HCM.Core.Security.Handlers;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Linq;
using ApplicationException = LSS.HCM.Core.Common.Exceptions.ApplicationException;

namespace LSS.HCM.Core.Validator
{
    public class LockerManagementValidator : ILockerManagementValidator
    {
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly IRepository<LockerConfiguration> _repository;
        private readonly AppSettings _appSettings;
        public LockerManagementValidator(IJwtTokenHandler jwtTokenHandler, IRepository<LockerConfiguration> repository, AppSettings appSettings)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _repository = repository;
            _appSettings = appSettings;
        }
        public (int, ApplicationException) PayloadValidator(string accessToken, string payloadType, string lockerId, string transactionId, string[] compartmentIds, string captureType)
        {
            int statusCode = StatusCodes.Status200OK;
            ApplicationException result = null;
            LockerConfiguration lockerConfiguration = null;
            if (_appSettings.Api.JsonWebTokens.IsEnabled)
            {
                var token = _jwtTokenHandler.PrepareTokenFromAccessToekn(accessToken);
                if (string.IsNullOrEmpty(token))
                {
                    statusCode = StatusCodes.Status401Unauthorized;
                    result = new ApplicationException { ErrorCode = ApplicationErrorCodes.InvalidToken, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidToken) } };
                    return (statusCode, result);
                }
                var (isVerified, transactionid) = _jwtTokenHandler.VerifyJwtSecurityToken(token);
                if ((!isVerified) || string.IsNullOrEmpty(transactionid))
                {
                    statusCode = StatusCodes.Status401Unauthorized;
                    result = new ApplicationException { ErrorCode = ApplicationErrorCodes.InvalidToken, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidToken) } };
                    return (statusCode, result);
                }
            }
            switch (payloadType)
            {
                case PayloadTypes.OpenCompartment:

                    if (string.IsNullOrEmpty(transactionId))
                    {
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        result = new ApplicationException { ErrorCode = ApplicationErrorCodes.EmptyTransactionId, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyTransactionId) } };
                    }
                    else if (string.IsNullOrEmpty(lockerId))
                    {
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        result = new ApplicationException { ErrorCode = ApplicationErrorCodes.EmptyLockerId, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId) } };
                    }
                    else if (compartmentIds == null || compartmentIds.Length == 0)
                    {
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        result = new ApplicationException { ErrorCode = ApplicationErrorCodes.EmptyCompartmentId, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyCompartmentId) } };
                    }
                    else if (compartmentIds.Length > 0)
                    {
                        lockerConfiguration = _repository.Get().Find(configuration => configuration.Id == _appSettings.LockerStationId).FirstOrDefault();
                        if (lockerConfiguration != null && lockerConfiguration.LockerId != lockerId)
                        {
                            statusCode = StatusCodes.Status422UnprocessableEntity;
                            result = new ApplicationException { ErrorCode = ApplicationErrorCodes.InvalidLockerId, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId) } };
                        }
                        if (lockerConfiguration != null && lockerConfiguration.Compartments.Count() > 0)
                        {
                            foreach (string compartmentId in compartmentIds)
                            {
                                var flag = lockerConfiguration.Compartments.Any(com => com.CompartmentId == compartmentId);
                                if (flag) continue;
                                else {
                                    statusCode = StatusCodes.Status422UnprocessableEntity;
                                    result = new ApplicationException { ErrorCode = ApplicationErrorCodes.InvalidCompartmentId, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidCompartmentId) } };
                                }
                            }
                            
                        }
                    }
                    return (statusCode, result);
                    
                case PayloadTypes.CompartmentStatus:

                    if (string.IsNullOrEmpty(lockerId))
                    {
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        result = new ApplicationException { ErrorCode = ApplicationErrorCodes.EmptyLockerId, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId) } };
                    }
                    lockerConfiguration = _repository.Get().Find(configuration => configuration.Id == _appSettings.LockerStationId).FirstOrDefault();
                    if (lockerConfiguration != null && lockerConfiguration.LockerId != lockerId)
                    {
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        result = new ApplicationException { ErrorCode = ApplicationErrorCodes.InvalidLockerId, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId) } };
                    }
                    return (statusCode, result);

                case PayloadTypes.LockerStatus:

                    if (string.IsNullOrEmpty(lockerId))
                    {
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        result = new ApplicationException { ErrorCode = ApplicationErrorCodes.EmptyLockerId, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId) } };
                    }
                    lockerConfiguration = _repository.Get().Find(configuration => configuration.Id == _appSettings.LockerStationId).FirstOrDefault();
                    if (lockerConfiguration != null && lockerConfiguration.LockerId != lockerId)
                    {
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        result = new ApplicationException { ErrorCode = ApplicationErrorCodes.InvalidLockerId, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId) } };
                    }
                    return (statusCode, result);

                case PayloadTypes.CaptureImage:

                    lockerConfiguration = _repository.Get().Find(configuration => configuration.Id == _appSettings.LockerStationId).FirstOrDefault();
                    if (string.IsNullOrEmpty(lockerId))
                    {
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        result = new ApplicationException { ErrorCode = ApplicationErrorCodes.EmptyLockerId, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId) } };
                    }
                    else if (lockerConfiguration != null && lockerConfiguration.LockerId != lockerId)
                    {
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        result = new ApplicationException { ErrorCode = ApplicationErrorCodes.InvalidLockerId, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId) } };
                    }
                    else if (string.IsNullOrEmpty(captureType))
                    {
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        result = new ApplicationException { ErrorCode = ApplicationErrorCodes.EmptyCaptureType, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyCaptureType) } };
                    }
                    else if (!(captureType == CaptureType.Photo || captureType == CaptureType.Screen)) 
                    {
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        result = new ApplicationException { ErrorCode = ApplicationErrorCodes.InvalidCaptureType, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidCaptureType) } };
                    }
                    else if (string.IsNullOrEmpty(transactionId))
                    {
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        result = new ApplicationException { ErrorCode = ApplicationErrorCodes.EmptyTransactionId, Data = new ErrorData() { ValidFormat = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyTransactionId) } };
                    }
                    return (statusCode, result);

            }
            return (statusCode, result);
        }
    }
}
