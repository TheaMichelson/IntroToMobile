using FingerPaintSummer24.ViewModels;
using Microsoft.Maui.Controls;

namespace FingerPaintSummer24.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new DrawingViewModel();
        }
    }
}
