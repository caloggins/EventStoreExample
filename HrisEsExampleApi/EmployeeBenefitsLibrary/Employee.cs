using System;
using JetBrains.Annotations;

namespace EmployeeBenefitsLibrary
{
    public class Employee : Aggregate
    {
        public Employee()
        : this(new EmployeeState())
        {
        }

        public Employee(State state)
        : base(state)
        {
        }

        public virtual void Hire(Guid id, string name, decimal salary)
        {
            if(State.Id != Guid.Empty)
                throw new InvalidOperationException("A person may only be hired once.");

            var evt = new EmployeeHired
            {
                EmployeeId = id,
                Name = name,
                Salary = salary,
            };

            Apply(evt);
        }

    }

    public class EmployeeState : State
    {
        public string Name;
        public decimal Salary;
        public bool Terminated;

        [UsedImplicitly]
        public void Apply(EmployeeHired evt)
        {
            Id = evt.EmployeeId;
            Name = evt.Name;
            Salary = evt.Salary;
        }
    }

    public class EmployeeHired : Event
    {
        public string Name;
        public decimal Salary;
        public Guid EmployeeId;
    }
}
