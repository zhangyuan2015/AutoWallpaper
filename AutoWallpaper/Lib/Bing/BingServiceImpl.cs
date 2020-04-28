using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoWallpaper.Lib.Bing
{
    public class BingServiceImpl
    {
        #region Bing

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetImgPath(string[] args)
        {
            string host = "http://cn.bing.com/HPImageArchive.aspx?n=1";

            string idxParam = args.FirstOrDefault(a => a.Contains("idx="));
            if (!string.IsNullOrWhiteSpace(idxParam))
                host += "&" + idxParam;
            else
            {
                string idx = new Random().Next(-1, 7).ToString();
                host += "&idx=" + idx;
            }

            string url = "";
            string copyright = "";
            string fileName = "";
            string strHtml = "";
            WebClient wc = new WebClient();
            try
            {
                Stream myStream = wc.OpenRead(host);
                if (myStream != null)
                {
                    StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
                    strHtml = sr.ReadToEnd();
                    myStream.Close();

                    string reg1 = "<url>(?<ImgUrl>.*?)</url>";
                    Match matche = Regex.Match(strHtml, reg1);
                    if (matche.Groups["ImgUrl"] != null)
                        url = matche.Groups["ImgUrl"].Value.Trim();

                    string reg2 = "<copyright>(?<Copyright>.*?)</copyright>";
                    Match matche2 = Regex.Match(strHtml, reg2);
                    if (matche2.Groups["Copyright"] != null)
                        copyright = matche2.Groups["Copyright"].Value.Trim();
                }

                if (!string.IsNullOrWhiteSpace(url))
                {
                    int w = Screen.PrimaryScreen.Bounds.Width;
                    int h = Screen.PrimaryScreen.Bounds.Height;

                    if (url.Contains("1366x768"))
                        url = url.Replace("1366x768", w + "x" + h);

                    Uri uri = new Uri(host);
                    url = uri.Scheme + "://" + uri.Host + url;

                    fileName = url.Split('&').FirstOrDefault(a => a.Contains("id=")).Split('=')[1];
                }
            }
            catch
            {
                // ignored
            }

            return Utils.SaveFile(url, "Bing", fileName, strHtml);
        }

        #endregion
    }
}