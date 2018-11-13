using System;

namespace EventStoreIntegration
{
    public class TypeMapSelector : ITypeMapSelector
    {
        public TypeMap For(Type type)
        {
            return new EmployeeTypeMap();
        }
    }
}