using System;

namespace EmployeeBenefitsLibrary
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    // Virtual methods for easier testing.
    public class Employee : Aggregate<EmployeeState>
    {
        public Employee()
        : this(new EmployeeState())
        {
        }

        public Employee(EmployeeState state)
        : base(state)
        {

        }

        public virtual void Hire(Guid id, string name, decimal salary)
        {
            if (State.Id != Guid.Empty)
                throw new InvalidOperationException("A person may only be hired once.");
            if (salary <= 0)
                throw new InvalidOperationException("Negative salaries are not allowed.");

            var employeeHired = new EmployeeHired
            {
                EmployeeId = id,
                Name = name,
                Salary = salary,
            };

            Apply(employeeHired);
        }

        public virtual void ChangeSalary(decimal salary, string reason)
        {
            if(State.Terminated)
                throw new InvalidOperationException("This employee has been terminated.");
            if (State.Id == Guid.Empty)
                throw new InvalidOperationException("The employee must be hired.");
            if (salary < 0)
                throw new InvalidOperationException("Negative salaries are not allowed.");

            var salaryChanged = new SalaryChanged
            {
                Salary = salary,
                Reason = reason
            };

            Apply(salaryChanged);
        }

        public virtual void Terminate(string reason)
        {
            if (State.Id == Guid.Empty)
                throw new InvalidOperationException("You cannot fire someone you didn't hire!");

            var terminated = new Terminated
            {
                Reason = reason
            };

            Apply(terminated);
        }
    }
}
