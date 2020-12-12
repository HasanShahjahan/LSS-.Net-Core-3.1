using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Interfaces;
using LSS.HCM.Core.Entities.Locker;
using LSS.HCM.Core.Infrastructure.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using Compartment = LSS.HCM.Core.Entities.Locker.Compartment;

namespace LSS.HCM.Core.Domain.Managers
{
    public class LockerManagement : ILockerManagement
    {
        private readonly IRepository<LockerConfiguration> _repository;
        private readonly AppSettings _appSettings;        
        private readonly ICompartmentManagement _compartmentManagement;
        public LockerManagement(IRepository<LockerConfiguration> repository, AppSettings appSettings, ICommunicationPortControl communicationPortControl, ICompartmentManagement compartmentManagement) 
        {
            _repository = repository;
            _appSettings = appSettings;            
            _compartmentManagement = compartmentManagement;
        }
        public Locker OpenCompartment(OpenCompartmentDto openCompartmentDto)
        {
            var lockerResult = new Locker();
            foreach (var compartmentId in openCompartmentDto.CompartmentIds)
            {
                Compartment targetCompartment = _compartmentManagement.DoorOpen(compartmentId);
                lockerResult.Compartments.Add(targetCompartment);
            }
            return new Locker();
        }
        public List<Entities.Locker.Compartment> CompartmentStatus()
        {
            throw new NotImplementedException();
        }

        public Compartments LockerStatus(string lockerId)
        {
            var lockerConfiguration = _repository.Get().Find(configuration => configuration.Id == _appSettings.LockerStationId).FirstOrDefault();
            List<string> list = new List<string>();
            foreach (var compartment in lockerConfiguration.Compartments) list.Add(compartment.CompartmentId);
            return new Compartments { LockerId = lockerId, CompartmentIds = list.ToArray() };
        }

        public Capture CaptureImage(string lockerId, string type)
        {
            throw new NotImplementedException();
        }
    }
}
