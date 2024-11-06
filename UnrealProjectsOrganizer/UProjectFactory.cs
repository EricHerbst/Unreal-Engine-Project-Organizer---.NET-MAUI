
using UnrealProjectsOrganizer.Models;

namespace UnrealProjectsOrganizer
{
    internal static class UProjectFactory
    {
        private const string ScreenshotPathAddition = @"Saved\AutoScreenshot.png";


        /// <summary>
        /// Create an unreal project object based on the given path to the uproject file
        /// </summary>
        public static UnrealProject CreateProjectFromFilePath(string projectPath)
        {
            var project = new UnrealProject();

            if (string.IsNullOrWhiteSpace(projectPath)) return null;

            // Populate convenience project directory info
            project.FullUProjectPath = projectPath;
            project.ProjectFolderName = GetProjectFolderNameFromPath(projectPath);
            project.ProjectDirectory = GetProjectFolderDirectory(projectPath);
            project.ScreenshotPath = GetScreenshotPath(projectPath);

            if (!PathDetailsAreComplete(project)) return null;

            // Populate dateTimes for optional sorting. Less important.
            AppendDirectoryTimestamps(project);

            return project;
        }

        // Ensure the important paths are not empty
        private static bool PathDetailsAreComplete(UnrealProject project)
        {
            if (project.ProjectFolderName == string.Empty || project.ProjectDirectory == string.Empty)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get the uproject name from the parent folder of the .uproject file path
        /// </summary>
        private static string GetProjectFolderNameFromPath(string filePath)
        {
            try
            {
                string folderPath = Path.GetDirectoryName(filePath);
                string[] brokenDirectory = folderPath.Split('\\');
                return brokenDirectory.Last();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the path for the project folder excuding the folder name
        /// </summary>
        private static string GetProjectFolderDirectory(string filePath)
        {
            try
            {
                string folderPath = Path.GetDirectoryName(filePath);
                string[] brokenDirectory = folderPath.Split('\\');
                // Remove last item from array for the combine step
                Array.Resize(ref brokenDirectory, brokenDirectory.Length - 1);

                return string.Join("\\", brokenDirectory);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the path of unreal's preview screenshot if it exists
        /// </summary>
        private static string GetScreenshotPath(string targetPath)
        {
            try
            {
                string folderPath = Path.GetDirectoryName(targetPath);
                var screenshotPath = Path.Combine(folderPath, ScreenshotPathAddition);
                if (File.Exists(screenshotPath))
                {
                    return screenshotPath;
                }

                // File doesn't exist
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        // Less important, used for sorting
        private static void AppendDirectoryTimestamps(UnrealProject project)
        {
            var projectPath = UProjectFileUtils.GetProjectDirectory(project);
            project.ProjectCreationDate = Directory.GetCreationTime(projectPath);
            project.ProjectModifiedDate = Directory.GetLastWriteTime(projectPath);
            project.LastProjectAccessDate = Directory.GetLastAccessTime(projectPath);
        }
    }
}
