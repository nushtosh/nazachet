using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AppMo
{
    public class dictionaries
    {
        

        public Dictionary<string, List<string>> moviename_movietags = new Dictionary<string, List<string>>();//1
        public Dictionary<string, string> tagcode_moviename = new Dictionary<string, string>();//2
        public Dictionary<string, string> imdbcode_ml = new Dictionary<string, string>();//3
        public Dictionary<string, string> ml_imdbcode = new Dictionary<string, string>();//4
        public Dictionary<string, string> imdbrating = new Dictionary<string, string>();//5
        public Dictionary<string, Set<string>> actorcodeimdb_movieset = new Dictionary<string, Set<string>>();//6
        public Dictionary<string, Set<pair<string, string>>> movie_actorset = new Dictionary<string, Set<pair<string, string>>>();//7
        public Dictionary<string, Set<string>> actorname_actorcodesimdb = new Dictionary<string, Set<string>>();//8
        public Dictionary<string, string> actorcode_actorname = new Dictionary<string, string>();//9
        public Dictionary<string, List<string>> tagcode_movielist = new Dictionary<string, List<string>>();//12
        public Dictionary<string, List<string>> moviecode_taglist = new Dictionary<string, List<string>>();//13
        public Dictionary<string, string> tagcode_tagname = new Dictionary<string, string>();//10
        public Dictionary<string, string> tagname_tagcode = new Dictionary<string, string>();  //11


        public List<film> movies = new List<film>();
        public List<actor> actors = new List<actor>();
        public List<tag> tags = new List<tag>();


        public void Fmoviecodes_imbd()
        {
            using (StreamReader sr = File.OpenText(@"C:\Users\olya\Documents\ml-latest\ml-latest\MovieCodes_IMDB.tsv"))
            {
                string line = "";
                string tcode;
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] text = line.Split('\t');
                    if (line.Contains("RU") || line.Contains("US"))//only russian or english movies
                    {
                        tcode = text[0];
                        string moviename = text[2];
                        if (!tagcode_moviename.ContainsKey(tcode))
                        {
                            tagcode_moviename.Add(tcode, moviename);
                        }
                        if (!moviename_movietags.ContainsKey(moviename))
                        {
                            List<string> movielist = new List<string>();
                            movielist.Add(tcode);
                            moviename_movietags.Add(moviename, movielist);
                        }
                        else
                        {

                            moviename_movietags[moviename].Add(tcode);
                        }
                    }
                }
            }
            Console.WriteLine("MovieName");
        }

        //public void Flinksimdbml()
        //{
        //    using (StreamReader sr = File.OpenText(@"C:\Users\olya\Documents\ml-latest\ml-latest\links_IMDB_MovieLens.csv"))
        //    {
        //        string line = "";
        //        string movie;
        //        string imdb;
        //        sr.ReadLine();
        //        while ((line = sr.ReadLine()) != null)
        //        {
        //            string[] text = line.Split('.', ',');

        //            movie = text[0];
        //            imdb = "tt" + text[1];
        //            imdbcode_ml.Add(imdb, movie);
        //            ml_imdbcode.Add(movie, imdb);
        //        }
        //    }
        //    Console.WriteLine("IMBDMovie");
        //}

        //public void Fratingsimdb()
        //{
        //    using (StreamReader sr = File.OpenText(@"C:\Users\olya\Documents\ml-latest\ml-latest\Ratings_IMDB.tsv"))
        //    {
        //        string line = "";
        //        sr.ReadLine();
        //        while ((line = sr.ReadLine()) != null)
        //        {
        //            string[] text = line.Split('\t');
        //            imdbrating.Add(text[0], text[1]);
        //        }
        //    }
        //    Console.WriteLine("Rating");
        //}

        public void Factorsdircodes()
        {
            using (StreamReader sr = File.OpenText(@"C:\Users\olya\Documents\ml-latest\ml-latest\ActorsDirectorsCodes_IMDB.tsv"))
            {
                Random rand = new Random();
                string line = "";
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string id = line.Substring(0, 9);
                    if (tagcode_moviename.ContainsKey(id))
                    {

                        string[] text = line.Split('\t');

                        string profession = text[3];
                        string IMBD = text[0];
                        string ActorID = text[2];
                        if (actorcodeimdb_movieset.ContainsKey(ActorID))
                        {
                            actorcodeimdb_movieset[ActorID].Add(IMBD);
                        }
                        else
                        {
                            Set<string> films = new Set<string>();
                            films.Add(IMBD);
                            actorcodeimdb_movieset.Add(ActorID, films);
                        }

                        if (movie_actorset.ContainsKey(IMBD))
                        {
                            pair<string, string> act = new pair<string, string>(ActorID, profession);
                            movie_actorset[IMBD].Add(act);
                        }
                        else
                        {
                            Set<pair<string, string>> actors = new Set<pair<string, string>>();
                            pair<string, string> act = new pair<string, string>(ActorID, profession);
                            actors.Add(act);
                            movie_actorset.Add(IMBD, actors);
                        }
                    }
                }
            }
            Console.WriteLine("ActorProfession");
        }

        public void Factorsdirnames()
        {
            using (StreamReader sr = File.OpenText(@"C:\Users\olya\Documents\ml-latest\ml-latest\ActorsDirectorsNames_IMDB.txt"))
            {
                string line = "";

                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string id = line.Substring(0, 9);
                    if (actorcodeimdb_movieset.ContainsKey(id))
                    {
                        string[] text = line.Split('\t');
                        string actorcode = text[0];

                        string actorname = text[1];
                        actorcode_actorname.Add(actorcode, actorname);
                        if (actorname_actorcodesimdb.ContainsKey(actorname))
                        {
                            actorname_actorcodesimdb[actorname].Add(actorcode);
                        }
                        else
                        {
                            Set<string> actors = new Set<string>();
                            actors.Add(actorcode);
                            actorname_actorcodesimdb.Add(actorname, actors);
                        }

                    }
                }
            }
            Console.WriteLine("ActorName");
        }

        //public void Ftagml()
        //{
        //    using (StreamReader sr = File.OpenText(@"C:\Users\olya\Documents\ml-latest\ml-latest\TagScores_MovieLens.csv"))
        //    {
        //        string line = "";
        //        sr.ReadLine();
        //        while ((line = sr.ReadLine()) != null)
        //        {
        //            string[] text = line.Split(',');
        //            if ((text[2].CompareTo("0.5") >= 0))
        //            {
        //                List<string> list1 = new List<string>();
        //                List<string> list2 = new List<string>();
        //                if (tagcode_movielist.ContainsKey(text[1]))
        //                {
        //                    tagcode_movielist[text[1]].Add(text[0]);
        //                }
        //                else
        //                {
        //                    list2.Add(text[0]);
        //                    tagcode_movielist.Add(text[1], list2);
        //                }

        //                if (moviecode_taglist.ContainsKey(text[0]))
        //                {
        //                    moviecode_taglist[text[0]].Add(text[1]);
        //                }
        //                else
        //                {
        //                    list1.Add(text[1]);
        //                    moviecode_taglist.Add(text[0], list1);
        //                }
        //            }

        //        }
        //    }
        //    Console.WriteLine("MovieTag");
        //}

        //public void TagName()
        //{
        //    using (StreamReader sr = File.OpenText(@"C:\Users\olya\Documents\ml-latest\ml-latest\TagCodes_MovieLens.csv"))
        //    {
        //        string line = "";
        //        sr.ReadLine();
        //        while ((line = sr.ReadLine()) != null)
        //        {
        //            string[] text = line.Split(',');
        //            tagcode_tagname.Add(text[0], text[1]);
        //            tagname_tagcode.Add(text[1], text[0]);
        //        }
        //    }
        //    Console.WriteLine("TagTagName");
        //}

        //public void movie(string line)
        //{
        //    string rating;
        //    string MLcode;
        //    string actorcode;
        //    string actordirector;
        //    foreach (var item in moviename_movietags[line])
        //    {
        //        Console.WriteLine("название: ", line);
        //        MLcode = imdbcode_ml[item];
        //        rating = imdbrating[item];
        //        Console.WriteLine("рейтинг:" + rating);
        //        Console.WriteLine("актёры:");
        //        foreach (var k in movie_actorset[item])
        //        {
        //            actorcode = k.first;
        //            actordirector = k.second;

        //            Console.WriteLine(actorcode_actorname[actorcode] + "профессия: " + actordirector);
        //        }

        //        foreach (var tag in moviecode_taglist[MLcode])
        //        {
        //            Console.WriteLine("теги: ");
        //            Console.WriteLine(tagcode_tagname[tag]);
        //        }

        //    }
        //}

        public void actor(string line)
        {

            foreach (var k in actorname_actorcodesimdb[line])
            {
                Console.WriteLine(line);
                Console.WriteLine("фильмы: ");
                foreach (var item in actorcodeimdb_movieset[k])
                {

                    Console.WriteLine(tagcode_moviename[item]);
                }
            }
        }

        //public void tag(string line)
        //{
        //    string tagcode = tagname_tagcode[line];
        //    Console.WriteLine("фильмы: ");
        //    foreach (var movie in tagcode_movielist[tagcode])
        //    {
        //        string IMBD = ml_imdbcode[movie];
        //        Console.WriteLine(tagcode_moviename[IMBD]);
        //    }

        //}

        public void CreateMovies()
        {


            string ActID;
            string prof;
            string movielens;
            int i = 0;
            ICollection<string> moviename = moviename_movietags.Keys;
            foreach (string s in moviename)
            {

                foreach (var IMBD in moviename_movietags[s])
                {
                    string actors = "";
                    string tags = "";
                    film clip = new film();
                    clip.name = s;
                    clip.code = IMBD;
                    if (imdbrating.ContainsKey(IMBD))
                    {
                        clip.rating = imdbrating[IMBD];
                    }
                    if (movie_actorset.ContainsKey(IMBD))
                    {
                        foreach (var id in movie_actorset[IMBD])
                        {
                            ActID = id.first;
                            prof = id.second;
                            if (actorcode_actorname.ContainsKey(ActID))
                            {
                                if (prof != "director")
                                    actors += actorcode_actorname[ActID] + "\t";
                                else
                                    clip.director = actorcode_actorname[ActID];
                            }
                        }
                    }
                    if (imdbcode_ml.ContainsKey(IMBD))
                    {
                        movielens = imdbcode_ml[IMBD];
                        if (moviecode_taglist.ContainsKey(movielens))
                        {
                            foreach (var tagid in moviecode_taglist[movielens])
                            {
                                tags += tagcode_tagname[tagid] + "\t";
                            }
                        }
                    }
                    clip.Actors = actors;
                    clip.Tags = tags;
                    clip.id = i;
                    i++;
                    movies.Add(clip);

                }

            }
            Console.WriteLine("filmdictionary done ");
        }

        public void Filmfinder(string name)
        {
            List<film> filmfinder = new List<film>();
            CreateMovies();
            foreach (film ourfilm in movies)
            {
                if (ourfilm.name == name) filmfinder.Add(ourfilm);
            }
            foreach (film ourfilm in filmfinder)
            {
                Console.WriteLine(ourfilm.name);
                Console.WriteLine(ourfilm.director);
                Console.WriteLine(ourfilm.rating);

            }
        }

        public void CreateTags()
        {
            string TagId;
            string IMBD;

            ICollection<string> tagname = tagname_tagcode.Keys;
            foreach (string t in tagname)
            {
                string films = "";
                tag hashtag = new tag();
                hashtag.name = t;

                TagId = tagname_tagcode[t];
                if (tagcode_movielist.ContainsKey(TagId))
                {
                    foreach (var film in tagcode_movielist[TagId])
                    {
                        IMBD = ml_imdbcode[film];
                        if (tagcode_moviename.ContainsKey(IMBD))
                        {
                            films += tagcode_moviename[IMBD] + "\t";
                        }
                    }
                }
                hashtag.Films = films;

                tags.Add(hashtag);
            }
            Console.WriteLine("tagdictionary done");
        }

        public void CreateActors()
        {

            int i = 1;
            ICollection<string> actname = actorname_actorcodesimdb.Keys;
            foreach (string actor in actname)
            {
                foreach (var actorid in actorname_actorcodesimdb[actor])
                {
                    actor act = new actor();
                    string films = "";
                    act.name = actor;
                    act.code = actorid;

                    if (actorcodeimdb_movieset.ContainsKey(actorid))
                    {
                        foreach (var imdb in actorcodeimdb_movieset[actorid])
                        {
                            if (tagcode_moviename.ContainsKey(imdb))
                            {
                                films += "" + tagcode_moviename[imdb] + "\t";
                            }
                        }
                        act.Films = films;
                        act.id = i;

                        i++;
                        actors.Add(act);
                    }
                }

            }
            Console.WriteLine("actordictionary done");
        }

        

        public void CreateAll()
        {
            Fmoviecodes_imbd();
            Console.WriteLine("название фильма");

            Factorsdircodes();
            Console.WriteLine("Id актёра или режиссёра");
            Factorsdirnames();
            Console.WriteLine("Id имя актёра");

            CreateandClear();
        }

        public void CreateandClear()
        {
            CreateActors();
            tagcode_moviename.Clear();
            actorcodeimdb_movieset.Clear();
            actorname_actorcodesimdb.Clear();
        }
    }
}
