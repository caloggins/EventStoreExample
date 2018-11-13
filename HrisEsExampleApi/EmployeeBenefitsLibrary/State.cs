using System;

namespace EmployeeBenefitsLibrary
{
    public abstract class State
    {
        // note, need to include this in the tests...
        public long Version { get; set; }
        public Guid Id;

        // ReSharper disable once VirtualMemberNeverOverridden.Global
        public virtual void ApplyEvent(Event evt)
        {
            ((dynamic)this).Apply((dynamic)evt);
            Version++;
        }
    }
}