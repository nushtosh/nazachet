using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMo
{
    public class actor
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string Films { get; set; }
        public string Professon { get; set; }

        public actor()
        {
            name = "";
            Professon = "";
            code = "";
            Films = "";
        }
        //public void Write()
        //{
        //    Console.WriteLine("Имя Актера: " + name + "  " + code);
        //    Console.Write("Фильмы с этим актером: ");
        //    Console.WriteLine(filmset);
        //    Console.WriteLine();
        //}
    }
}