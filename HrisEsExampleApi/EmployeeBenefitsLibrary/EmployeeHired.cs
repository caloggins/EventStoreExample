using System;

namespace EmployeeBenefitsLibrary
{
    public class EmployeeHired : Event
    {
        public string Name;
        public decimal Salary;
        public Guid EmployeeId;
    }
}