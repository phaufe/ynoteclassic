using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SS.Ynote.Classic
{
    public enum Language
    {
        Text,
        Batch,
        XML,
        HTML,
        PHP,
        CSS,
        Javascript,
        ASP,
        SQL,
        Actionscript,
        CS,
        CPP,
        Java,
        Python,
        QBasic,
        VB
    }
     public class Globals
     {
        public Globals()
        {
        }
      public Language Language
        {
            get{
                    return this.Language;
                }
       
            set
            {
                Language = value;
            }
        }
    }
    
}
