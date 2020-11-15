using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
namespace invoiceElahe
{
    public class InvoiceRepository
    {
        DbProviderFactory factory;
       // factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        //DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
 
        string provider;
        string connectionString;

        public InvoiceRepository()
        {
            provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            factory = DbProviderFactories.GetFactory(provider);

            DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);

            //for Connection
            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection connection = factory.CreateConnection();
        }

        public List<Invoices> GetAll()
        {
            var invoices = new List<Invoices>();
            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select * From Employees;";
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invoices.Add(new Invoices
                        {
                            PurchaseNumber = (int)reader["PurchaseNumber"],
                            Name = (string)reader["Name"],
                            FamilyName = (string)reader["FamilyName"],
                            MobileNumber = (string)reader["MobileNumber"],
                            Amount = (string)reader["Amount"],
                            Scan = (string)reader["Scan"]
                        });
                    }
                }
            }

            return invoices;
        }
    }
}