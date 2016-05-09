using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
/// <summary>
/// Summary description for wiDBMethods
/// </summary>
public class wiDB
{
    private static wiDB wiInstance = null;
    static SqlDataReader reader;
    static SqlCommand command;
    public wiDB()
    {
        wiInstance = this;
    }
    /*
    public static  wiDB getInstance()
    {
        if(wiInstance == null)
        {
            wiInstance = new wiDB();
        }

        return wiInstance;
    }
    */
    public static SqlConnection connect()
    {
        String connString = WebConfigurationManager.ConnectionStrings["wiConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connString);
        conn.Open();

        return conn;
    }

    public static bool authentication(string username, string passwd)
    {
        bool valid = false;
            
        SqlConnection conn = connect();
        string query = "select * from wiUser where username = @username and passwd = @passwd";
        command = new SqlCommand(query, conn);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@passwd", passwd);

        reader = command.ExecuteReader();

        if (reader.HasRows)
            valid = true;

        conn.Close();
        return valid;
    }


    public static DataTable getDataTable(string query)
    {
        SqlConnection conn = connect();
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        DataTable dt = new DataTable();

        try
        {
            adapter.Fill(dt);
        }
        catch(SqlException se)
        {
            throw new Exception(se.Message);
        }
        finally
        {
            adapter.Dispose();
            conn.Close();
            conn.Dispose();
        }

        return dt;
    }
    public static DataSet getDataSet(string query)
    {
        SqlConnection conn = connect();
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        DataSet ds = new DataSet();

        try
        {
            adapter.Fill(ds);
        }
        catch (SqlException e)
        {
            throw new Exception(e.Message);
        }

        adapter.Dispose();
        conn.Close();
        conn.Dispose();

        return ds;
    }
    public static DataRow getDataRow(string query)
    {
        DataTable dt = getDataTable(query);
        if(dt.Rows.Count == 0)
        {
            return null;
        }

        return dt.Rows[0];
    }


    public static string getDataCell(string query)
    {
        DataTable dt = getDataTable(query);
        if (dt.Rows.Count == 0)
        {
            return null;
        }
        return dt.Rows[0][0].ToString();
    }




    /*** Get category id **/
    public static int getCatId(String catName)
    {
        int catId = 0;
        SqlConnection conn = connect();
        string query = "select catId from wiCategory where catName = @catName";
        command = new SqlCommand(query, conn);
        command.Parameters.AddWithValue("@catName", catName);

        reader = command.ExecuteReader();
        if(reader.HasRows)
        {
           catId = int.Parse(reader["catId"].ToString());
        }

        conn.Close();
        return catId;
    }

    /*** Get gender id **/
    public static int getGenderId(String genderType)
    {
        int genderId = 0;
        SqlConnection conn = connect();
        string query = "select genderId from wiGender where genderType = @genderType";
        command = new SqlCommand(query, conn);
        command.Parameters.AddWithValue("@genderType", genderType);

        reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            genderId = int.Parse(reader["genderId"].ToString());
        }

        conn.Close();
        return genderId;
    }


}