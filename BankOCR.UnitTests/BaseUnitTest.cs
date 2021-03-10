using NUnit.Framework;

namespace BankOcr.UnitTests
{
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycleAttribute(LifeCycle.InstancePerTestCase)]
    public abstract class BaseUnitTest
    {
    }
    
    public abstract class BaseUnitTest<TSut> : BaseUnitTest
        where TSut : class
    {
        private readonly NSubstituteAutoMocker.Standard.NSubstituteAutoMocker<TSut> _mocker = new();

        public virtual TSut Sut => _mocker.ClassUnderTest;

        public T Mock<T>() where T : class => _mocker.Get<T>();
    }
}