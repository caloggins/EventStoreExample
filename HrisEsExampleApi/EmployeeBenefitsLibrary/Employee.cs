using System;

namespace EmployeeBenefitsLibrary
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    // Virtual methods for easier testing.
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
            if(salary <= 0)
                throw new InvalidOperationException("Negative salaries are not allowed.");

            var evt = new EmployeeHired
            {
                EmployeeId = id,
                Name = name,
                Salary = salary,
            };

            Apply(evt);
        }


        public virtual void ChangeSalary(decimal salary, string reason)
        {
            if(State.Id == Guid.Empty)
                throw new InvalidOperationException("The employee must be hired.");
            if(salary < 0)
                throw new InvalidOperationException("Negative salaries are not allowed.");

            var evt = new SalaryChanged
            {
                Salary = salary,
                Reason = reason
            };

            Apply(evt);
        }
    }
}
