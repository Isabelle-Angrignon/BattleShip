﻿//Flotte.cs
//Isabelle Angrignon

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlotteDLL
{
    public class Flotte
    {
        // Définition de la flotte
        const int NBRENAVIRES = 5;
        String[] NOMSNAVIRES = { "Porte-avions", "Croiseur", "Contre-torpilleur", "Sous-marin", "Torpilleur" };        
        int[] TAILLESNAVIRES = { 5, 4, 3, 3, 2 };
        const int FORMAT = 10; // on ne gère pas plus de 10*10

        public String[,] _grille;// contient les noms de bateaux pour repérage rapide: valeur: soit "" ou "nom_bateau" 
        public Navire[] _flotte;


        public Flotte()
        {
            _grille = new String[FORMAT, FORMAT];
            _flotte = new Navire[NBRENAVIRES];

            //Créer les navires
            for (int noNav = 0; noNav < NBRENAVIRES; ++noNav)
            {
                this._flotte[noNav] = new Navire(TAILLESNAVIRES[noNav], NOMSNAVIRES[noNav]);
            }

            initGrille();

        }

        //initialiser la grille avec des ""
        private void initGrille()
        {
            for (int i = 0; i < FORMAT; ++i)
            {
                for (int j = 0; j < FORMAT; ++j)
                {

                    _grille[i, j] = "";
                }
            }
        }


        //Place les positions des trous d'un navire spécifique.
        //Intrants: no du navire: de 0 à 4
        //          un tableau avec la position de chaque trous.
        public void placerNavire(int noNav, Pos[] positions)
        {
            this._flotte[noNav].placerNavire(positions);

        }

        //Flotte complete: noms et pos (format numérique des positions envoyé par client au serveur)
        public override string ToString()
        {
            String flotte = "";
            //jusqu'à lAvant dernier, il y a une virgule.
            for (int nav = 0; nav < this._flotte.Length; ++nav)
            {
                flotte += this._flotte[nav].ToPos();
            }
            return flotte;
        }
    }
}
