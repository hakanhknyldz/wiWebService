using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class wiConsole : System.Web.Services.WebService
{
    //wiDB wi = wiDB.getInstance();
    SqlConnection conn;
    public wiConsole()
    {

    }
  
    //wiUser Methods
    
    [WebMethod]
    public bool wiRegister(string name, string surname, string username, string passwd, string genderId)
    {
        bool valid = false;

        conn = wiDB.connect();
        DataRow dr = wiDB.getDataRow("select * from wiUser where username = '" + username + "'");
        if(dr != null)
        {
            return valid;
        }
        else
        {
            string query = "insert into wiUser values(@name,@surname,@username,@passwd,@genderId)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@passwd", passwd);
            cmd.Parameters.AddWithValue("@genderId", (int)genderId);

            cmd.ExecuteNonQuery();

            valid = true;
        }

        conn.Close();

        return valid;
    }


    [WebMethod]
    public bool wiLogin(string username,string passwd)
    {
        bool valid = false;

        if(wiDB.authentication(username, passwd))
        {
            valid = true;
        }

        return valid;
    }

    [WebMethod]
    public string wiGetUserDetails(string username)
    {
        string query = "select * from wiUser where username = '" + username + "'";
        SqlConnection conn = wiDB.connect();
        DataRow dr = wiDB.getDataRow(query);

        wiUser wiUser = null;
        if(dr != null)
        {
            wiUser = new wiUser
            {
                name = dr.Field<String>("name"),
                surname = dr.Field<String>("surname"),
                username = dr.Field<String>("username"),
                passwd = dr.Field<String>("passwd"),
                genderId = dr.Field<int>("genderId")
            };
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        
        return js.Serialize(wiUser);
    }

    /// <summary>
    /// android tarafında hava sıcaklığı , nem ve yağış oranına göre hesaplama yaptıktan sonra
    /// belirli aralıklara göre o sıcaklıkta giyilebilecek kıyafet türleri belirlenir.. ex: 35 de t-shirt giyilmesi.Mont giymek saçma olur!
    /// o aralıklar ve kullanıcının cinsiyeti baz alınarak kıyafetleri çekmek için webservisine ihtiyac duyarız
    /// servisimizin bu metotu catName => (tshirt, bluz,gömlek etc.) ve genderType => (Male,Female) olmak üzere 2 parametreye sahiptir.
    /// analiz kısmı android kısmında oldugundan bu metot analiz sonucunda istenilen kıyafet türlerinin database ile ilişkisini oluşturur sadece.
    /// 
    /// </summary>
    /// <param name="catName"></param>
    /// <param name="genderType"></param>
    /// <returns></returns>
   [WebMethod]
   public string wiGetClothes(string catName , string genderType)
    {
        int catId = wiDB.getCatId(catName);
        int genderId = wiDB.getGenderId(genderType);

        wiClothes clothes = null;
        wiClothes[] clothesArray = null;

        string query = "select * from wiClothes where catId = " + catId + " and genderId = "+ genderId;
            
        DataTable dt = wiDB.getDataTable(query);
        if(dt != null)
        {
            int rowCount = dt.Rows.Count;
            clothesArray = new wiClothes[rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                clothes = new wiClothes
                {
                    wiPath = dt.Rows[i]["wiPath"].ToString(),
                    wiUrl = dt.Rows[i]["wiUrl"].ToString(),
                    catId = (int)dt.Rows[i]["catId"],
                    genderId = (int)dt.Rows[i]["genderId"]
                };

                clothesArray[i] = clothes;
            }
        }
        else
        {
            return null;
        }
        JavaScriptSerializer js = new JavaScriptSerializer();

        return js.Serialize(clothesArray);
    }

}
