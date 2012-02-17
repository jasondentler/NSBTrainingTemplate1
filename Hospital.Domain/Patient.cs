using System;
using CommonDomain.Core;
using Hospital.Events;

namespace Hospital.Domain
{
    public class Patient : AggregateBase
    {

        private bool _admitted;
        private int? _bed;
        private bool _discharged;
        private string _firstName;
        private string _lastName;

        public Patient(Guid id)
        {
            Id = id;
        }

        public Patient(Guid id, string firstName, string lastName) : this(id)
        {
            RaiseEvent(new PatientCreated()
                           {
                               EventId = Guid.NewGuid(),
                               PatientId = Id,
                               FirstName = firstName,
                               LastName = lastName
                           });
        }

        public void Admit(DateTimeOffset when)
        {
            if (!_admitted)
                RaiseEvent(new PatientAdmitted()
                               {
                                   EventId = Guid.NewGuid(),
                                   PatientId = Id,
                                   When = when,
                                   FirstName = _firstName,
                                   LastName = _lastName
                               });
        }

        public void AssignBed(int bed)
        {
            if (!_admitted)
                throw new ApplicationException("The patient can't be assigned to a bed until admitted");

            if (!_bed.HasValue)
            {
                RaiseEvent(new BedAssigned()
                               {
                                   EventId = Guid.NewGuid(),
                                   PatientId = Id,
                                   Bed = bed
                               });
            }
            else
            {
                RaiseEvent(new PatientMoved()
                               {
                                   EventId = Guid.NewGuid(),
                                   PatientId = Id,
                                   FromBed = _bed.Value,
                                   ToBed = bed
                               });
            }
        }

        public void Discharge()
        {
            if (_discharged)
                return;
            if (!_admitted)
                throw new ApplicationException("The patient can't be discharged without being admitted");

            RaiseEvent(new PatientDischarged()
                           {
                               EventId = Guid.NewGuid(),
                               PatientId = Id,
                               When = DateTimeOffset.UtcNow,
                               Bed = _bed,
                           });
        }

        private void Apply(PatientCreated e)
        {
            _firstName = e.FirstName;
            _lastName = e.LastName;
        }

        private void Apply(PatientAdmitted e)
        {
            _admitted = true;
        }

        private void Apply(BedAssigned e)
        {
            _bed = e.Bed;
        }

        private void Apply(PatientMoved e)
        {
            _bed = e.ToBed;
        }

        private void Apply(PatientDischarged e)
        {
            _discharged = true;
            _admitted = false;
        }

    }
}
