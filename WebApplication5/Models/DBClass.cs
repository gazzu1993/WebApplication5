using System;
using System.Data;
using System.Data.SqlClient;
 


public class DBClass : IDisposable
{
    SqlConnection con;
    SqlCommand cmd;
    string strConnection = "";
    public DBClass(string spname, CommandType cmdtype)
    {
        strConnection = "Server=tcp:gazzusql.database.windows.net,1433;Initial Catalog=Gazzu_Db1;Persist Security Info=False;User ID=gajendraq3;Password=gajendrajangid@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        con = new SqlConnection(strConnection);
        cmd = new SqlCommand(spname);
        cmd.CommandType = cmdtype;
        cmd.Connection = con;

        if (con.State == ConnectionState.Closed)
        con.Open();
    }
    public DBClass(string spname, CommandType cmdtype, string ConnStr)
    {
        strConnection = ConnStr;
        con = new SqlConnection(strConnection);

        cmd = new SqlCommand(spname);
        cmd.CommandType = cmdtype;
        cmd.Connection = con;

        if (con.State == ConnectionState.Closed)
            con.Open();
    }
    public void AddParameters(string pname, string pvalue)
    {
        cmd.Parameters.Add(pname, SqlDbType.NVarChar).Value = pvalue;
    }
    public void AddParameters(string pname, DataTable pvalue)
    {
        SqlParameter PrmTbl = cmd.Parameters.AddWithValue(pname, pvalue);
        PrmTbl.SqlDbType = SqlDbType.Structured;
        PrmTbl.Direction = ParameterDirection.Input;
    }
    public void AddParameters(string pname, bool pvalue)
    {
        cmd.Parameters.Add(pname, SqlDbType.Bit).Value = pvalue;
    }
    public void AddParameters(string pname, System.DBNull pvalue)
    {
        cmd.Parameters.Add(pname, SqlDbType.NVarChar).Value = pvalue;
    }
    public void AddParameters(string pname, Int64 pvalue)
    {
        cmd.Parameters.Add(pname, SqlDbType.BigInt).Value = pvalue;
    }
    public void AddParameters(string pname, int pvalue)
    {
        cmd.Parameters.Add(pname, SqlDbType.Int).Value = pvalue;
    }
    public void AddParameters(string pname, float pvalue)
    {
        cmd.Parameters.Add(pname, SqlDbType.Float).Value = pvalue;
    }
    public void AddParameters(string pname, DateTime pvalue)
    {
        cmd.Parameters.Add(pname, SqlDbType.DateTime).Value = pvalue;
    }
    public void AddParameters(string pname, Decimal pvalue)
    {
        cmd.Parameters.Add(pname, SqlDbType.Decimal).Value = pvalue;
    }
    public void AddParameters(string pname, Double pvalue)
    {
        cmd.Parameters.Add(pname, SqlDbType.Decimal).Value = pvalue;
    }
    //public void AddParameters(string pname, DataTable pvalue)
    //{
    //    cmd.Parameters.AddWithValue(pname, pvalue);
    //}


    public DataTable ReturnDataTable()
    {
        using (DataTable dt = new DataTable())
        {
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
    }
    public int ExecuteNonQueryint()
    {
        return cmd.ExecuteNonQuery();
    }
    public DataSet ReturnDataSet()
    {
        using (DataSet ds = new DataSet())
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(ds);
            }
            return ds;
        }
    }

    public void TimeOut(int time)
    {
        cmd.CommandTimeout = time;
    }

    public string ReturnString()
    {
        return Convert.ToString(cmd.ExecuteScalar());
    }

    public void ExecuteNonQuery()
    {
        cmd.ExecuteNonQuery();
    }

    public int ExecuteNonQueryWithReturn()
    {
        return Convert.ToInt16(cmd.ExecuteNonQuery());
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // get rid of managed resources
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
            }
            cmd.Dispose();
        }
    }
}
