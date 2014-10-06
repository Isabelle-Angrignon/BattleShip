//Partie.cs
//Isabelle Angrignon

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
        //String _joueur1;
        //String _joueur2;
        Flotte _flotte1;
        Flotte _flotte2;

        Flotte _flotteAttaquee;
        int _attaquant;   // contient 1 ou 2 
        bool _partieEstFinie;
        bool _tourPremierJoueur; // true = tour du joueur 1 et false = tour du joueur 2
                
        CommunicationServeur _comVersClient;
        
        //Constructeur
        public Partie()
        {
            //Initialisation des attributs
            //_joueur1 = "Joueur 1";
            //_joueur2 = "Joueur 2";
            _flotte1 = new Flotte();
            _flotte2 = new Flotte();            
            _partieEstFinie = false;
            _tourPremierJoueur = true;
            _comVersClient = new CommunicationServeur("0.0.0.0", 8888);
            _attaquant = 1;
        }

        //Méthode qui gère la réception des flottes des joueurs
        void preparerPartie()
        {
            //démarrer le commServeur
            _comVersClient.Connection(2);

            //////////////   JOUEUR 1   ///////////
            //Recevoir flotte               
            String flotteJ1 = _comVersClient.LireMessage(1);
            System.Console.WriteLine("Flotte 1 recue");
            //Interpréter le message contient flotte: reconstruit l'objet
            _flotte1 = lireFlotte(flotteJ1);
            System.Console.WriteLine("Flotte 1 traitee");

            //////////////   JOUEUR 2   ///////////            
            //Recevoir flotte          
            String flotteJ2 = _comVersClient.LireMessage(2);
            System.Console.WriteLine("Flotte 2 recue");
            //Interpréter le message contient flotte: reconstruit l'objet
            _flotte2 = lireFlotte(flotteJ2);
            System.Console.WriteLine("Flotte 2 traitee");
            
            //Définir la première flotte attaquée:
            //Comme le joueur 1 est attaquant, le joueur 2 est attaqué.
            _flotteAttaquee = _flotte2;            
            //Donner le signal d'attaquer au premier jouer                                      //Suivi des communications au comServeur...
            _comVersClient.EnvoyerMessage(_attaquant, "Attaque");                                   //envoyer  "attaque"
        }

        public void demarrer()
        {
            preparerPartie();

            //tant que partie pas finie 
            while (!_partieEstFinie)
            {
                String resultatTir ="";
                //recevoir une position au format string  
                String tir = _comVersClient.LireMessage(_attaquant);                                    // lire "tir"  de l'attaquant
                //vérifier si veut quitter
                if(veutQuitter(tir))
                {
                    _partieEstFinie = true;
                    System.Console.WriteLine("Abandon du joueur " + _attaquant ); 
                    //Alterne joueur pour qu'on affiche le bon gagnant plus bas...
                    alternerJoueur();                    
                }
                else
                { 
                    //analyserTir = On traduit d'abord le string en "pos"
                    resultatTir = analyserTir(stringToPos(tir));
                    System.Console.WriteLine("résultat tir = " + resultatTir);
                }

                //Après analyse, on vérifie si la partie est finie
                if (_partieEstFinie)
                {
                    _comVersClient.EnvoyerMessage(_attaquant, "Gagnant=" + resultatTir);
                    alternerJoueur();
                    _comVersClient.EnvoyerMessage(_attaquant, "Perdant=" + resultatTir);
                    System.Console.WriteLine("Gagnant = " + _attaquant);                                       
                }
                else
                {
                    //avertir les joueurs, on retourne le string du résultat
                    _comVersClient.EnvoyerMessage(_attaquant, resultatTir);                                 // envoyer  "resultat" à l'attaquant
                    _comVersClient.LireMessage(_attaquant);                                                 // lire  "ok"  envoyé par l'attaquant
                    alternerJoueur();                                                                       // Alterner "joueur"                
                    _comVersClient.EnvoyerMessage(_attaquant, resultatTir);                                 // envoyer "resultat " à l'attaqué qui devient attaquant                
                }
                //on recommence...                                                                          // on recommence... 
            }
        }




        //Méthodes utilisées:

        //Recoit les valeurs numériques sous forme de String et les mets sous forme de notre objet Pos
        //Ne pourrais être utilisé pour des positions de plus de 10 ( de 0 à 9)
        Pos stringToPos(String tir)
        {
            Pos p = new Pos();
            p._x = int.Parse(tir[0].ToString());
            p._y = int.Parse(tir[1].ToString());
            return p;
        }

        //Vérifie si le joueur a demandé de quitter
        bool veutQuitter(String message)
        {
            bool veutQuitter = false;
            if (message =="Quitte")
            {
                veutQuitter = true;
            }
            return veutQuitter;
        }

        //Alterne la flotte attaqué, le numéro du joueur attaquant et le booléen de tour
        void alternerJoueur()
        {
            if (_attaquant == 1)
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


        //Analyse le tir et retourne un message selon le protocol établi avec le client
        String analyserTir(Pos tir)
        {
            String resultat = "";
            //vérifie si l'espace est occupée
            if (CaseTireeEstOccupee(tir))
            {
                //Trouver le navire attaqué dans la flotte attaquée
                Navire navireAttaque = trouverNavireAttaque(nombateauToucheEst(tir));
                //marquer le navire touché 
                toucherBateau(navireAttaque, tir);

                //Vérifie si navire est coulé
                if (bateauEstCoule(navireAttaque))
                {
                    //marquer le navire coulé
                    navireAttaque._estCoule = true;
                    //Vérifie si tous les bateaux coulés
                    _partieEstFinie = PartieEstTerminee(_flotteAttaquee);
                    resultat = "Coule=" + navireAttaque.ToPos();
                    if (_partieEstFinie)
                    {
                        resultat = "" + tir._x + tir._y; //position qui a fait ganger
                    }
                }
                else
                {
                    //touché, non coulé
                    resultat = "Touche=" + tir._x + tir._y;
                }
            }
            else
            {
                resultat = "Manque=" + tir._x + tir._y;
            }
            return resultat;
        }


        //change le string a toutes les positions du bateau pour ajouter "coule"
        //pour le joueur attaqué.
        bool CaseTireeEstOccupee(Pos p)
        {
            return !(_flotteAttaquee._grille[p._x, p._y] == "");
        }

        //Vérifie si toutes les positions du navire sont touchées
        bool bateauEstCoule(Navire nav)
        {
            bool estCoule = true;
            for (int p = 0; p < nav._pos.Length && estCoule == true; ++p)
            {
                if (nav._pos[p]._estTouche == false)
                {
                    estCoule = false;
                }
            }
            return estCoule;
        }


        //Vérifier si tous les navires sont coulés
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

        //recuperer nom du bateau dans la grille
        String nombateauToucheEst(Pos p)
        {
            return _flotteAttaquee._grille[p._x, p._y].ToString();
        }

        void toucherBateau(Navire nav, Pos p)
        {
            int indice = trouverIndicePosition(nav, p);
            nav._pos[indice]._estTouche = true;
        }

        //Trouver où cette position est (son indice) dans le tableau de positions du navire.
        int trouverIndicePosition(Navire nav, Pos p)
        {
            int indice = 0;
            while ((nav._pos[indice]._x != p._x || nav._pos[indice]._y != p._y) && indice < nav._pos.Length)
            {
                ++indice;
            }
            if (indice >= nav._pos.Length)
            {
                //position non trouvée
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
                //navire non trouvé
                return null;
            }
            else
            {
                return flotte._flotte[nav];
            }
        }

 /*       String accesJoueurAttaquant()
        {
            String nom = "";
            if (_tourPremierJoueur) 
            {
                nom = _joueur1;
            }
            else
            {
                nom = _joueur2;
            }
            return nom;
        }*/

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

        //lorsque le client envoi tous ses bateaux, on recompose l'object flotte avec le String
        Flotte lireFlotte(String flotte)
        {
            Flotte f = new Flotte();

            String listeNav = flotte.Split(':')[1]; // ex: Flotte=Premierbateau=Pos1Pos2;EtLesSuivants....
            List<String> tabNavires = new List<String>(listeNav.Split(';')); // une ligne Premierbateau=Pos1,Pos2 par élément du tableau
            tabNavires.RemoveAt(tabNavires.Count - 1);

            String[] infoNav;  //Une seule ligne du genre Premierbateau=Pos1,Pos2
            String nomNav;     // Premierbateau
            String lisPosNav; // Pos1,Pos2
            int compteNav = 0;
            foreach (String navire in tabNavires)
            {
                infoNav = navire.Split('=');
                nomNav = infoNav[0];
                lisPosNav = infoNav[1];
                String[] tabPos = lisPosNav.Split(',');
                f._flotte[compteNav]._nom = nomNav;
                int comptePos = 0;
                foreach (String coord in tabPos)
                {
                    Pos p = stringToPos(coord);

                    f._flotte[compteNav]._pos[comptePos] = p;
                    f._grille[p._x, p._y] = nomNav;
                    ++comptePos;
                }
                ++compteNav;
            }
            return f;
        }


        //sert 2 fois au début, pour chaque joueur.
        void placerFlotteSurGrille(Flotte joueur)
        {
            foreach (Navire navire in joueur._flotte)
            {
                //identifier ses positions sur la grille
                foreach (Pos p in navire._pos)
                {
                    joueur._grille[p._x, p._y] = navire.getNom();// sera append... avec _touche_coule
                }
            }
        }
    }
}
