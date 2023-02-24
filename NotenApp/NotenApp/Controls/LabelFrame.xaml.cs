using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LabelFrame : ContentView
	{
		public static readonly BindableProperty FrameTextProperty = BindableProperty.Create("FrameText", typeof(string), typeof(LabelFrame), default(string));
        public static readonly BindableProperty FrameColorProperty = BindableProperty.Create("FrameColor", typeof(Color), typeof(LabelFrame));
        public string FrameText
		{
			get { return (string)GetValue(FrameTextProperty);}
			set { SetValue(FrameTextProperty, value);}
		}
		public Color FrameColor
		{
			get { return (Color)GetValue(FrameColorProperty); }
			set { SetValue(FrameColorProperty, value); }
		}
		public LabelFrame ()
		{
			InitializeComponent();
			innerLabel.SetBinding(Label.TextProperty, new Binding("FrameText", source: this));
			frame.SetBinding(Frame.BackgroundColorProperty, new Binding("FrameColor", source: this));
		}
	}
}