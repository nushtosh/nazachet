using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AppMo
{
    public class TMDBParser
    {
        public string Information { get; set; }
        private string imgstart = "https://image.tmdb.org/t/p/w200";
        public string Image { get; set; }
        public List<(string, string)> Recommends { get; set; }

        public TMDBParser(film movie)
        {
            Recommends = new List<(string, string)>();
            var uri = "https://api.themoviedb.org/3/movie/" + movie.TMBD + "?api_key=12cc22e510ea8204c404498269c06dee&language=en-US";
            var request = (HttpWebRequest)WebRequest.Create(uri);
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                var parsed = responseFromServer.Split("\"overview\":\"");
                Information = parsed[1].Split("\"popularity\"")[0];
                var poster = responseFromServer.Split("\"poster_path\":\"");
                if (poster.Length != 1)
                {
                    string imageName = poster[1].Split("\"")[0];
                    Image = imgstart + imageName;
                }
                var recommended = movie.Recomended.Split('\t');
                foreach (var rec in recommended)
                {
                    if (rec != "")
                    {
                        Recommends.Add(getNmNdPst(rec));
                    }
                }
            }
        }


        public (string, string) getNmNdPst(string TMDB)
        {
            var movUri = "https://api.themoviedb.org/3/movie/" +
                TMDB +
                "?api_key=12cc22e510ea8204c404498269c06dee&language=en-US";
            var pic = "";
            var name = "";
            var request = (HttpWebRequest)WebRequest.Create(movUri);
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string alikeResp = reader.ReadToEnd();
                response.Close();
                reader.Close();
                stream.Close();
                var arr = alikeResp.Split("\"original_title\":\"");
                var arr2 = arr[1].Split("\"");
                var arr3 = alikeResp.Split("\"poster_path\":\"");
                if (arr3.Length != 1)
                {
                    string imgend = arr3[1].Split("\"")[0];
                    pic = imgstart + imgend;
                }
                name = arr2[0];
            }
            return (name, pic);
        }
    }

}
