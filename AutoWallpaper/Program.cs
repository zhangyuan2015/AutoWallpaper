using AutoWallpaper.Lib;
using AutoWallpaper.Lib.Unsplash;

namespace AutoWallpaper
{
    class Program
    {
        static void Main(string[] args)
        {
            Utils.SetWallpaper(UnsplashServiceImpl.GetImgPath());
        }
    }
}