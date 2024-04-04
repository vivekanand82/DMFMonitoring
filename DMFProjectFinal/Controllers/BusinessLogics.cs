using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;

namespace DMFProjectFinal.Controllers
{
    public class BusinessLogics
    {
        public static string GetJSDateStr(DateTime? DTTM)
        {
            string DTTMStr = "";
            try
            {
                if (DTTM != null)
                {
                    DTTMStr = DTTM.Value.Year + "-" + (DTTM.Value.Month <= 9 ? "0" + DTTM.Value.Month : "" + DTTM.Value.Month) + "-" + (DTTM.Value.Day <= 9 ? "0" + DTTM.Value.Day : "" + DTTM.Value.Day);
                }
            }
            catch (Exception)
            {

            }
            return DTTMStr;
        }
        public static string GetJSDateStrDDMMYYYY(DateTime? DTTM)
        {
            string DTTMStr = "";
            try
            {
                if (DTTM != null)
                {
                    DTTMStr =  (DTTM.Value.Day <= 9 ? "0" + DTTM.Value.Day : "" + DTTM.Value.Day) + "-" + (DTTM.Value.Month <= 9 ? "0" + DTTM.Value.Month : "" + DTTM.Value.Month) + "-"+DTTM.Value.Year ;
                }
            }
            catch (Exception)
            {

            }
            return DTTMStr;
        }
        public static DateTime GetDateFromDateStr(string DTSTR)
        {
            DateTime DTTM = new DateTime(1900, 1, 1);
            var Strs = DTSTR.Split('-');
            try
            {
                DTTM = new DateTime(int.Parse(Strs[0]),int.Parse(Strs[1]), int.Parse(Strs[2]));
            }
            catch (Exception)
            {

            }
            return DTTM;
        }
        public static TimeSpan GetTimeFromTimeStr(string TMSTR)
        {
            TimeSpan TS = new TimeSpan(0, 0, 0);
            var Strs = TMSTR.Split(':');
            try
            {
                TS = new TimeSpan(int.Parse(Strs[0]), int.Parse(Strs[1]), (Strs.Length>2)? int.Parse(Strs[2]):0);
            }
            catch (Exception)
            {

            }
            return TS;
        }
        public static string GetTimeStrFromTimeSpan(TimeSpan? TS)
        {
            string TSStr = "";
            try
            {
                if (TS != null)
                {
                    TSStr = TS.Value.Hours+":"+TS.Value.Minutes+":"+TS.Value.Seconds;
                }
            }
            catch (Exception)
            {

            }
            return TSStr;
        }
        public static string ConvertToJSONStr(object data)
        {
            try
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(data, settings);
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string UploadFileDMF(string CompString)
        {
            if (!CompString.Contains("Extt::"))
            {
                return "Expp:: Invalid File";
            }
            string FileString = CompString.Substring(0,CompString.IndexOf("Extt::"));
            string FileExt = CompString.Substring(CompString.IndexOf("Extt::")+6);
            return UploadFile(FileString, FileExt);
        }
        private static string UploadFile(string FileString,string FileExt)
        {
            try
            {
                string filePath = HttpContext.Current.Server.MapPath("~/Documents/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                try
                {
                    var ValidExtensions = ConfigurationManager.AppSettings["ValidExt"].Trim().Split(',').ToList();
                    if (!ValidExtensions.Contains("." + FileExt.ToLower().Trim()))
                    {
                        return "Expp:: File Extension is not valid";
                    }
                }
                catch (Exception)
                {
                }

                var guid = Guid.NewGuid().ToString();
                string FileName = guid + "." + FileExt;
                filePath = filePath + FileName;

                byte[] bytes = Convert.FromBase64String(FileString.Split(',')[1]);
                File.WriteAllBytes(filePath, bytes);

                return "/Documents/"+ FileName;
            }
            catch (Exception)
            {
                return "Expp:: Some error occured while uploading the file";
            }
        }

        public static string ConvertModelToJSONString<T>(T obj)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj);
                if (!property.GetGetMethod().IsVirtual)
                {
                    dictionary[property.Name] = value;
                }
            }
            try
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(dictionary, settings);
            }
            catch (Exception)
            {
                return "";
            }
        }

    }
    public class MailAndMessageServices
    {
        public static string SendSMS(string mobs, string msg, string TemplateID)
        {
            string RspMsg = "";
            try
            {
                if (!string.IsNullOrEmpty(mobs))
                {
                    //string stringpost = "http://sms.margsoft.com/submitsms.jsp?user=fisheri&key=a6ff8aab02XX&mobile="+ mobs + "&message="+ msg + "&senderid=UPFISH&accusage=1";
                    string stringpost = "http://sms.margsoft.org/sms_api/sendsms.php?username=fisheri&password=f@ish432&mobile=" + mobs + "&sendername=UPFISH&message=" + msg + "&templateid="+ TemplateID + "";

                    HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(stringpost);
                    try
                    {
                        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                        StreamReader sr = new StreamReader(httpres.GetResponseStream());
                        string results = sr.ReadToEnd();
                        sr.Close();
                        RspMsg = results;
                    }
                    catch(Exception Ex)
                    {
                        RspMsg = "Exp:: " + Ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                RspMsg = "Exp1 :: " + ex.Message;
            }
            return RspMsg;
        }
    }
}