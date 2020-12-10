using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Interfaces;
using MQTTnet.Client;
using System;

namespace LSS.HCM.Core.Domain.Services
{
    public class MqttHandler : IMqttHandler
    {
        private readonly AppSettings _appSettings;
        public MqttHandler(AppSettings appSettings)
        {
            _appSettings = appSettings;            
        }
        public void Scanner()
        {
            throw new NotImplementedException();
        }

        public void Ups()
        {
            throw new NotImplementedException();
        }

        public void Led()
        {
            throw new NotImplementedException();
        }
    }
}
