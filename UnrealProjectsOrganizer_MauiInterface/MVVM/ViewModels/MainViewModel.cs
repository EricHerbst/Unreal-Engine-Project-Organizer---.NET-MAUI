
using System.Collections.ObjectModel;
using System.Windows.Input;
using UnrealProjectsOrganizer;
using UnrealProjectsOrganizer.Models;

namespace UnrealProjectsOrganizer_MauiInterface.MVVM.ViewModels
{
    class MainViewModel 
    {
        public ObservableCollection<UnrealProject> Projects { get; set; }

        public ICommand OpenProjectCommand
        {
            get => new Command(OpenProject);
        }

        public ICommand CleanProjectCommand
        {
            get => new Command(CleanProject);
        }

        public ICommand DeleteProjectCommand
        {
            get => new Command(DeleteProject);
        }



        public MainViewModel()
        {
            PopulateProjects();
        }

        private async Task PopulateProjects()
        {
            List<UnrealProject> unrealProjects = await UProjectFetcher.GetAllUnrealProjectsAsync();
            unrealProjects.ForEach(p =>
            {
                // Set unreal logo as default if no image exists
                if (string.IsNullOrEmpty(p.ScreenshotPath))
                    p.ScreenshotPath = "unreal_logo.jpg";
            });

            Projects = new ObservableCollection<UnrealProject>(unrealProjects);
        }

        public async void OpenProject(object sender)
        {
            if (sender is UnrealProject project)
            {
                bool confirmation = await App.Current.MainPage.DisplayAlert("Open Project", $"Open {project.ProjectFolderName}?", "Yes", "No");
                if (!confirmation) return;

                UProjectFileInteraction.OpenProject(project);
            }
        }

        public async void CleanProject(object sender)
        {
            if (sender is UnrealProject project)
            {
                bool confirmation = await App.Current.MainPage.DisplayAlert("Clean Project", $"Clean {project.ProjectFolderName}? \n This will delete the binaries folder, intermediate folder, and the .sln file. ", "Yes", "No");
                if (!confirmation) return;

                UProjectFileInteraction.CleanForSolutionRebuild(project);
            }
        }

        public async void DeleteProject(object sender)
        {
            if (sender is UnrealProject project)
            {
                bool confirmation = await App.Current.MainPage.DisplayAlert("Delete Project", $"Delete {project.ProjectFolderName}?", "Yes", "No");
                if (!confirmation) return;

                UProjectFileInteraction.DeleteProject(project);
            }
        }
    }
}
