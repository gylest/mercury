using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using AngularClient.Models;

namespace AngularClient.Services
{
    public class ProductsService
    {
        // Database connection string
        private readonly string _connectionString;

        public ProductsService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Product> GetAllProducts()
        {
            const string sqlGet = "SELECT [ProductId],[Name],[ProductNumber],[ProductCategoryId],[Cost],[RecordCreated],[RecordModified] FROM [dbo].[Product]";
            List<Product> listProducts = new List<Product>();

            using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    SqlDataReader reader;

                    cmd.CommandText = sqlGet;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection;

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                ProductId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ProductNumber = reader.GetString(2),
                                ProductCategoryId = reader.GetInt32(3),
                                Cost = reader.GetDecimal(4),
                                RecordCreated = reader.GetDateTime(5),
                                RecordModified = reader.GetDateTime(6)
                            };

                            listProducts.Add(product);
                        }
                    }

                    reader.Close();

                }

                sqlConnection.Close();

            }

            return listProducts;

        }

        public List<Product> GetProductsByName(string name, string productNumber)
        {
            List<Product> listProducts = new List<Product>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                using (SqlCommand cmd = new SqlCommand("GetProductsByNameNumber", conn))
                {

                    // 2. set the command object so it knows to execute a stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 3. add parameters
                    SqlParameter paramName = new SqlParameter
                    {
                        ParameterName = "@name",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 100,
                        Direction = ParameterDirection.Input
                    };
                    paramName.Value = string.IsNullOrEmpty(name) ? DBNull.Value : name;
                    cmd.Parameters.Add(paramName);

                    SqlParameter paramProductNumber = new SqlParameter
                    {
                        ParameterName = "@productNumber",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 100,
                        Direction = ParameterDirection.Input
                    };
                    paramProductNumber.Value = string.IsNullOrEmpty(productNumber) ? DBNull.Value : productNumber;
                    cmd.Parameters.Add(paramProductNumber);

                    // execute the command
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                ProductId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ProductNumber = reader.GetString(2),
                                ProductCategoryId = reader.GetInt32(3),
                                Cost = reader.GetDecimal(4),
                                RecordCreated = reader.GetDateTime(5),
                                RecordModified = reader.GetDateTime(6)
                            };

                            listProducts.Add(product);
                        }
                    }
                    else
                    {
                        throw new Exception(message: $"Cannot find products that match name '{name}' and number '{productNumber}'");
                    }


                    // close connection
                    reader.Close();
                }

                conn.Close();
            }

            return listProducts;
        }

        public Product GetProductById(int productId)
        {
            Product product = new Product();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                using (SqlCommand cmd = new SqlCommand("GetProductById", conn))
                {

                    // 2. set the command object so it knows to execute a stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 3. add parameters
                    SqlParameter paramId = new SqlParameter
                    {
                        ParameterName = "@id",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                        Value = productId
                    };
                    cmd.Parameters.Add(paramId);

                    // execute the command
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            product.ProductId = reader.GetInt32(0);
                            product.Name = reader.GetString(1);
                            product.ProductNumber = reader.GetString(2);
                            product.ProductCategoryId = reader.GetInt32(3);
                            product.Cost = reader.GetDecimal(4);
                            product.RecordCreated = reader.GetDateTime(5);
                            product.RecordModified = reader.GetDateTime(6);
                        }
                    }
                    else
                    {
                        throw new Exception(message: $"Cannot find product for id {productId}");
                    }

                    // close connection
                    reader.Close();
                }

                conn.Close();
            }

            return product;
        }

        public void AddProduct(Product product)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    string storedProcedure = "AddProduct";

                    cmd.CommandText = storedProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlConnection;

                    SqlParameter paramName = new SqlParameter
                    {
                        ParameterName = "@Name",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 50,
                        Direction = ParameterDirection.Input,
                        Value = product.Name
                    };
                    cmd.Parameters.Add(paramName);

                    SqlParameter paramProductNumber = new SqlParameter
                    {
                        ParameterName = "@ProductNumber",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 100,
                        Direction = ParameterDirection.Input,
                        Value = product.ProductNumber
                    };
                    cmd.Parameters.Add(paramProductNumber);

                    SqlParameter paramProductCategoryId = new SqlParameter
                    {
                        ParameterName = "@ProductCategoryId",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                        Value = product.ProductCategoryId
                    };
                    cmd.Parameters.Add(paramProductCategoryId);

                    SqlParameter paramCost = new SqlParameter
                    {
                        ParameterName = "@Cost",
                        SqlDbType = SqlDbType.Money,
                        Size = 50,
                        Direction = ParameterDirection.Input,
                        Value = product.Cost
                    };
                    cmd.Parameters.Add(paramCost);

                    // Execute stored procedure
                    cmd.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }

        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    string storedProcedure = "DeleteProductById";

                    cmd.CommandText = storedProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlConnection;

                    SqlParameter paramId = new SqlParameter("@Id", id);
                    cmd.Parameters.Add(paramId);

                    cmd.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }

        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    string storedProcedure = "UpdateProductById";

                    cmd.CommandText = storedProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlConnection;

                    SqlParameter paramId = new SqlParameter
                    {
                        ParameterName = "@ProductId",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                        Value = product.ProductId
                    };
                    cmd.Parameters.Add(paramId);

                    SqlParameter paramName = new SqlParameter
                    {
                        ParameterName = "@Name",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 50,
                        Direction = ParameterDirection.Input,
                        Value = product.Name
                    };
                    cmd.Parameters.Add(paramName);

                    SqlParameter paramProductNumber = new SqlParameter
                    {
                        ParameterName = "@ProductNumber",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 100,
                        Direction = ParameterDirection.Input,
                        Value = product.ProductNumber
                    };
                    cmd.Parameters.Add(paramProductNumber);

                    SqlParameter paramProductCategoryId = new SqlParameter
                    {
                        ParameterName = "@ProductCategoryId",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                        Value = product.ProductCategoryId
                    };
                    cmd.Parameters.Add(paramProductCategoryId);

                    SqlParameter paramCost = new SqlParameter
                    {
                        ParameterName = "@Cost",
                        SqlDbType = SqlDbType.Money,
                        Size = 50,
                        Direction = ParameterDirection.Input,
                        Value = product.Cost
                    };
                    cmd.Parameters.Add(paramCost);

                    // Execute stored procedure
                    cmd.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }

        }

    }

}
