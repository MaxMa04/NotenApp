using NotenApp.Themes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NotenApp.Services
{
    public static class Settings
    {
        const int theme = 0;
        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }
        public static void SetTheme(int theme)
        {
            Theme = theme;
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if(mergedDictionaries != null)
    {
                mergedDictionaries.Clear();

                switch (Theme)
                {
                    case 0:
                        mergedDictionaries.Add(new RosaTheme());
                        break;
                    case 1:
                        mergedDictionaries.Add(new BlueTheme());
                        break;
                    case 2:
                        mergedDictionaries.Add(new GreenTheme());
                        break;
                }
            }
        }
    }
}
