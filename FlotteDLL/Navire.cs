using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlotteDLL
{
    public class Navire
    {
        private const int MAX_CASES = 5;
        private const int MIN_CASES = 2;

        //Attributs
        public String _nom;
        public Pos[] _pos;

        //Constructeur
        public Navire(int nbCases, string nom)
        {
            this._nom = nom;
            if (nbCases <= MAX_CASES && nbCases >= MIN_CASES)// valide le format du navire
            {
                this._pos = new Pos[nbCases];
                for (int p = 0; p < nbCases; ++p)
                {
                    this._pos[p] = new Pos();
                }
            }         
        } 
        private void definirPosition(int noCase, Pos position)
        {
            this._pos[noCase] = position;
        }
        public void placerNavire(Pos[] positions)
        {
            //pour chaque trou..
            for (int i = 0; i < positions.Length; ++i)
            {
                //on donne sa position
                definirPosition(i, positions[i]);
            }
        }
        public String getNom()
        {
            return _nom;
        }
        public void setNom(String nom)
        {
            this._nom = nom;
        }
        public override string ToString()
        {
            return this.getNom() + "(" + this._pos.Length + ")";
        }
    }
}
