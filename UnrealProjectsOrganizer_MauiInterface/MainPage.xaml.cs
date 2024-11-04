
using UnrealProjectsOrganizer_MauiInterface.MVVM.ViewModels;

namespace UnrealProjectsOrganizer_MauiInterface
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
