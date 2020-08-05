using Xunit;

namespace Bebidas.Tests
{
    [CollectionDefinition("Repositorio Collection")]
    public class RepositorioCollection : ICollectionFixture<BancoFixture> { }
}
