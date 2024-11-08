
using UnrealProjectsOrganizer_MauiInterface.MVVM.ViewModels;

namespace UnrealProjectsOrganizer_MauiInterface
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            BindingContext = _viewModel;
        }

        // A work around. Couldn't get the Entry TextChanged to bind to the bindingContext.RunProjectNameWithFilter. Maybe a bug?
        public void RunProjectNameFilter(object sender, TextChangedEventArgs e)
        {
            _viewModel.RunProjectNameFilter(sender, e);
        }
    }
}
