using System;

namespace EmployeeBenefitsLibrary.EventStoreIntegration
{
    public class TypeMapSelector : ITypeMapSelector
    {
        public TypeMap For(Type type)
        {
            return new EmployeeTypeMap();
        }
    }
}