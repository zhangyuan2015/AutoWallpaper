using AutoWallpaper.Lib;
using AutoWallpaper.Lib.Bing;
using AutoWallpaper.Lib.Unsplash;
using System.Linq;

namespace AutoWallpaper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("Bing"))
            {
                Utils.SetWallpaper(BingServiceImpl.GetImgPath(args));
            }
            else
            {
                Utils.SetWallpaper(UnsplashServiceImpl.GetImgPath(args));
            }
        }
    }
}