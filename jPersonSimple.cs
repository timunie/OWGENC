using System;
using System.Collections.Generic;
using System.Text;

namespace OWGENC
{
       
public class jPersonSimple
    {
        public string name { get; set; }
        public int id { get; set; }
        public addr address { get; set; }        

        public class addr
        {
            public string team { get; set; }
            public string community { get; set; }
        }


        //public string name { get; set; }
        //public int id { get; set; }
        //public string message { get; set; }
        ////object attribute
        public List<Platform> platforms { get; set; }
        public class Platform
        {

            public string platformName { get; set; }
            public string handle { get; set; }

        }

        
    }
}
