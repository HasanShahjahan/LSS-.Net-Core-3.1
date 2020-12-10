using LSS.HCM.Core.Common.Extensions;

namespace LSS.HCM.Core.Common.Enums
{
    public sealed class CommandType : StringEnum
    {
        public CommandType(string value) : base(value)
        {
        }
        public const string DoorOpen= "DoorOpen";
        public const string ItemDetection= "ItemDetection";
        public const string DoorStatus = "DoorStatus";
    }
}
