using LSS.HCM.Core.Common.Extensions;
using System;

namespace LSS.HCM.Core.Common.Exceptions 
{
    public sealed class ApplicationErrorCodes : StringEnum
    {
        public ApplicationErrorCodes(string value) : base(value)
        {
        }
        
        public const string EmptyTransactionId = "EMPTY_TRANSACTION_ID";
        public const string EmptyLockerId = "EMPTY_LOCKER_ID";
        public const string EmptyCompartmentId = "EMPTY_COMPARTMENT_ID";
        public const string EmptyCaptureType = "EMPTY_IMAGE_CAPTURE_TYPE";

        public const string InvalidToken = "INVALID_JWT_TOKEN";
        public const string InvalidCompartmentId= "INVALID_COMPARTMENT_ID";
        public const string InvalidLockerId = "INVALID_LOCKER_ID";
        public const string InvalidCaptureType = "INVALID_IMAGE_CAPTURE_TYPE";
        public static string GetMessage(string value)
        {
            switch (value)
            {
                case InvalidToken:
                    return "Specify Valid JWT Token";
                case EmptyTransactionId:
                    return "Specify Valid Transaction Id";
                case EmptyLockerId:
                    return "Specify Valid Locker Id";
                case EmptyCompartmentId:
                    return "Compartment Id is Required";
                case InvalidCompartmentId:
                    return "Specify Valid Compartment Id's";
                case InvalidLockerId:
                    return "Specify Valid Locker Id";
                case EmptyCaptureType:
                    return "Capture Type is Required";
                case InvalidCaptureType:
                    return "Specify Valid Capture Type";
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}
