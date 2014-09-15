using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlotteDLL;

namespace ControleurServeur
{
    class Partie
    {
        Flotte joueur1;
        Flotte joueur2;
        bool partieEstFinie;
        bool tourPremierJoueur;
        // + instance serveur


        public void Partie()
        {
            joueur1 = new Flotte();
            joueur2 = new Flotte();
            partieEstFinie = false;
            tourPremierJoueur = true;
            //instancier le commServeur
        }
        
        public void demarrer()
        {
            //démarrer le commServeur
            //appel fonction connecter(2)
            //...
            //...

            //envoyer message placez btx j1...
            placezVosBateaux(tourPremierJoueur);
            //attendre client
            String message1 = "";
            //lire message du client, contient flotte
            lireFlotte(1, message1);
            //mise a jour grille 1

            //envoyer message placez btx j2...
            placezVosBateaux(!tourPremierJoueur);
            //attendre client
            String message2 = "";
            //lire message du client, contient flotte
            lireFlotte(2, message2);
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
        
        //Méthodes utilisées:

        //affichage d'invite au joueur:
        public String placezVosBateaux(bool premierJoueur)
        {
            String message = "Placez vos bateaux, joueur ";
            if (premierJoueur)
            {
                message += "1:";
            }
            else
            {
                message += "2:";
            }
            message += ": ";

            return message;
        }

        void lireFlotte(int noJoueur, String flotte)
        {
            
            //regex pour récupérer premier mot...
            //si premier mot est "Flotte" alors regex chaque navires.

            //Ex. partiel...
            Pos[] positions = new Pos[5];
            joueur1._flotte[0]._pos = positions;

        }

        void placerFlotteSurGrille(Flotte joueur)
        {
            foreach(Navire navire in joueur._flotte)
            {
                //identifier ses positions sur la grille
                foreach(Pos p in navire._pos)
                {
                    joueur._grille[p._x, p._y] = navire.getNom();// a revoir....
                }
            }
        }
    }
}
