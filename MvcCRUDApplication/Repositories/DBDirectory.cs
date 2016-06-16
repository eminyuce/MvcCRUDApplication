using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MvcCRUDApplication.Entities;
using MvcCRUDApplication.Helpers;

namespace MvcCRUDApplication.Repositories
{
    public class DBDirectory
    {

        protected static string ConnectionStringKey = "ConnectionString";



        public static int SaveOrUpdateProduct(Product item)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            String commandText = @"test_SaveOrUpdateProduct";
            var parameterList = new List<SqlParameter>();
            var commandType = CommandType.StoredProcedure;
            parameterList.Add(DatabaseUtility.GetSqlParameter("ProductId", item.ProductId, SqlDbType.Int));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Name", item.Name.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Description", item.Description.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Price", item.Price, SqlDbType.Float));
            int id = DatabaseUtility.ExecuteScalar(new SqlConnection(connectionString), commandText, commandType, parameterList.ToArray()).ToInt();
            return id;
        }
         

        public static Product GetProduct(int productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            String commandText = @"SELECT * FROM test_Products WHERE productId=@productId";
            var parameterList = new List<SqlParameter>();
            var commandType = CommandType.Text;
            parameterList.Add(DatabaseUtility.GetSqlParameter("productId", productId, SqlDbType.Int));
            DataSet dataSet = DatabaseUtility.ExecuteDataSet(new SqlConnection(connectionString), commandText, commandType, parameterList.ToArray());
            if (dataSet.Tables.Count > 0)
            {
                using (DataTable dt = dataSet.Tables[0])
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var e = GetProductFromDataRow(dr);
                        return e;
                    }
                }
            }
            return null;
        }

        public static List<Product> GetProducts()
        {
            var list = new List<Product>();
            String commandText = @"SELECT * FROM test_Products ORDER BY ProductId DESC";
            var parameterList = new List<SqlParameter>();
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            var commandType = CommandType.Text;
            DataSet dataSet = DatabaseUtility.ExecuteDataSet(new SqlConnection(connectionString), commandText, commandType, parameterList.ToArray());
            if (dataSet.Tables.Count > 0)
            {
                using (DataTable dt = dataSet.Tables[0])
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var e = GetProductFromDataRow(dr);
                        list.Add(e);
                    }
                }
            }
            return list;
        }
        public static void DeleteProduct(int productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            String commandText = @"DELETE FROM test_Products WHERE ProductId=@ProductId";
            var parameterList = new List<SqlParameter>();
            var commandType = CommandType.Text;
            parameterList.Add(DatabaseUtility.GetSqlParameter("ProductId", productId, SqlDbType.Int));
            DatabaseUtility.ExecuteNonQuery(new SqlConnection(connectionString), commandText, commandType, parameterList.ToArray());
        }

        private static Product GetProductFromDataRow(DataRow dr)
        {
            var item = new Product();

            item.ProductId = dr["ProductId"].ToInt();
            item.Name = dr["Name"].ToStr();
            item.Description = dr["Description"].ToStr();
            item.Price = dr["Price"].ToFloat();
            return item;
        }


    }
}