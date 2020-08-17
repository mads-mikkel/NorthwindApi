using NorthwindApi.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NorthwindApi.DataAccess
{
    public class Repository
    {
        const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Invoice> GetAllInvoices(string customerID)
        {
            string sql = $"SELECT CustomerName, ExtendedPrice, Freight FROM Invoices WHERE CustomerID LIKE '{customerID}'";
            DataRowCollection dataRows = Execute(sql);
            List<Invoice> invoices = ProcessInvoices(dataRows);
            return invoices;
        }

        public List<Customer> GetAllCustomers()
        {
            string sql = "SELECT DISTINCT CustomerID FROM Customers";
            DataRowCollection dataRows = Execute(sql);
            List<Customer> customers = ProcessCustomers(dataRows);
            return customers;
        }

        private DataRowCollection Execute(string sql)
        {
            try
            {
                DataSet resultSet = new DataSet();
                using (SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(sql, new SqlConnection(ConnectionString))))
                {
                    adapter.Fill(resultSet);
                }
                return resultSet.Tables[0].Rows;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<Customer> ProcessCustomers(DataRowCollection dataRows)
        {
            List<Customer> customers = new List<Customer>();
            foreach (DataRow row in dataRows)
            {
                string customerName = (string)row["CustomerID"];
                Customer customer = new Customer() { CustomerName = customerName };
                customers.Add(customer);
            }
            return customers;
        }

        private List<Invoice> ProcessInvoices(DataRowCollection dataRows)
        {
            List<Invoice> invoices = new List<Invoice>();
            foreach (DataRow row  in dataRows)
            {
                string customerName = (string)row["CustomerName"];
                decimal extendedPrice = (decimal)row["ExtendedPrice"];
                decimal freightPrice = (decimal)row["Freight"];
                Invoice invoice = new Invoice(customerName, extendedPrice, freightPrice);
                invoices.Add(invoice);
            }
            return invoices;
        }
    }
}
