using System.Collections.Generic;

namespace AutoWallpaper.Lib.Unsplash
{
    public class UnsplashImgModel
    {
        public string id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string color { get; set; }
        public string slug { get; set; }
        public int downloads { get; set; }
        public int likes { get; set; }
        public int views { get; set; }
        public bool liked_by_user { get; set; }
        public string description { get; set; }
        public exif exif { get; set; }
        public List<string> current_user_collections { get; set; }
        public urls urls { get; set; }
        public List<string> categories { get; set; }
        public links links { get; set; }
        public user user { get; set; }
    }

    public class exif
    {
        public string make { get; set; }
        public string model { get; set; }
        public string exposure_time { get; set; }
        public string aperture { get; set; }
        public string focal_length { get; set; }
        public int iso { get; set; }
    }

    public class urls
    {
        public string raw { get; set; }
        public string full { get; set; }
        public string regular { get; set; }
        public string small { get; set; }
        public string thumb { get; set; }

        public string custom { get; set; }
    }

    public class links
    {
        public string self { get; set; }
        public string html { get; set; }
        public string download { get; set; }
        public string download_location { get; set; }
    }

    public class user
    {
        public string id { get; set; }
        public string updated_at { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string twitter_username { get; set; }
        public string portfolio_url { get; set; }
        public string bio { get; set; }
        public string location { get; set; }
        public int total_likes { get; set; }
        public int total_photos { get; set; }
        public int total_collections { get; set; }
        public user_profile_image profile_image { get; set; }
        public user_links links { get; set; }
    }

    public class user_profile_image
    {
        public string small { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
    }

    public class user_links
    {
        public string self { get; set; }
        public string html { get; set; }
        public string photos { get; set; }
        public string likes { get; set; }
        public string portfolio { get; set; }
        public string following { get; set; }
        public string followers { get; set; }
    }
}