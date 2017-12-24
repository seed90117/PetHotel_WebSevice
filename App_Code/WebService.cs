using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;

/// <summary>
/// WebService 的摘要描述
/// </summary>
[WebService(Namespace = "http://140.127.22.4/PetHotel_WebService/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    string strdbcon = "server=140.127.22.4;database=pet;uid=b10056;pwd=b10056";
    SqlConnection objcon;
    SqlCommand sqlcmd;
    string sql;

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public string Insertclient(string owner, string phone, string address, string petname, string species, string breed, string sex, string age, string photo)
    {
        string re = "false";
        try
        {
            objcon = new SqlConnection(strdbcon);
            objcon.Open();
            sql = "INSERT INTO Client(owner,phone,address,petname,species,breed,sex,age,photo,register) VALUES('" + owner + "','" + phone + "','" + address + "','" + petname + "','" + species + "','" + breed + "','" + sex + "','" + age + "','" + photo + "','" + DateTime.Now + "')";
            SqlCommand insertcmd = new SqlCommand(sql, objcon);
            insertcmd.ExecuteNonQuery();
            objcon.Close();
            re = "true";
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
        return re;
    }

    [WebMethod]
    public string ListLodgin(string lodgin)
    {
        string re = null;
        try
        {
            objcon = new SqlConnection(strdbcon);
            objcon.Open();
            sql = "select * from Lodgin where clientID =" + "'" + lodgin +"'";
            sqlcmd = new SqlCommand(sql, objcon);
            SqlDataReader dr = sqlcmd.ExecuteReader();
            if (dr.IsClosed == false)
            {
                while (dr.Read())
                {
                    re += dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString() + "," + dr[3].ToString() + "," + dr[4].ToString();
                }
                dr.Close();
                objcon.Close();
            }
            else
            {
                re = "No Data";
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }

        return re;
    }

    [WebMethod]
    public string List()
    {
        string re = null;
        try
        {
            objcon = new SqlConnection(strdbcon);
            objcon.Open();
            sql = "select * from Report";
            sqlcmd = new SqlCommand(sql, objcon);
            SqlDataReader dr = sqlcmd.ExecuteReader();
            if (dr.IsClosed == false)
            {
                while (dr.Read())
                {
                    re += dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString() + "," + dr[3].ToString() + "," + dr[4].ToString() + "," + dr[5].ToString() + "," + dr[6].ToString() + "," + dr[7].ToString() + "," + dr[8].ToString() + "," + ",http://140.127.22.4/PetHotel_Datebase/petphoto/" + dr[9].ToString() + dr[10].ToString() + ";";
                }
                dr.Close();
                objcon.Close();
            }
            else
            {
                re = "No Data";
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }

        return re;
    }

    [WebMethod]
    public string List(string inquiryItem, string inquiryString)
    {
        string re = null;
        switch (inquiryItem)
        {
            case "全部":
                sql = "select * from Report";
                break;

            case "電話":
                sql = "select * from Report where phone =" + "'" + inquiryString + "'";
                break;

            case "飼主":
                sql = "select * from Report where owner =" + "'" + inquiryString + "'";
                break;

            case "寵物名":
                sql = "select * from Report where petname =" + "'" + inquiryString + "'";
                break;
        }
        try
        {
            objcon = new SqlConnection(strdbcon);
            objcon.Open();
            sqlcmd = new SqlCommand(sql, objcon);
            SqlDataReader dr = sqlcmd.ExecuteReader();
            if (dr.IsClosed == false)
            {
                while (dr.Read())
                {
                    re += dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString() + "," + dr[3].ToString() + "," + dr[4].ToString() + "," + dr[5].ToString() + "," + dr[6].ToString() + "," + dr[7].ToString() + "," + dr[8].ToString() + "," + ",http://140.127.22.4/PetHotel_Datebase/petphoto/" + dr[9].ToString() + dr[10].ToString() + ";";
                }
                dr.Close();
                objcon.Close();
            }
            else
            {
                re = "No Data";
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }

        return re;
    }

    [WebMethod]
    public string registrimageinsert(String in_image, String fileName)
    {

        byte[] bt = Convert.FromBase64String(in_image);
        System.IO.MemoryStream stream = new System.IO.MemoryStream(bt);
        Bitmap bitmap = new Bitmap(stream);

        // string fileName = "uploadImage.jpg";
        try
        {

            MemoryStream memoryStream = new MemoryStream(bt);

            FileStream fileStream = new FileStream("C:\\inetpub\\wwwroot\\Diabetes\\food\\" + fileName, FileMode.Create);

            memoryStream.WriteTo(fileStream);

            memoryStream.Close();

            fileStream.Close();

            fileStream = null;

            memoryStream = null;
            return "上傳成功";

        }

        catch (Exception ex)
        {

            return ex.Message;

        }
    }
}
