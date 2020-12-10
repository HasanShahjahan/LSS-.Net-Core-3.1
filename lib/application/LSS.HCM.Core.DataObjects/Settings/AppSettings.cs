namespace LSS.HCM.Core.DataObjects.Settings
{
    public class AppSettings
    {
        public DatabaseSettings DatabaseSettings { get; set; }
        public MessageQueuingTelemetryTransport Mqtt { get; set; }
        public Microcontroller Microcontroller { get; set; }
        public ApiConfiguration Api { get; set; }
        public string LockerStationId {get;set;}
        public LockerConfiguration Locker { get; set; } 
    }
}
