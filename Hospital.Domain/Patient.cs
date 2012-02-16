using System;
using CommonDomain.Core;
using Hospital.Events;

namespace Hospital.Domain
{
    public class Patient : AggregateBase 
    {

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

        private void Apply(PatientCreated e)
        {
        }


    }
}
