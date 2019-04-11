using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace AutoWallpaper.Lib
{
    public static class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="origin"></param>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SaveFile(string url, string origin,string fileName,string content)
        {
            if (string.IsNullOrWhiteSpace(url))
                return "";

            string savePath = Directory.GetCurrentDirectory() + "\\Img\\" + origin + "\\" + DateTime.Now.Year + "\\";
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            savePath += fileName;
            if (File.Exists(savePath))
                return savePath;

            File.WriteAllText(@savePath + ".txt", content);

            WebClient wc = new WebClient();
            try
            {
                wc.DownloadFile(url, savePath);
            }
            catch (Exception ex)
            {
            }
            if (File.Exists(savePath))
                return savePath;
            return "";
        }

        #region [设置电脑背景图片]

        /// <summary>
        /// 设置电脑背景图片
        /// </summary>
        /// <param name="imgPath"></param>
        public static void SetWallpaper(string imgPath)
        {
            if (string.IsNullOrWhiteSpace(imgPath))
                return;

            RegistryKey myRegKey = Registry.CurrentUser.OpenSubKey(@"Control Panel\\Desktop", true);
            if (myRegKey != null)
            {
                myRegKey.SetValue("TileWallpaper", "0"); //0 居中 1  平铺 默认
                myRegKey.SetValue("WallpaperStyle", "2"); //2 拉伸
                myRegKey.Close(); //关闭该项,并将改动保存到磁盘
            }

            //设置墙纸
            string strPathBmp = Directory.GetCurrentDirectory() + "\\Img\\Current.bmp";
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(imgPath);
            bm.Save(strPathBmp, System.Drawing.Imaging.ImageFormat.Bmp); //把jpg转成bmp 
            SystemParametersInfo(20, 1, strPathBmp, 1); //SPI_SETDESKWALLPAPER
        }

        /// <summary>
        /// 调用电脑底层的接口
        /// </summary>
        /// <param name="uAction"></param>
        /// <param name="uParam"></param>
        /// <param name="lpvParam">图片的路径</param>
        /// <param name="fuWinIni"></param>
        /// <returns></returns>
        /// uAction Long，指定要设置的参数。参考uAction常数表 
        ///uParam Long，参考uAction常数表 
        ///
        ///lpvParam Any，按引用调用的Integer、Long和数据结构。 
        ///
        ///fuWinIni 这个参数规定了在设置系统参数的时候，是否应更新用户设置参数 
        ///
        ///下面是部分uAction参数，和使用它们的方法： 
        ///
        ///参数    意义和使用方法   
        ///
        ///6    设置视窗的大小，SystemParametersInfo(6, 放大缩小值, P, 0)，lpvParam为long型 
        ///
        ///17    开关屏保程序，SystemParametersInfo(17, False, P, 1)，uParam为布尔型 
        ///
        ///13，24    改变桌面图标水平和垂直间距，uParam为间距值(像素)，lpvParam为long型 
        ///
        ///15    设置屏保等待时间，SystemParametersInfo(15, 秒数, P, 1)，lpvParam为long型 
        ///
        ///20    设置桌面背景墙纸，SystemParametersInfo(20, True, 图片路径, 1) 
        ///
        ///93    开关鼠标轨迹，SystemParametersInfo(93, 数值, P, 1)，uParam为False则关闭 
        ///
        ///97    开关Ctrl+Alt+Del窗口，SystemParametersInfo(97, False, A, 0)，uParam为布尔型 
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
        private static extern Int32 SystemParametersInfo(Int32 uAction, Int32 uParam, string lpvParam, Int32 fuWinIni); //////lpvParam要设置成string

        #endregion
    }
}