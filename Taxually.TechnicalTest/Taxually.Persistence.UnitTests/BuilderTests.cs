using Taxually.Persistence.Builders;
using Taxually.Persistence.Resolvers;

namespace Taxually.Persistence.UnitTests
{
    public class ResolverBuilderTests
    {
        [Fact]
        public void TestResolveBuilder_1()
        {
            var builder = new ResolverBuilder();

            var result = builder.Build("DE");

            Assert.IsType<QueueXmlResolver>(result);
        }

        [Fact]
        public void TestResolveBuilder_2()
        {
            var builder = new ResolverBuilder();

            var result = builder.Build("FR");

            Assert.IsType<QueueCsvResolver>(result);
        }

        [Fact]
        public void TestResolveBuilder_3()
        {
            var builder = new ResolverBuilder();

            var result = builder.Build("GB");

            Assert.IsType<HttpResolver>(result);
        }


        [Fact]
        public void TestResolveBuilder_4()
        {
            var builder = new ResolverBuilder();

            Assert.Throws<Exception>(() => builder.Build("HU"));
        }
    }
}