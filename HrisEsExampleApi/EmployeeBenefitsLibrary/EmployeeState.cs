using JetBrains.Annotations;

namespace EmployeeBenefitsLibrary
{
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

        [UsedImplicitly]
        public void Apply(SalaryChanged evt)
        {
            Salary = evt.Salary;
        }

        [UsedImplicitly]
        public void Apply(Terminated evt)
        {
            Salary = 0;
            Terminated = true;
        }
    }
}