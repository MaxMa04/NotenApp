using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Logic
{
    public enum NotenTyp
    {
        //Block1
        LK = 1,
        Klausur = 2,
        Ziel = 5,
        Endnote = 6,
        //Block2
        Schriftlich = 3,
        Mündlich = 4
        
    };
    public enum FachAufgabenfeld
    {
        Sprachlich = 1,
        Gesellschaftlich = 2,
        Naturwissenschaftlich = 3,
        Kein = 4

    };
    public enum SetFach
    {
        LK,
        GK,
        PrFach,
        NoPrFach

    };
    public enum WhichNote
    {
        Block1 = 1,
        Block2 = 2,
        Ziel = 3,
        Endnote = 4
    };
    
}
