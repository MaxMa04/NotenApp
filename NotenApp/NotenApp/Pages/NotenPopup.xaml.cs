﻿using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.Services;
using NotenApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotenPopup : Popup
    {
        private int? returnWhenDismissed;
        public NotenPopup(WhichNote note, NotenTyp notenTyp, string fachName)
        {
            InitializeComponent();
            switch (note)
            {
                case WhichNote.Block1:
                    btn.Text = "Zurück";
                    returnWhenDismissed = null;
                    break;case WhichNote.Endnote:
                    btn.Text = "Löschen";
                    returnWhenDismissed = null;
                    break;
                default:
                    btn.Text = "Löschen";
                    returnWhenDismissed = -1;
                    break;
            }
            switch (notenTyp)
            {
                case NotenTyp.LK:
                    notentyp.Text = "Lk";
                    break;
                case NotenTyp.Klausur:
                    notentyp.Text = "Klausur";
                    break;
                case NotenTyp.Schriftlich:
                    notentyp.Text = "Schriftlich";
                    break;
                case NotenTyp.Mündlich:
                    notentyp.Text = "Mündlich";
                    break;
                case NotenTyp.Ziel:
                    notentyp.Text = "Ziel";
                    break;
                case NotenTyp.Endnote:
                    notentyp.Text = "Endnote";
                    break;
            }
            fachname.Text = fachName;
            

        }
        private void NotenButton_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int note = Convert.ToInt32(button.Text);
            Dismiss(note);
        }

        private void DismissButton_Clicked(object sender, EventArgs e)
        {
            Dismiss(returnWhenDismissed);
        }
    }
}