
using System.Collections.ObjectModel;
using System.Windows.Input;
using UnrealProjectsOrganizer;
using UnrealProjectsOrganizer.Models;

namespace UnrealProjectsOrganizer_MauiInterface.MVVM.ViewModels
{
    class MainViewModel 
    {   
        private List<UnrealProject> _allProjects = new List<UnrealProject>();
        private const string DefaultUnrealLogo = "unreal_logo.jpg";

        // After some experience, probably not the best choice. See SetDisplayedProjects comments.
        public ObservableCollection<UnrealProject> Projects { get; set; } = new ObservableCollection<UnrealProject>();

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
            PopulateAllProjectsAsync();
        }

        private async Task PopulateAllProjectsAsync()
        {
            _allProjects = await UProjectFetcher.GetAllUnrealProjectsAsync();
            _allProjects.ForEach(p =>
            {
                // Set unreal logo as default if no image exists
                if (string.IsNullOrEmpty(p.ScreenshotPath))
                    p.ScreenshotPath = DefaultUnrealLogo;
            });

            SetDisplayedProjects(_allProjects);            
        }

        /// <summary>
        /// Filter project names by provided string
        /// </summary>
        public void RunProjectNameFilter(object sender, TextChangedEventArgs e)
        {
            var filterValue = e.NewTextValue.ToLower();

            if (string.IsNullOrEmpty(filterValue))
            {
                SetDisplayedProjects(_allProjects);
                return;
            }

            List<UnrealProject> filteredProjects = _allProjects.Where(p => p.ProjectFolderName.ToLower().Contains(filterValue)).ToList();
            SetDisplayedProjects(filteredProjects);
        }

        // Can't add range or completely replace with new ObservableCollection(projectsToDisplay) as the events no longer seem to reach the listener.
        private void SetDisplayedProjects(List<UnrealProject> projectsToDisplay)
        {
            Projects.Clear();
            foreach (var project in projectsToDisplay)
            {
                Projects.Add(project);
            }
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
                bool confirmation = await Application.Current.MainPage.DisplayAlert("Clean Project", $"Clean {project.ProjectFolderName}? \n This will delete the binaries folder, intermediate folder, and the .sln file. ", "Yes", "No");
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
