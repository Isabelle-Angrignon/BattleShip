using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlotteDLL
{
    public class Pos
    {
        public String[] colonnes;
        public int _x;
        public int _y;
        public bool _estTouche;
        public void setPos(int x, int y)
        {
            _x = x;
            _y = y;
        }

        //Constructeur
        public Pos()
        {
            setPos(-1,-1);//initialisé hors de la grille
            colonnes = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            _estTouche = false;
        }

        // Exemple de format:  A1  J10 pour affichage au client
        public override string ToString()
        {             
            int noLigne = _y + 1;
            return colonnes[_x] + noLigne;
        }
        // Pour envoi des positions au serveur
        public string ToPos()
        {
            return _x.ToString() + _y.ToString();
        }
       
    }
}
