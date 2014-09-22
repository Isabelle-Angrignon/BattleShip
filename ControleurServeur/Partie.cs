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
        String joueur1;
        String joueur2;
        Flotte flotte1;
        Flotte flotte2;
        Flotte flotteAttaquee;
        bool partieEstFinie;
        bool tourPremierJoueur;

        // + instance serveur


        public void Partie()
        {
            joueur1 = "Joueur 1";
            joueur2 = "Joueur 2";
            flotte1 = new Flotte();
            flotte2 = new Flotte();
            flotteAttaquee = flotte2;
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
                //recevoir une pos
                //analyserTir = recoit un string
                //avertir les joueurs  retourne le string 
                //passer la main
                //rafraichir les grilles des joueurs

                //alterner joueur;
                tourPremierJoueur = !tourPremierJoueur;
                //changer la flotte attaquee
                flotteAttaquee = accesFlotteAttaquee();
            }
            //afficher gagnant aux deux clients
        }
        
        //Méthodes utilisées:

        //analyserTir
        String analyserTir(Pos tir)
        {
            String resultat = "";
            //vérifie si l'espace est occupée
            if (CaseTireeEstOccupee(tir))
            {
                Navire navireAttaque = trouverNavireAttaque(nombateauToucheEst(tir));
                //marquer touché 
                toucherBateau(navireAttaque, tir);
                    //Vérifie si bateau coulé
                
                if (bateauEstCoule(navireAttaque))
                {
                    //coulerBateau(nombateau, nojoueur)
                    navireAttaque._estCoule = true;
                    //Vérifie si tous les bateaux coulés
                    partieEstFinie = PartieEstTerminee(flotteAttaquee);
                    resultat = "Coulé=" + navireAttaque.getNom();
                    if (partieEstFinie)
                    {
                        resultat = ""+ tir._x + tir._y; //position qui a fait ganger
                    }
                } 
                else
                {
                    //touché, non coulé
                    resultat = "Touché=" + tir._x + tir._y;
                }
            }
            else
            {
                resultat = "Manqué=" + tir._x + tir._y;
            }
              
            return resultat;
        }


        //change le string a toutes les positions du bateau pour ajouter "coule"
        //pour le joueur attaqué.
        bool CaseTireeEstOccupee(Pos p)
        {
            return !(flotteAttaquee._grille[p._x, p._y] == "");
        }

        bool bateauEstCoule(Navire nav)
        {
            bool estCoule = true;
            for (int p = 0; p < nav._pos.Length && estCoule == true; ++p )
            {
                if (nav._pos[p]._estTouche == false)
                {
                    estCoule = false;
                }
            }
            return estCoule;
        }

        bool PartieEstTerminee(Flotte flotte)
        {
            bool estTerminee = true;
            for (int n = 0; n < flotte._flotte.Length && estTerminee == true; ++n)
            {
                if (flotte._flotte[n]._estCoule == false)
                {
                    estTerminee = false;
                }
            }
            return estTerminee;
        }

        //recuperer nom bateau
        String nombateauToucheEst(Pos p)
        {
            return flotteAttaquee._grille[p._x, p._y].ToString();
        }

        
        void toucherBateau(Navire nav, Pos p)
        {
            int indice  = trouverIndicePosition(nav, p);
           nav._pos[indice]._estTouche = true;
        }

        int trouverIndicePosition(Navire nav, Pos p)
        {
            int indice = 0; 
            while (nav._pos[indice] != p && indice < nav._pos.Length)
            {
                ++indice;
            }
            if (indice >= nav._pos.Length)
            {
                //position non trouvé!!!!-------------------
                return -1;
            }
            return indice;
        }

        Navire trouverNavireAttaque(String nombateau)
        {
            Flotte flotte = accesFlotteAttaquee();
            int nav = 0;
            while (flotte._flotte[nav].getNom() != nombateau && nav < flotte._flotte.Length)
            {
                ++nav;
            }
            if (nav >= flotte._flotte.Length)
            {
                //navire non trouvé!!!!-------------------
                return null;
            }
            else
            {                
                return flotte._flotte[nav];
            }           
        }

        String accesJoueurAttaquant()// celui qui gagne...
        {
            String nom="";
            if (tourPremierJoueur)// si ce n'est pas le tour du premier joueur, c'est lui qui est attaqué. 
            {
                nom = joueur1;
            }
            else
            {
                nom = joueur2;
            }
            return nom;
        }
        
        //retourne la flotte dont c'est le tour a jouer
        Flotte accesFlotteAttaquee()
        {
            Flotte f;
            if (!tourPremierJoueur)// si ce n'est pas le tour du premier joueur, c'est lui qui est attaqué. 
            {
                f = flotte1;
            }
            else 
            {
                f = flotte2;
            }
            return f;
        }        
                

        //affichage d'invite au joueur:
        String placezVosBateaux(bool premierJoueur)
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

        //lorsque le client envoi tous ses bateaux...
        Flotte lireFlotte(int noJoueur, String flotte)
        {
            Flotte f = new Flotte();
            //regex pour récupérer premier mot...
            //si premier mot est "Flotte" alors regex chaque navires.

            //Ex. partiel...
            Pos[] positions = new Pos[5];
            f._flotte[0]._pos = positions;

            return f;
        }


        //sert 2 fois au début.
        void placerFlotteSurGrille(Flotte joueur)
        {
            foreach(Navire navire in joueur._flotte)
            {
                //identifier ses positions sur la grille
                foreach(Pos p in navire._pos)
                {
                    joueur._grille[p._x, p._y] = navire.getNom();// sera append... avec _touche_coule
                }
            }
        }        
    }
}
