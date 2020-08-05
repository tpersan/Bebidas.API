using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Dac;

namespace Bebidas.Tests
{

    public class LocalDB
    {
        public string NomeDaBase { get; private set; }

        public LocalDB(string nomeDaBase)
        {
            NomeDaBase = nomeDaBase;
        }


        public void CriarLocalDB(bool sempreRecriar = false)
        {
            if (sempreRecriar)
                DestruirLocalDB();

            var connectionStringSetup = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True";

            var diretorio = Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName);
            var dacPacFilePath = Path.Combine(diretorio, "script\\Bebidas.BD.dacpac");



            var dacService = new DacServices(connectionStringSetup);
            var options = new DacDeployOptions() { CreateNewDatabase = true };

            using (var dacPac = DacPackage.Load(dacPacFilePath))
            {
                dacService.Deploy(dacPac, this.NomeDaBase, true, options);
            }
        }

        public void DestruirLocalDB()
        {
            var connectionStringSetup = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True";

            using (var conn = new SqlConnection(connectionStringSetup))
            {
                conn.Open();

                var command = conn.CreateCommand();

                command.CommandText = $@"
                USE [MASTER];

                IF (DB_ID('{NomeDaBase}') IS NOT NULL) BEGIN
                    DECLARE @KILL VARCHAR(8000) = '';  

                    SELECT @KILL = @KILL + 'KILL ' + CONVERT(VARCHAR(5), SESSION_ID) + ';'  
                    FROM SYS.DM_EXEC_SESSIONS
                    WHERE DATABASE_ID  = DB_ID('{NomeDaBase}')

                    EXEC(@KILL);

                    DROP DATABASE {NomeDaBase}
                END";

                command.ExecuteNonQuery();

                conn.Close();
            }
        }

    }
}
