using NotenApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Logic
{
    public static class Controller
    {
        public static List<HjFach> SortList(List<HjFach> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int maxIndex = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[j].Durchschnitt > list[maxIndex].Durchschnitt)
                    {
                        maxIndex = j;
                    }
                }
                (list[maxIndex], list[i]) = (list[i], list[maxIndex]);
            }
            return list;
        }
        public static List<double> SortList(List<double> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int maxIndex = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[j] > list[maxIndex])
                    {
                        maxIndex = j;
                    }
                }
                (list[maxIndex], list[i]) = (list[i], list[maxIndex]);
            }
            return list;
        }
    }
}
