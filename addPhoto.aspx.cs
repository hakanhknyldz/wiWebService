using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addPhoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        StartUpLoad();

    }//end-upload-button

    private void StartUpLoad()
    {

        if (FileUploadPhoto.HasFile)
        {
            string imgName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + FileUploadPhoto.FileName.ToString();
            string wiPath = "~/Admin/Weather/" + imgName;

            FileUploadPhoto.SaveAs(Server.MapPath(wiPath));
            try
            {
                if (FileUploadPhoto.PostedFile.ContentType == "image/jpeg" || FileUploadPhoto.PostedFile.ContentType == "image/jpg" || FileUploadPhoto.PostedFile.ContentType == "image/png" || FileUploadPhoto.PostedFile.ContentType == "image/jpeg" || FileUploadPhoto.PostedFile.ContentType == "image/gif" || FileUploadPhoto.PostedFile.ContentType == "image/bmp")
                {
                    if (FileUploadPhoto.PostedFile.ContentLength < 1048576)
                    {
                        
                        ExecuteInsert(wiPath, tbUrl.Text);

                        lblSuccess.Text = "Başarılı";


                    }
                    else
                        lblSuccess.Text = "Boyut 1 mb'dan fazla olamaz!";
                }
                else
                    lblSuccess.Text = "Sadece fotoğraf yüklemeyi deneyiniz!";
            }
            catch (Exception ex)
            {
                lblSuccess.Text = "Hata: " + ex.Message;
            }
        }
    }
    
    public string GenerateFileName(string context)
    {
        return DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N");
    }
    private void ExecuteInsert(string wiPath,string wiUrl)
    {

        SqlConnection conn = wiDB.connect();

        int catId = wiDB.getCatId(ddlCategory.SelectedValue);
        int genderId = wiDB.getGenderId(ddlGender.SelectedValue);

        string query = "insert into wiClothes values(@wiPath,@wiUrl,@catId,@genderId)";
        //string old_query = "INSERT INTO Weather (name, path, category, gender) VALUES(@name,@path,@category, @gender)";
        try
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@wiPath", wiPath);
            cmd.Parameters.AddWithValue("@wiUrl", wiUrl);
            cmd.Parameters.AddWithValue("@catId", catId);
            cmd.Parameters.AddWithValue("@genderId", genderId);

            int result = cmd.ExecuteNonQuery();
        }

        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);

        }

        finally
        {
            conn.Close();
        }

    }//end-executeInsert
}