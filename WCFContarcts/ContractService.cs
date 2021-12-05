using System;
using System.Data.SqlClient;
using System.IO;

namespace WCFContarcts
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ContractService" в коде и файле конфигурации.
    public class ContractService : IContractService
    {
        SqlConnection sqlConnection = new SqlConnection(connectionString);

        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Malik\source\repos\WCFContarcts\WCFContarcts\Database1.mdf;Integrated Security=True";
        private string cmdTextLoad;
        private string cmdTextSave;
        private string PATH = $"{Environment.CurrentDirectory}\\myJson.json";

        public string LoadBase()
        {
            sqlConnection.Open();

            cmdTextLoad = "SELECT * FROM Test_Table for json path";

            SqlCommand sqlCommandLoad = new SqlCommand(cmdTextLoad, sqlConnection);

            string getValue = sqlCommandLoad?.ExecuteScalar()?.ToString() ?? "[{\"NamberContract\":1}]";

            string valueStr = Сrutch(getValue);

            using (StreamWriter writer = File.CreateText(PATH))
            {
                //запись строчки в файл
                writer.Write(valueStr);
            }

            sqlConnection.Close();

            return valueStr;
        }

        public void SaveBase(string str)
        {
            sqlConnection.Open();

            cmdTextSave = $"truncate table Test_Table DECLARE @json VARCHAR(MAX) select @json = BulkColumn FROM OPENROWSET(BULK'{PATH}', SINGLE_BLOB) JSON; insert into Test_Table select* from openjson(@json) with(DataContract date, NamberContract nvarchar(50), DateAppCon date)";

            SqlCommand sqlCommandSave = new SqlCommand(cmdTextSave, sqlConnection);

            sqlCommandSave.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public string Сrutch(string crutch)
        {
            crutch = crutch.Replace(@"\", "");

            crutch = crutch.Remove(crutch.Length - 1);

            crutch = crutch.Remove(0, 1);

            crutch = crutch.Insert(0, "[");

            crutch = crutch.Insert(crutch.Length, "]");

            return crutch;
        }
    }
}
