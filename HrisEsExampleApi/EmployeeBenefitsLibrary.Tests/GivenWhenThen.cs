namespace EmployeeBenefitsLibrary.Tests
{
    public abstract class GivenWhenThen
    {
        protected GivenWhenThen()
        {
            Given();
            When();
        }

        protected abstract void Given();

        protected abstract void When();
    }
}