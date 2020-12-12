using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.Domain.Interfaces;
using LSS.HCM.Core.Validator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace LSS.HCM.Core.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LockerController : ControllerBase
    {
        private readonly ILockerManagement _lockerManagement;
        private readonly ILockerManagementValidator _lockerManagementValidator;
        private readonly ILogger<LockerController> _logger;
        public LockerController(ILockerManagement lockerManagement, ILockerManagementValidator lockerManagementValidator, ILogger<LockerController> logger)
        {
            _lockerManagement = lockerManagement;
            _lockerManagementValidator = lockerManagementValidator;
            _logger = logger;
        }

        [HttpPost("{locker_id}/compartments/open")]
        public IActionResult OpenCompartment([FromBody] OpenCompartmentDto request)
        {
            var lockerId = (Request.Path.Value.Split('/'))[2];
            _logger.LogInformation("[Open Compartment] " + "[" + request.TransactionId + "]" + " Locker Id: " + lockerId + " Request: " + JsonConvert.SerializeObject(request));
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(Request.Headers[HeaderNames.Authorization], PayloadTypes.OpenCompartment, lockerId, request.TransactionId, request.CompartmentIds, string.Empty);
            //_logger.LogInformation("[Open Compartment]" + "[" + request.TransactionId + "]" + " Locker Id: " + lockerId + " Status Code: " + statusCode + " Error Result : " + errorResult.Data.ValidFormat + " Access Token : " + Request.Headers[HeaderNames.Authorization]);
            if (statusCode != StatusCodes.Status200OK) return StatusCode(statusCode, errorResult);
            var result = _lockerManagement.OpenCompartment(request);
            _logger.LogInformation("[Open Compartment]" + "[" + request.TransactionId + "]" + " Locker Id: " + lockerId + "[Response]" + JsonConvert.SerializeObject(result));
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("{locker_id}/compartment/statuses")]
        public IActionResult CompartmentStatus()
        {
            var lockerId = (Request.Path.Value.Split('/'))[2];
            var result = _lockerManagement.CompartmentStatus();
            return StatusCode(StatusCodes.Status200OK, result);

        }

        [HttpGet("{locker_id}/status")]
        public IActionResult LockerStatus()
        {
            var lockerId = (Request.Path.Value.Split('/'))[2];
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(Request.Headers[HeaderNames.Authorization], PayloadTypes.LockerStatus, lockerId, string.Empty, null, string.Empty);
            if (statusCode != StatusCodes.Status200OK) return StatusCode(statusCode, errorResult);
            var result = _lockerManagement.LockerStatus(lockerId);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost("{locker_id}/capture/{type}")]
        public IActionResult CaptureImage(CaptureImageDto request)
        {
            var lockerId = (Request.Path.Value.Split('/'))[2];
            var captureType = (Request.Path.Value.Split('/'))[4];
            var (statusCode, errorResult) = _lockerManagementValidator.PayloadValidator(Request.Headers[HeaderNames.Authorization], PayloadTypes.CaptureImage, lockerId, request.TransactionId, null, captureType);
            if (statusCode != StatusCodes.Status200OK) return StatusCode(statusCode, errorResult);
            var result = _lockerManagement.CaptureImage(lockerId, captureType);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
