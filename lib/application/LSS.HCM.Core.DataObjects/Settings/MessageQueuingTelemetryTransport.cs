namespace LSS.HCM.Core.DataObjects.Settings
{
    public class MessageQueuingTelemetryTransport
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Topic Topic { get; set; } 

    }
}
