using LSS.HCM.Core.Common.Extensions;

namespace LSS.HCM.Core.Common.Enums
{
    public sealed class PayloadTypes : StringEnum
    {
        public PayloadTypes(string value) : base(value)
        {
        }
        public const string OpenCompartment = "OpenCompartment";
        public const string CompartmentStatus = "CompartmentStatus";
        public const string LockerStatus = "LockerStatus";
        public const string CaptureImage = "CaptureImage";
    }
}
