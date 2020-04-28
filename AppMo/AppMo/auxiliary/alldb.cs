using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace AppMo
{
    public class alldb
    {
        
        public dictionaries Dict = new dictionaries();

        public void CreateDB()
        {
            Dict.CreateAll();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.ReCreate();
                plustags();
                plusactors();
                plusfilms();
                
            }
        }

        private void plusfilms()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int counter = 0;
                int saver = 0;
                foreach (var mov in Dict.movies)
                {
                    db.movies.Add(mov);
                    saver++;
                    if (saver == 60)
                    {
                        saver = 0;
                        db.SaveChanges();
                        break;
                    }
                    counter++;
                }
            }
            Dict.movies.Clear();
        }

        public List<film> FilmList(string name)
        {
            foreach(var mov in Dict.movies)
            {
                GetRec(mov);
            }
            
            List<film> ourlist = new List<film>();
            
            using (ApplicationContext db = new ApplicationContext())
            {
                IEnumerable<film> onemovie =
                from mov in db.movies
                where mov.name == name
                select mov;
                
                ourlist = onemovie.ToList();
            }

            foreach( film movie in ourlist)
            {
                movie.Write();
            }
            return ourlist;
        }

        private void plustags()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int counter = 0;
                int saver = 0;
                foreach (var tag in Dict.tags)
                {
                    db.tags.Add(tag);
                    saver++;
                    if (saver == 20)
                    {
                        saver = 0;
                        db.SaveChanges();
                        break;
                    }
                    counter++;
                }
            }
            Dict.tags.Clear();
        }

        public List<tag> TagList(string name)
        {
            List<tag> ourlist = new List<tag>();
            using (ApplicationContext db = new ApplicationContext())
            {
                IEnumerable<tag> onetag =
                            from tag in db.tags
                            where tag.name == name
                            select tag;
                ourlist = onetag.ToList();
            }
            return ourlist;
        }

        private void plusactors()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int counter = 0;
                int saver = 0;
                foreach (var actor in Dict.actors)
                {
                    db.actors.Add(actor);
                    saver++;
                    if (saver == 2)
                    {
                        saver = 0;
                        db.SaveChanges();
                        break;
                    }
                    counter++;
                }
            }
            Dict.actors.Clear();
        }

        public List<actor> ActorList(string name)
        {
            List<actor> ourlist = new List<actor>();
            using (ApplicationContext db = new ApplicationContext())
            {
                IEnumerable<actor> oneactor =
                from act in db.actors
                where act.name == name
                select act;

                ourlist = oneactor.ToList();


            }
            return ourlist;

        }
        public List<string> ActorTagfilmList(string name)
        {
            List<string> ourlist = new List<string>();
            
                if (Dict.actorname_actorcodesimdb.ContainsKey(name)) 
                    foreach(var code in Dict.actorname_actorcodesimdb[name])
                    {
                        if(Dict.actorcodeimdb_movieset.ContainsKey(code))
                            foreach(var mov in Dict.actorcodeimdb_movieset[code]) { ourlist.Add(mov); }
                    }
                if (Dict.tagname_tagcode.ContainsKey(name))
                    
                            foreach (var mov in Dict.tagcode_movielist[Dict.tagname_tagcode[name]])
                {if (!ourlist.Contains(mov)) ourlist.Add(mov); }
            return ourlist;
        }

        public int Compare(film movie1, film movie2)
        {
            int comp = 0;
            foreach (var actor1 in movie1.Actors.Split("\n"))
            {
                foreach (var actor2 in movie2.Actors.Split("\n"))
                {
                    if (actor1 == actor2)
                    {
                        comp++;
                    }
                }
            }
            
                    if (movie1.director == movie2.director)
                    {
                        comp = comp + 3;
                    }
                
            foreach (var tag1 in movie1.Tags.Split("\n"))
            {
                foreach (var tag2 in movie2.Tags.Split("\n"))
                {
                    if (tag1 == tag2)
                    {
                        comp=comp+2;
                    }
                }
            }
            return comp;
        }

        public void GetRec(film mov)
        {
            var sortinglist = new List<(int, String, String)>();
            //var ourlist = new List<film>();
            
            
            foreach(var movie in ActorTagfilmList(mov.name))
            {
                foreach(var film in Dict.movies)
                {
                    if (film.name == movie)
                        {
                          //ourlist.Add(film);
                        sortinglist.Add((Compare(mov, film), film.TMBD, film.name));
                        }
                }
            }

            sortinglist.Sort(Comparer<(int, String, String)>.Create(new Comparison<(int, String, String)>((x, y) => y.Item1 - x.Item1)));
            for (int j = 0, i = 0; i < 10 && j < sortinglist.Count; ++j)
            {
                
                    if (sortinglist[j].Item2 != null && sortinglist[j].Item2 != "")
                    {
                        mov.Recomended= mov.Recomended+sortinglist[i].Item2+"\t" ;
                        ++i;
                    }
            }
        }

        public void Where()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Where();
            }
        }
    }
}