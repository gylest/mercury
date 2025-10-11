namespace AngularServer.Services;

public class CustomersService
{
    // Database connection string
    private readonly string _connectionString;

    public CustomersService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Customer> GetAllCustomers()
    {
        string sqlGet = "SELECT [Id],[FirstName],[LastName],[MiddleName],[AddressLine1],[AddressLine2],[City],[PostalCode],[Telephone],[Email],[RecordCreated],[RecordModified] FROM [dbo].[Customer];";
        List<Customer> listCustomers = new List<Customer>();

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
                        Customer curCustomer = new Customer
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2)
                        };
                        if (reader.IsDBNull(3)) curCustomer.MiddleName = null; else curCustomer.MiddleName = reader.GetString(3);
                        curCustomer.AddressLine1 = reader.GetString(4);
                        if (reader.IsDBNull(5)) curCustomer.AddressLine2 = null; else curCustomer.AddressLine2 = reader.GetString(5);
                        curCustomer.City = reader.GetString(6);
                        curCustomer.PostalCode = reader.GetString(7);
                        curCustomer.Telephone = reader.GetString(8);
                        curCustomer.Email = reader.GetString(9);
                        curCustomer.RecordCreated = reader.GetDateTime(10);
                        curCustomer.RecordModified = reader.GetDateTime(11);

                        listCustomers.Add(curCustomer);
                    }
                }

                reader.Close();
            }

            sqlConnection.Close();

        }

        return listCustomers;

    }

    public void AddCustomer(Customer customer)
    {
        using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand cmd = new SqlCommand())
            {
                string storedProcedure = "AddCustomer";

                cmd.CommandText = storedProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection;

                SqlParameter paramFirstName = new SqlParameter
                {
                    ParameterName = "@FirstName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 100,
                    Direction = ParameterDirection.Input,
                    Value = customer.FirstName
                };
                cmd.Parameters.Add(paramFirstName);

                SqlParameter paramLastName = new SqlParameter
                {
                    ParameterName = "@LastName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 100,
                    Direction = ParameterDirection.Input,
                    Value = customer.LastName
                };
                cmd.Parameters.Add(paramLastName);

                SqlParameter paramMiddleName = new SqlParameter
                {
                    ParameterName = "@MiddleName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Direction = ParameterDirection.Input,
                    Value = String.IsNullOrEmpty(customer.MiddleName) ? DBNull.Value : customer.MiddleName
                };
                cmd.Parameters.Add(paramMiddleName);

                SqlParameter paramAddr1 = new SqlParameter
                {
                    ParameterName = "@AddressLine1",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 60,
                    Direction = ParameterDirection.Input,
                    Value = customer.AddressLine1
                };
                cmd.Parameters.Add(paramAddr1);

                SqlParameter paramAddr2 = new SqlParameter
                {
                    ParameterName = "@AddressLine2",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 60,
                    Direction = ParameterDirection.Input,
                    Value = String.IsNullOrEmpty(customer.AddressLine2) ? DBNull.Value : customer.AddressLine2
                };
                cmd.Parameters.Add(paramAddr2);

                SqlParameter paramCity = new SqlParameter
                {
                    ParameterName = "@City",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 30,
                    Direction = ParameterDirection.Input,
                    Value = customer.City
                };
                cmd.Parameters.Add(paramCity);

                SqlParameter paramPostCode = new SqlParameter
                {
                    ParameterName = "@PostalCode",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15,
                    Direction = ParameterDirection.Input,
                    Value = customer.PostalCode
                };
                cmd.Parameters.Add(paramPostCode);

                SqlParameter paramTel = new SqlParameter
                {
                    ParameterName = "@Telephone",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 25,
                    Direction = ParameterDirection.Input,
                    Value = customer.Telephone
                };
                cmd.Parameters.Add(paramTel);

                SqlParameter paramEmail = new SqlParameter
                {
                    ParameterName = "@Email",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 25,
                    Direction = ParameterDirection.Input,
                    Value = customer.Email
                };
                cmd.Parameters.Add(paramEmail);

                cmd.ExecuteNonQuery();
            }

            sqlConnection.Close();
        }

    }

    public List<Customer> GetCustomersByName(string lastName, string firstName)
    {
        List<Customer> listCustomers = new List<Customer>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            using (SqlCommand cmd = new SqlCommand("GetCustomersByName", conn))
            {

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameters
                SqlParameter paramLastName = new SqlParameter
                {
                    ParameterName = "@lastName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 100,
                    Direction = ParameterDirection.Input
                };
                if (string.IsNullOrEmpty(lastName)) paramLastName.Value = DBNull.Value; else paramLastName.Value = lastName;
                cmd.Parameters.Add(paramLastName);

                SqlParameter paramFirstName = new SqlParameter
                {
                    ParameterName = "@firstName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 100,
                    Direction = ParameterDirection.Input
                };
                if (string.IsNullOrEmpty(lastName)) paramFirstName.Value = DBNull.Value; else paramFirstName.Value = firstName;
                cmd.Parameters.Add(paramFirstName);

                // execute the command
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Customer curCustomer = new Customer
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2)
                        };
                        if (reader.IsDBNull(3)) curCustomer.MiddleName = null; else curCustomer.MiddleName = reader.GetString(3);
                        curCustomer.AddressLine1 = reader.GetString(4);
                        if (reader.IsDBNull(5)) curCustomer.AddressLine2 = null; else curCustomer.AddressLine2 = reader.GetString(5);
                        curCustomer.City = reader.GetString(6);
                        curCustomer.PostalCode = reader.GetString(7);
                        curCustomer.Telephone = reader.GetString(8);
                        curCustomer.Email = reader.GetString(9);
                        curCustomer.RecordCreated = reader.GetDateTime(10);
                        curCustomer.RecordModified = reader.GetDateTime(11);

                        listCustomers.Add(curCustomer);
                    }
                }

                // close connection
                reader.Close();
            }

            conn.Close();
        }

        return listCustomers;
    }

    public void DeleteCustomer(int id)
    {
        using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand cmd = new SqlCommand())
            {
                string storedProcedure = "DeleteCustomerById";

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

    public void UpdateCustomer(Customer customer)
    {
        using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand cmd = new SqlCommand())
            {
                string storedProcedure = "UpdateCustomerById";

                cmd.CommandText = storedProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection;

                SqlParameter paramId = new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = customer.Id
                };
                cmd.Parameters.Add(paramId);

                SqlParameter paramFirstName = new SqlParameter
                {
                    ParameterName = "@FirstName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 100,
                    Direction = ParameterDirection.Input,
                    Value = customer.FirstName
                };
                cmd.Parameters.Add(paramFirstName);

                SqlParameter paramLastName = new SqlParameter
                {
                    ParameterName = "@LastName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 100,
                    Direction = ParameterDirection.Input,
                    Value = customer.LastName
                };
                cmd.Parameters.Add(paramLastName);

                SqlParameter paramMiddleName = new SqlParameter
                {
                    ParameterName = "@MiddleName",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 50,
                    Direction = ParameterDirection.Input,
                    Value = String.IsNullOrEmpty(customer.MiddleName) ? DBNull.Value : customer.MiddleName
                };
                cmd.Parameters.Add(paramMiddleName);

                SqlParameter paramAddr1 = new SqlParameter
                {
                    ParameterName = "@AddressLine1",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 60,
                    Direction = ParameterDirection.Input,
                    Value = customer.AddressLine1
                };
                cmd.Parameters.Add(paramAddr1);

                SqlParameter paramAddr2 = new SqlParameter
                {
                    ParameterName = "@AddressLine2",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 60,
                    Direction = ParameterDirection.Input,
                    Value = String.IsNullOrEmpty(customer.AddressLine2) ? DBNull.Value : customer.AddressLine2
                };
                cmd.Parameters.Add(paramAddr2);

                SqlParameter paramCity = new SqlParameter
                {
                    ParameterName = "@City",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 30,
                    Direction = ParameterDirection.Input,
                    Value = customer.City
                };
                cmd.Parameters.Add(paramCity);

                SqlParameter paramPostCode = new SqlParameter
                {
                    ParameterName = "@PostalCode",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 15,
                    Direction = ParameterDirection.Input,
                    Value = customer.PostalCode
                };
                cmd.Parameters.Add(paramPostCode);

                SqlParameter paramTel = new SqlParameter
                {
                    ParameterName = "@Telephone",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 25,
                    Direction = ParameterDirection.Input,
                    Value = customer.Telephone
                };
                cmd.Parameters.Add(paramTel);

                SqlParameter paramEmail = new SqlParameter
                {
                    ParameterName = "@Email",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 25,
                    Direction = ParameterDirection.Input,
                    Value = customer.Email
                };
                cmd.Parameters.Add(paramEmail);

                cmd.ExecuteNonQuery();
            }

            sqlConnection.Close();
        }

    }

}
