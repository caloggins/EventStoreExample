using System;

namespace EventStoreIntegration
{
    public interface ITypeMapSelector
    {
        // ReSharper disable once UnusedParameter.Global
        TypeMap For(Type type);
    }
}