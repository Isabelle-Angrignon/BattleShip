using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlotteDLL;
using CommServeur;

namespace ControleurServeur
{
    class Partie
    {
        String _joueur1;
        String _joueur2;
        Flotte _flotte1;
        Flotte _flotte2;


        Flotte _flotteAttaquee;
        int _attaquant;   // contient 1 ou 2 
        bool _partieEstFinie;
        bool _tourPremierJoueur; // true = 1 false = 2

        // + instance serveur
        CommunicationServeur _comVersClient;


        public Partie()
        {
            _joueur1 = "Joueur 1";
            _joueur2 = "Joueur 2";
            _flotte1 = new Flotte();
            _flotte2 = new Flotte();
            _flotteAttaquee = _flotte2;            
            _partieEstFinie = false;
            _tourPremierJoueur = true;
            _comVersClient = new CommunicationServeur("127.0.0.1", 8088);
            _attaquant = 1;
        }

        void preparerPartie()
        {
            //démarrer le commServeur
            _comVersClient.Connection(2);

            _comVersClient.LireMessage(1);
            _comVersClient.LireMessage(2);

            //////////////   JOUEUR 1   ///////////
            //envoyer message placez btx j1...
            _comVersClient.EnvoyerMessage(1, "Placer");
            _comVersClient.EnvoyerMessage(2, "Attendre");
            //attendre client            
            String flotteJ1 = _comVersClient.LireMessage(1);
            //Interpréter le messagge contient flotte: reconstruit l'objet
            _flotte1 = lireFlotte(flotteJ1);

            //////////////   JOUEUR 2   ///////////
            _comVersClient.LireMessage(2);
            //envoyer message placez btx j2...
            _comVersClient.EnvoyerMessage(2, "Placer");
            _comVersClient.EnvoyerMessage(1, "Attendre");
            //attendre client            
            String flotteJ2 = _comVersClient.LireMessage(2);
            //Interpréter le messagge contient flotte: reconstruit l'objet
            _flotte2 = lireFlotte(flotteJ2);
            _comVersClient.LireMessage(1);
        }
        
        public void demarrer()
        {
            preparerPartie();            

            //tant que partie pas finie 
            while (!_partieEstFinie)
            {    
                //demander de lancer une torpille;
                _comVersClient.EnvoyerMessage(_attaquant, "Attaque");
                //recevoir une pos recoit un string  retourne une pos
                String tir = _comVersClient.LireMessage(_attaquant);
                //analyserTir = 
                String resultatTir = analyserTir(posDuTirAttaque(tir)); 
                if(_partieEstFinie)
                {
                    _comVersClient.EnvoyerMessage(_attaquant, "Gagnant=" + resultatTir);
                    alternerJoueur();
                    _comVersClient.EnvoyerMessage(_attaquant, "Perdant=" + resultatTir);
                    //lire réponse1
                    //lire réponse2
                    //redémarrer partie???   
                    //réinitiaiser tout ou ...
                    // System.Diagnostics.Process.Start(Application.ExecutablePath);                    
                }
                else
                {
                    //avertir les joueurs  retourne le string 
                    _comVersClient.EnvoyerMessage(_attaquant, resultatTir);
                    _comVersClient.LireMessage(_attaquant);
                    alternerJoueur();
                    _comVersClient.EnvoyerMessage(_attaquant, resultatTir);
                    _comVersClient.LireMessage(_attaquant);                
                }
                //on recommence... 
            }            
        }
        
        //Méthodes utilisées:

        Pos posDuTirAttaque(String tir)
        {
            Pos p = new Pos();  
            p._x = int.Parse(tir[0].ToString());
            p._y = int.Parse(tir[1].ToString());
            return p;
        }

        void alternerJoueur()
        {
            if(_attaquant == 1)
            {
                _attaquant = 2;
            }
            else
            {
                _attaquant = 1;
            }
            _tourPremierJoueur = !_tourPremierJoueur;
            //changer la flotte attaquee
            _flotteAttaquee = accesFlotteAttaquee();
        }



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
                    _partieEstFinie = PartieEstTerminee(_flotteAttaquee);
                    resultat = "Coulé=" + navireAttaque.getNom();
                    if (_partieEstFinie)
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
            return !(_flotteAttaquee._grille[p._x, p._y] == "");
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
            return _flotteAttaquee._grille[p._x, p._y].ToString();
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
            if (_tourPremierJoueur)// si ce n'est pas le tour du premier joueur, c'est lui qui est attaqué. 
            {
                nom = _joueur1;
            }
            else
            {
                nom =_joueur2;
            }
            return nom;
        }
        
        //retourne la flotte dont c'est le tour a jouer
        Flotte accesFlotteAttaquee()
        {
            Flotte f;
            if (!_tourPremierJoueur)// si ce n'est pas le tour du premier joueur, c'est lui qui est attaqué. 
            {
                f = _flotte1;
            }
            else 
            {
                f = _flotte2;
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
        Flotte lireFlotte(String flotte)
        {
            Flotte f = new Flotte();
            //regex pour récupérer premier mot...
            //si premier mot est "Flotte" alors regex chaque navires.

            //Ex. partiel...
            Pos[] positions = new Pos[5];
            f._flotte[0]._pos = positions;
            placerFlotteSurGrille(f);
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
