using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMo
{
    public class film
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string Actors { get; set; }
        public string Tags { get; set; }
        public string director { get; set; }
        public string rating { get; set; }
        public string TMBD { get; set; }
        public string Recomended { get; set; }




        public film()
        {
            name = "";
            director = "";
            rating = "";
            Actors = "";
            Tags = "";
            code = "";
            TMBD = "";
        }

        

        public void Write()
        {
            Console.WriteLine("Название фильма: " + name);
            Console.WriteLine("Режиссер: " + director);
            Console.WriteLine("Рейтинг: " + rating);
            Console.Write("Актеры: ");
            Console.WriteLine(Actors);
            Console.Write("Тэги: ");
            Console.WriteLine(Tags);
            Console.WriteLine();
        }

        //public override string ToString()
        //{
        //    Write();
        //    return "";
        //}
    }
}
