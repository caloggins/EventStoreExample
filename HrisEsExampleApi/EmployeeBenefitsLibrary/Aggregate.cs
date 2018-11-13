using System;
using System.Collections.Generic;

namespace EmployeeBenefitsLibrary
{
    public abstract class Aggregate<TState> : IAggregate
        where TState : State
    {
        private readonly List<Event> changes = new List<Event>();

        protected Aggregate(TState state)
        {
            State = state;
        }


        public IEnumerable<Event> NewEvents => changes.AsReadOnly();
        public string StreamName => GetType().Name;

        public Guid Id => State.Id;
        public long Version => State.Version;

        // this a test seam, use at your own risk.
        public TState State { get; }

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