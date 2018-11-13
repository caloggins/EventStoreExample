using System.Threading.Tasks;

namespace EmployeeBenefitsLibrary
{
    public interface IWriter
    {
        Task Write<TAggregate>(TAggregate aggregate) where TAggregate : IAggregate;
    }
}