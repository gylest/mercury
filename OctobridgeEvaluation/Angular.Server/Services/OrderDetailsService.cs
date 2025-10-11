namespace AngularServer.Services;

public class OrderDetailsService
{
    // Database connection string
    private readonly string _connectionString;

    public OrderDetailsService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
    {
        List<OrderDetail> listOrderDetails = new List<OrderDetail>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            using (SqlCommand cmd = new SqlCommand("GetOrderDetailsByOrderId", conn))
            {
                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameters
                SqlParameter paramOrderId = new SqlParameter
                {
                    ParameterName = "@orderId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = orderId
                };
                cmd.Parameters.Add(paramOrderId);

                // execute the command
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    // 	SELECT 	[LineId] , [OrderId], [ProductId] , [UnitPrice] , [Quantity] , [RecordCreated],[RecordModified] 
                    while (reader.Read())
                    {
                        OrderDetail curOrderDetail = new OrderDetail
                        {
                            LineId = reader.GetInt32(0),
                            OrderId = reader.GetInt32(1),
                            ProductId = reader.GetInt32(2),
                            UnitPrice = reader.GetDecimal(3),
                            Quantity = reader.GetInt32(4),
                            RecordCreated = reader.GetDateTime(5),
                            RecordModified = reader.GetDateTime(6)
                        };

                        listOrderDetails.Add(curOrderDetail);
                    }
                }
                else
                {
                    throw new Exception(message: $"Cannot find order details for id {orderId}");
                }

                // close connection
                reader.Close();
            }

            conn.Close();
        }

        return listOrderDetails;
    }

}
