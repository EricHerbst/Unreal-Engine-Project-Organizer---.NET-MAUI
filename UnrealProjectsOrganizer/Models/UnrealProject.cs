
namespace UnrealProjectsOrganizer.Models
{
    public class UnrealProject
    {
        /// <summary>
        /// The absolute path of the .uproject directory, including the uproject the file. 
        /// Ex: C:\UnrealProjects\MainProject\MainProject.uproject
        /// </summary>
        public string FullUProjectPath { get; set; }

        /// <summary>
        /// The project name as it would be displayed normally. 
        /// The name of the folder containing the uproject file.
        /// </summary>
        public string ProjectFolderName { get; set; }

        /// <summary>
        /// The path to the directory containing the project folder, absent the folder name
        /// </summary>
        public string ProjectDirectory { get; set; }

        /// <summary>
        /// The saved preview screenshot display in the epic launcher
        /// </summary>
        public string ScreenshotPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ProjectCreationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ProjectModifiedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastProjectAccessDate { get; set; }
    }
}
