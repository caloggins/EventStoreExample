namespace EmployeeBenefitsLibrary.EventStoreIntegration
{
    public class EmployeeTypeMap : TypeMap
    {
        public EmployeeTypeMap()
        {
            Add((typeof(EmployeeHired).Name, typeof(EmployeeHired)));
            Add((typeof(SalaryChanged).Name, typeof(SalaryChanged)));
        }
    }
}