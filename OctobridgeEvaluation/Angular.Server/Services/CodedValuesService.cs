namespace AngularServer.Services;

public class CodedValuesService
{
    // Database connection string
    private readonly string _connectionString;

    public CodedValuesService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<CodedValue> GetCodedValuesByName(string groupName)
    {
        string sqlGet = $"SELECT [GroupName],[Value],[Description] FROM [Octobridge].[dbo].[CodedValue] WHERE GroupName = @GroupName;";
        List<CodedValue> listCodedValues = new List<CodedValue>();

        using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand cmd = new SqlCommand())
            {
                SqlDataReader reader;

                cmd.CommandText = sqlGet;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                cmd.Parameters.AddWithValue("@GroupName", groupName);

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CodedValue curValue = new CodedValue
                        {
                            GroupName = reader.GetString(0),
                            Value = reader.GetString(1),
                            Description = reader.GetString(2)
                        };

                        listCodedValues.Add(curValue);
                    }
                }

                reader.Close();

            }

            sqlConnection.Close();

        }

        return listCodedValues;
    }

}
