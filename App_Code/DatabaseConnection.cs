using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace com.ashaw.pricing
{
    /// <summary>
    /// Summary description for DatabaseConnection
    /// </summary>
    public class DatabaseConnection : IDisposable {
        /// <summary>
        /// Gets or sets the SQL connection.
        /// </summary>
        /// <value>
        /// The SQL connection.
        /// </value>
        protected SqlConnection sqlConnection { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnection" /> class.
        /// </summary>
        public DatabaseConnection()
        {
            this.sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["PricingConnectionString"].ConnectionString);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.sqlConnection.Dispose();
        }

        /// <summary>
        /// Runs the command.
        /// </summary>
        /// <param name="comm">The command.</param>
        public void RunReaderCommand ( SqlCommand command ) {
            command.Connection = this.sqlConnection;
            SqlDataReader sdr = command.ExecuteReader();
            //  TODO : complete.
        }

        /// <summary>
        /// Runs the scalar command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public object RunScalarCommand(SqlCommand command)
        {
            command.Connection = this.sqlConnection;
            command.Connection.Open();
            object result = command.ExecuteScalar();
            command.Connection.Close();
            command.Dispose();
            return result;
        }

        public List<object> SProcToObjectList ( Type resultType, string sProcName, params KeyValuePair<string,object>[] sProcParams ) {
            List<object> results = new List<object>();

            // Call the stored procedure with the parameters.
            SqlCommand command = new SqlCommand();
            command.Connection = this.sqlConnection;
            command.Connection.Open();
            foreach ( KeyValuePair<string,object> kvp in sProcParams){
                command.Parameters.Add(new SqlParameter(kvp.Key,kvp.Value));
            }
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = sProcName;

            SqlDataReader sdr = command.ExecuteReader();
            // Go through each row.
            while (sdr.Read())
            {
                // Build a list of KVPs for the results from the stored procedure.
                int fc = sdr.VisibleFieldCount;
                List<KeyValuePair<string, object>> rowResults = new List<KeyValuePair<string, object>>();
                for (int i = 0; i < fc; i++)
                {
                    string res_name =  sdr.GetName(i);
                    object res = sdr.GetValue(i);
                    rowResults.Add(new KeyValuePair<string, object>(res_name, res));
                }

                // translate each KVP from this row into the properties in our class.
                results.Add(DataObjectSerialisers.TranslateKVPsToObjects(rowResults, resultType));
            }
            command.Connection.Close();
            command.Dispose();
            return results;
        }

        /// <summary>
        /// Run a stored procedure that returns a scalar and return the resulting object.
        /// </summary>
        /// <param name="sProcName">Name of the s proc.</param>
        /// <param name="sProcParams">The s proc params.</param>
        /// <returns></returns>
        public object SProcToObject(string sProcName, params KeyValuePair<string, object>[] sProcParams)
        {
            object result = new object();

            // Call the stored procedure with the parameters.
            SqlCommand command = new SqlCommand();
            command.Connection = this.sqlConnection;
            command.Connection.Open();
            foreach (KeyValuePair<string, object> kvp in sProcParams)
            {
                command.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
            }
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = sProcName;

            SqlParameter pRetValue = command.Parameters.Add("@Ret", System.Data.SqlDbType.Int);
            pRetValue.Direction = ParameterDirection.ReturnValue;

            command.ExecuteScalar();
            command.Connection.Close();
            command.Dispose();

            return command.Parameters["@Ret"].Value;
        }
    }
}