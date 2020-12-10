using LSS.HCM.Core.Api;
using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Common.Exceptions;
using LSS.HCM.Core.Validator;
using LSS.HCM.UnitTest.DependencyResolver.Resolver;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Xunit;

namespace HCM.Core.Validator.UnitTest
{
    public class PayloadValidatorUnitTest
    {
        private readonly DependencyResolverHelper _serviceProvider;
        private readonly string accessToken = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2MDkzNTU5MjEsInRyYW5zYWN0aW9uX2lkIjoiNzBiMzZjNDEtMDc4Yi00MTFiLTk4MmMtYzViNzc0YWFjNjZmIn0.ujOkQJUq5WY_tZJgKXqe_n4nql3cSAeHMfXGABZO3E4";
        private readonly string transactionId = "70b36c41-078b-411b-982c-c5b774aac66f";
        private readonly string lockerId = "PANLOCKER-1";
        private readonly string[] validCompartmentIds = { "M0-1", "M0-2","M0-3"};
        private readonly string[] invalidCompartmentIds = { "S0-1"};
        private readonly string captureType = "Photo";
        public PayloadValidatorUnitTest()
        {
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
        }

        #region Open Compartment
        [Fact]
        public void OpenCompartmentEmptyTransactionId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var(statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.OpenCompartment, lockerId, string.Empty,null,null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyTransactionId), errorResult.Data.ValidFormat);
        }

        [Fact]
        public void OpenCompartmentEmptyLockerId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.OpenCompartment, string.Empty, transactionId, null, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId), errorResult.Data.ValidFormat);
        }

        [Fact]
        public void OpenCompartmentInvalidLockerId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.OpenCompartment, "Hasan", transactionId, validCompartmentIds, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId), errorResult.Data.ValidFormat);
        }

        [Fact]
        public void OpenCompartmentValidLockerId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.OpenCompartment, lockerId, transactionId, validCompartmentIds, null);
            Assert.Equal(200, statusCode);
        }

        [Fact]
        public void OpenCompartmentEmptyCompartmentIds()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.OpenCompartment, lockerId, transactionId, null, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyCompartmentId), errorResult.Data.ValidFormat);
        }

        [Fact]
        public void OpenCompartmentInvalidCompartmentIds()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.OpenCompartment, lockerId, transactionId, invalidCompartmentIds, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidCompartmentId), errorResult.Data.ValidFormat);
        }

        [Fact]
        public void OpenCompartmentValidCompartmentIds()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.OpenCompartment, lockerId, transactionId, validCompartmentIds, null);
            Assert.Equal(200, statusCode);
        }

        #endregion

        #region Compartment Status
        [Fact]
        public void CompartmentStatusEmptyLockerId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.CompartmentStatus, string.Empty, transactionId, null, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId), errorResult.Data.ValidFormat);
        }

        [Fact]
        public void CompartmentStatusInvalidLockerId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.CompartmentStatus, "Hasan", transactionId, validCompartmentIds, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId), errorResult.Data.ValidFormat);
        }

        [Fact]
        public void CompartmentStatusValidLockerId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.CompartmentStatus, lockerId, transactionId, validCompartmentIds, null);
            Assert.Equal(200, statusCode);
        }
        #endregion

        #region Locker Status
        [Fact]
        public void LockerStatusEmptyLockerId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.LockerStatus, string.Empty, transactionId, null, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId), errorResult.Data.ValidFormat);
        }

        [Fact]
        public void LockerStatusInvalidLockerId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.LockerStatus, "Hasan", transactionId, validCompartmentIds, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId), errorResult.Data.ValidFormat);
        }

        [Fact]
        public void LockerStatusValidLockerId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.LockerStatus, lockerId, transactionId, validCompartmentIds, null);
            Assert.Equal(200, statusCode);
        }
        #endregion

        #region Capture Image
        [Fact]
        public void CaptureImageEmptyLockerId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.CaptureImage, string.Empty, transactionId, null, captureType);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId), errorResult.Data.ValidFormat);
        }

        [Fact]
        public void CaptureImageInvalidLockerId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.CaptureImage, "Hasan", transactionId, null, captureType);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId), errorResult.Data.ValidFormat);
        }

        [Fact]
        public void CaptureImageValidLockerId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.CaptureImage, lockerId, transactionId, null, captureType);
            Assert.Equal(200, statusCode);
        }

        [Fact]
        public void CaptureImageEmptyCaptureType()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.CaptureImage, lockerId, transactionId, null, string.Empty);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyCaptureType), errorResult.Data.ValidFormat);
        }

        [Fact]
        public void CaptureImageInvalidCaptureType()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.CaptureImage, lockerId, transactionId, null, "Hasan");
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidCaptureType), errorResult.Data.ValidFormat);
        }

        [Fact]
        public void CaptureImageValidCaptureType()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.CaptureImage, lockerId, transactionId, null, captureType);
            Assert.Equal(200,statusCode);
        }

        [Fact]
        public void CaptureImageEmptyTransactionId()
        {
            var _lockerManagementValidator = _serviceProvider.GetService<ILockerManagementValidator>();
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(accessToken, PayloadTypes.CaptureImage, lockerId, string.Empty, null, captureType);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyTransactionId), errorResult.Data.ValidFormat);
        }
        #endregion
    }
}
