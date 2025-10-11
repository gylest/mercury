namespace AngularServer.Services;

public class AttachmentsService
{
    // Database connection string
    private readonly string _connectionString;

    public AttachmentsService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void SaveFile(string fileName, string fileType, long length, byte[] buffer, out int identityValue, out DateTime dateStamp)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            using (SqlCommand cmd = new SqlCommand("AddAttachment", conn))
            {

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add("@filename", SqlDbType.NVarChar, 255).Value = fileName;
                cmd.Parameters.Add("@filetype", SqlDbType.NVarChar, 100).Value = fileType;
                cmd.Parameters.Add("@filelength", SqlDbType.BigInt).Value = length;
                cmd.Parameters.Add("@filedata", SqlDbType.VarBinary, int.MaxValue).Value = buffer;

                SqlParameter paramErrorNumber = new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramErrorNumber);

                SqlParameter paramDateStamp = new SqlParameter
                {
                    ParameterName = "@dt",
                    SqlDbType = SqlDbType.DateTime2,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(paramDateStamp);

                // execute the command
                cmd.ExecuteNonQuery();

                identityValue = (int)cmd.Parameters["@id"].Value;
                dateStamp = (DateTime)cmd.Parameters["@dt"].Value;
            }

            // close connection
            conn.Close();
        }
    }

    public byte[] GetFile(int id, out string fileName, out string fileType)
    {
        SqlDataReader reader;
        byte[] fileData = null;
        DateTime recordCreated;
        long length;

        // Initialize output variables
        fileName = "FileNotSet.txt";
        fileType = "Unknown";

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            using (SqlCommand cmd = new SqlCommand("GetAttachmentData", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                // execute the command
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fileName = reader.GetString(0);
                        fileType = reader.GetString(1);
                        length = reader.GetInt64(2);
                        fileData = reader.GetSqlBinary(3).Value;
                        recordCreated = reader.GetDateTime(4);

                        Console.WriteLine($"Attachment Data: Filename = {fileName}, FileType = {fileType}, Length = {length}, CreateDate = {recordCreated}");
                    }
                }
            }

        }

        return fileData;
    }

    public Attachment[] GetAttachments(string fileName)
    {
        List<Attachment> listAttachments = new List<Attachment>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            using (SqlCommand cmd = new SqlCommand("GetAttachmentsByName", conn))
            {

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter
                SqlParameter paramFileName = new SqlParameter
                {
                    ParameterName = "@filename",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 255,
                    Direction = ParameterDirection.Input
                };
                if (string.IsNullOrEmpty(fileName)) paramFileName.Value = DBNull.Value; else paramFileName.Value = fileName;
                cmd.Parameters.Add(paramFileName);

                // execute the command
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Attachment curAttachment = new Attachment
                        {
                            Id = reader.GetInt32(0),
                            FileName = reader.GetString(1),
                            FileType = reader.GetString(2),
                            Length = reader.GetInt64(3),
                            RecordCreated = reader.GetDateTime(4)
                        };

                        listAttachments.Add(curAttachment);
                    }
                }

                // close connection
                reader.Close();
            }

            conn.Close();
        }

        Attachment[] arrayAttachments = listAttachments.ToArray();

        return arrayAttachments;
    }

    public void DeleteFile(int id)
    {
        using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand cmd = new SqlCommand())
            {
                string storedProcedure = "DeleteAttachmentById";

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
}
