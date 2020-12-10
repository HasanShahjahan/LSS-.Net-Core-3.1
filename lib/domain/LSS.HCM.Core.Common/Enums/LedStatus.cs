using LSS.HCM.Core.Common.Extensions;

namespace LSS.HCM.Core.Common.Enums
{
    public sealed class LedStatus : StringEnum
    {
        public LedStatus(string value) : base(value)
        {
        }
        public const string On = "ON";
        public const string Off = "OFF";
        public const string Blinking= "BLINK ";
    }
}
