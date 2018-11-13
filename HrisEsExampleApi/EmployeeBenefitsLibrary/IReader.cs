using System;
using System.Threading.Tasks;

namespace EmployeeBenefitsLibrary
{
    public interface IReader
    {
        Task<TAggregate> Read<TAggregate>(Guid id)
            where TAggregate : IAggregate, new();
    }
}