﻿using MvvmHelpers;
using NotenApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Logic
{
    public static class Controller
    {
        public static ObservableRangeCollection<HjFach> SortList(ObservableRangeCollection<HjFach> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int maxIndex = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[maxIndex].Durchschnitt == null)
                    {
                        maxIndex = j;
                    }
                    else if(list[j].Durchschnitt > list[maxIndex].Durchschnitt)
                    {
                        maxIndex = j;
                    }
                }
                (list[maxIndex], list[i]) = (list[i], list[maxIndex]);
            }
            return list;
        }

        public static List<HjFach> SortList(List<HjFach> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int maxIndex = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[maxIndex].Durchschnitt == null)
                    {
                        maxIndex = j;
                    }
                    else if (list[j].Durchschnitt > list[maxIndex].Durchschnitt)
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
        public static List<HjFach> SortListByHalbjahr(List<HjFach> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    
                    if (list[j].Halbjahr < list[minIndex].Halbjahr)
                    {
                        minIndex = j;
                    }
                }
                (list[minIndex], list[i]) = (list[i], list[minIndex]);
            }
            return list;
        }

    }
}
