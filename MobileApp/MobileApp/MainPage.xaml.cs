using System;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class MainPage : ContentPage
    {
        private readonly CamInfoViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel = new CamInfoViewModel();
            BindingContext = viewModel;
        }
    }
}
