using System;
using System.Collections.Generic;

namespace EmployeeBenefitsLibrary
{
    public abstract class Aggregate
    {
        private readonly List<Event> changes = new List<Event>();

        protected Aggregate(State state)
        {
            State = state;
        }

        public Guid Id => State.Id;
        public string StreamName => GetType().Name;
        public long Version => State.Version;

        // this a test seam, use at your own risk.
        public State State { get; }

        public IEnumerable<Event> NewEvents => changes.AsReadOnly();

        public void LoadHistory(IEnumerable<Event> events)
        {
            foreach (var evt in events)
                Apply(evt, false);
        }

        protected void Apply(Event evt)
        {
            Apply(evt, true);
        }

        private void Apply(Event evt, bool eventIsNew)
        {
            if (eventIsNew)
                changes.Add(evt);

            State.ApplyEvent(evt);
        }
    }
}