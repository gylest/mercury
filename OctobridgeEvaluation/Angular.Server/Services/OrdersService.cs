using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using AngularClient.Models;

namespace AngularClient.Services
{
    public class OrdersService
    {
        // Database connection string
        private readonly string _connectionString;

        public OrdersService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Order[] GetAllOrders()
        {
            string sqlGet = "SELECT [Id],[OrderStatus],[CustomerId],[FreightAmount],[SubTotal],[TotalDue],[PaymentDate],[ShippedDate],[CancelDate],[RecordCreated],[RecordModified] FROM [dbo].[Order]";
            Order[] arrayOrders;

            using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    SqlDataReader reader;
                    List<Order> listOrders = new List<Order>();

                    cmd.CommandText = sqlGet;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection;

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Order curOrder = new Order
                            {
                                Id = reader.GetInt32(0),
                                OrderStatus = reader.GetString(1),
                                CustomerId = reader.GetInt32(2),
                                FreightAmount = reader.GetDecimal(3),
                                SubTotal = reader.GetDecimal(4),
                                TotalDue = reader.GetDecimal(5),
                                PaymentDate = reader.IsDBNull(6) ? null : reader.GetDateTime(6),
                                ShippedDate = reader.IsDBNull(7) ? null : reader.GetDateTime(7),
                                CancelDate = reader.IsDBNull(8) ? null : reader.GetDateTime(8),
                                RecordCreated = reader.GetDateTime(9),
                                RecordModified = reader.GetDateTime(10)
                            };

                            listOrders.Add(curOrder);
                        }
                    }

                    reader.Close();

                    arrayOrders = listOrders.ToArray();

                }

                sqlConnection.Close();

            }

            return arrayOrders;

        }

        public void AddOrder(Order order)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    string storedProcedure = "AddOrder";

                    cmd.CommandText = storedProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlConnection;

                    SqlParameter paramOrderStatus = new SqlParameter
                    {
                        ParameterName = "@OrderStatus",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 50,
                        Direction = ParameterDirection.Input,
                        Value = order.OrderStatus
                    };
                    cmd.Parameters.Add(paramOrderStatus);

                    SqlParameter paramCustomerId = new SqlParameter
                    {
                        ParameterName = "@CustomerId",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                        Value = order.CustomerId
                    };
                    cmd.Parameters.Add(paramCustomerId);

                    SqlParameter paramFreightAmount = new SqlParameter
                    {
                        ParameterName = "@FreightAmount",
                        SqlDbType = SqlDbType.Money,
                        Direction = ParameterDirection.Input,
                        Value = order.FreightAmount
                    };
                    cmd.Parameters.Add(paramFreightAmount);

                    SqlParameter paramSubTotal = new SqlParameter
                    {
                        ParameterName = "@SubTotal",
                        SqlDbType = SqlDbType.Money,
                        Direction = ParameterDirection.Input,
                        Value = order.SubTotal
                    };
                    cmd.Parameters.Add(paramSubTotal);

                    SqlParameter paramTotalDue = new SqlParameter
                    {
                        ParameterName = "@TotalDue",
                        SqlDbType = SqlDbType.Money,
                        Direction = ParameterDirection.Input,
                        Value = order.TotalDue
                    };
                    cmd.Parameters.Add(paramTotalDue);

                    // Execute stored procedure
                    cmd.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }

        }

        public void DeleteOrder(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    string storedProcedure = "DeleteOrderById";

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

        public void UpdateOrder(Order order, DateTime orderDate)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    string storedProcedure = "UpdateOrder";

                    cmd.CommandText = storedProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlConnection;

                    SqlParameter paramId = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                        Value = order.Id
                    };
                    cmd.Parameters.Add(paramId);

                    SqlParameter paramOrderStatus = new SqlParameter
                    {
                        ParameterName = "@OrderStatus",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 50,
                        Direction = ParameterDirection.Input,
                        Value = order.OrderStatus
                    };
                    cmd.Parameters.Add(paramOrderStatus);

                    SqlParameter paramOrderDate = new SqlParameter
                    {
                        ParameterName = "@OrderDate",
                        SqlDbType = SqlDbType.DateTime2,
                        Direction = ParameterDirection.Input,
                        Value = orderDate
                    };
                    cmd.Parameters.Add(paramOrderDate);


                    // Execute stored procedure
                    cmd.ExecuteNonQuery();
                }

                sqlConnection.Close();
            }

        }

    }

}
