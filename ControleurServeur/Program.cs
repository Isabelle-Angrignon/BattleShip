using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlotteDLL;


namespace ControleurServeur
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Partie game = new Partie();
            game.demarrer();            
        }  
    }
}
