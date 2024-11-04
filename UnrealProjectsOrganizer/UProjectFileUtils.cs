
using UnrealProjectsOrganizer.Models;

namespace UnrealProjectsOrganizer
{
    internal static class UProjectFileUtils
    {
        /// <summary>
        /// Return the directory of the .uproject folder
        /// </summary>
        public static string GetProjectDirectory(UnrealProject unrealProject)
        {
            return Path.Combine(unrealProject.ProjectDirectory, unrealProject.ProjectFolderName);
        }
    }
}
