using System;
using System.Collections.Generic;

namespace EmployeeBenefitsLibrary
{
    public interface IAggregate
    {
        Guid Id { get; }

        string StreamName { get; }

        IEnumerable<Event> NewEvents { get; }

        void LoadHistory(IEnumerable<Event> events);
    }
}