using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public class View
    {
        //public Player player { get; set; }
        public Field labyrinth { get; set; }

        public View(int w, int h) 
        {
            labyrinth = new();
            //player = new();
        }
    }
}
