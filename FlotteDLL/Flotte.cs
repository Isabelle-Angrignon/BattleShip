using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlotteDLL
{
    public class Flotte
    {
        //Définition de la flotte
        const int NBRENAVIRES = 5;
        String[] NOMSNAVIRES = { "Porte-avions", "Croiseur", "Contre-torpilleur", "Sous-marin", "Torpilleur" };
        const int FORMAT = 10;
        int[] TAILLESNAVIRES = { 5, 4, 3, 3, 2 };// va au controleur???

        public String[,] _grille;// contient statut: vide, bateau, manqué, touché ou coulé
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
        }


        //Place les positions des trous d'un navire spécifique.
        //Intrants: no du navire: de 0 à 4
        //          un tableau avec la position de chaque trous.
        public void placerNavire(int noNav, Pos[] positions)
        {
            this._flotte[noNav].placerNavire(positions);

        }
    }
}
