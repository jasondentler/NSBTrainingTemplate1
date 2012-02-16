using System;
using Hospital.Commands;
using NServiceBus;

namespace Hospital.CommandHandlers
{
    public class AssignBedHandler : IHandleMessages<AssignBed>
    {
        public void Handle(AssignBed message)
        {
        }
    }
}
