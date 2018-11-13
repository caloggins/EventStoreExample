using System;

namespace EmployeeBenefitsLibrary
{
    public class Event
    {
        // note, there is a subtle issue where loaded events have new ids
        public Guid Id { get; } = Guid.NewGuid();
    }
}