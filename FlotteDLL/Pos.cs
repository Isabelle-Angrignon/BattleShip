﻿using System;
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
        public void setPos(int x, int y)
        {
            _x = x;
            _y = y;
        }

        //Constructeur
        public Pos()
        {
            setPos(-1, -1);
            colonnes = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        }

        public override string ToString()
        {             
            int noLigne = _y + 1;
            return colonnes[_x] + noLigne;
        }        
    }
}