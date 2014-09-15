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
            Flotte joueur1 = new Flotte();
            Flotte joueur2 = new Flotte();
            bool partieEstFinie = false;
            bool tourPremierJoueur = true;


            //placez btx j1...
            //mise a jour grille 1
            //placez btx j2...
            //mise a jour grille 2

            //tant que partie 
            while (!partieEstFinie)
            {
                //demander de lancer une torpille;
                //analyser le coup
                    //vérifier si touché
                        //vérifier si coulé
                            //vérifier si gagné
                //rafraichir les grilles des joueurs



                //alterner joueur;
                tourPremierJoueur = !tourPremierJoueur;
            }
            //afficher gagnant
        }    
    }
}
