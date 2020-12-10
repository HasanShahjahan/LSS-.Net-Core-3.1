using LSS.HCM.Core.Common.Extensions;

namespace LSS.HCM.Core.Common.Enums
{
    public sealed class CaptureType : StringEnum
    {
        public CaptureType(string value) : base(value)
        {
        }
        public const string Photo = "Photo";
        public const string Screen = "Screen";
    }
}
