using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMo
{
    public class tag
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Films { get; set; }

        public tag()
        {
            name = "";
            Films = "";
        }
        //public void Write()
        //{
        //    Console.WriteLine("Имя тэга: " + name);
        //    Console.Write("Фильмы с таким тэгом: ");
        //    Console.WriteLine(filmset);
        //    Console.WriteLine();
        //}
    }
}
