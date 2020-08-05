namespace Bebidas.Tests
{

    public class LocalWorkshop : LocalDB
    {
        public LocalWorkshop(string nomeDaBase) : base(nomeDaBase) { }

        public void ExecutarCriacao()
        {
            var localDB = new LocalDB(this.NomeDaBase);
            localDB.CriarLocalDB(true);
        }
    }
}
