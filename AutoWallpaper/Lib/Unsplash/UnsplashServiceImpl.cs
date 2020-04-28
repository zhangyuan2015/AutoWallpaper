using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;

namespace AutoWallpaper.Lib.Unsplash
{
    public class UnsplashServiceImpl
    {
        #region Unsplash

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetImgPath(string[] args)
        {
            string host = "https://api.unsplash.com/photos/random?client_id=f6e65da8c7e88bebb109e98426e0161eeb7a7aed976d11cb46b9ce0e487ee2d4";
            string url = "";
            string fileName = "";
            string strJson = "";
            WebResponse response = null;
            StreamReader streamReader = null;

            try
            {
                string orientationParam = args.FirstOrDefault(a => a.Contains("orientation="));
                if (!string.IsNullOrWhiteSpace(orientationParam))
                    host += "&" + orientationParam;
                
                int w = Screen.PrimaryScreen.Bounds.Width;
                int h = Screen.PrimaryScreen.Bounds.Height;
                host += "&w=" + w + "&h=" + h;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebRequest request = WebRequest.Create(host);
                request.Method = "get";
                response = request.GetResponse();
                streamReader = new StreamReader(response.GetResponseStream());
                strJson = streamReader.ReadToEnd();

                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(strJson));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof (UnsplashImgModel));
                UnsplashImgModel imgObj = ser.ReadObject(ms) as UnsplashImgModel;
                ms.Close();
                if (imgObj != null)
                {
                    url = imgObj.urls.custom;
                    fileName = imgObj.id + ".jpg";
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                response?.Dispose();
                streamReader?.Dispose();
            }
            return !string.IsNullOrWhiteSpace(url) ? Utils.SaveFile(url, "Unsplash", fileName, strJson) : "";
        }

        #endregion
    }
}