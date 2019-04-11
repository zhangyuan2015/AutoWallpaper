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
            var idx = args.Any() ? args[0] : new Random().Next(-1, 7).ToString();
            string host = "http://cn.bing.com/HPImageArchive.aspx?idx=" + idx + "&n=1";

            string url = "";
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
                }

                if (!string.IsNullOrWhiteSpace(url))
                {
                    int w = Screen.PrimaryScreen.Bounds.Width;
                    int h = Screen.PrimaryScreen.Bounds.Height;

                    if (url.Contains("1366x768"))
                        url = url.Replace("1366x768", w + "x" + h);

                    Uri uri = new Uri(host);
                    url = uri.Scheme + "://" + uri.Host + url;
                }
            }
            catch
            {
                // ignored
            }

            return Utils.SaveFile(url, "Bing", Path.GetFileName(url), strHtml);
        }

        #endregion
    }
}