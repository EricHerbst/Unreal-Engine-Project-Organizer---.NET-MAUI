
using UnrealProjectsOrganizer.Models;

namespace UnrealProjectsOrganizer
{
    public static class UProjectFetcher
    {
        private const int DefaultRecursionDepth = 4;
        private const string ProjectDrive = @"P:\";
        private const string UProjectExtension = ".uproject";


        /// <summary>
        /// Retrieves all uprojects that are children of the provided directory
        /// </summary>
        public static async Task<List<UnrealProject>> GetAllUnrealProjectsAsync(string absolutePath = ProjectDrive, int recursionDepth = DefaultRecursionDepth)
        {
            var projects = new List<UnrealProject>();

            var projectPaths = await GetAllUProjectFiles(absolutePath);
            if (projectPaths == null || projectPaths.Count == 0) return projects;

            foreach (var projectPath in projectPaths)
            {
                UnrealProject project = UProjectFactory.CreateProjectFromFilePath(projectPath);
                if (project == null) continue;

                projects.Add(project);
            }

            return projects;
        }

        /// <summary>
        /// Gets all uproject files that are children of the provided path. Ordered alphabetically.
        /// </summary>
        private static async Task<List<string>> GetAllUProjectFiles(string absolutePath = ProjectDrive, int recursionDepth = DefaultRecursionDepth)
        {
            if (string.IsNullOrWhiteSpace(absolutePath)) return null;

            // Get all project files, recursively n levels deep, skipping inaccessible ones
            var options = new EnumerationOptions();
            options.IgnoreInaccessible = true;
            options.RecurseSubdirectories = true;
            options.MaxRecursionDepth = recursionDepth;

            // Get all uproject filess, grab their directory, and then sort paths alphabetically
            var files = Directory.EnumerateFiles(ProjectDrive, $"*{UProjectExtension}", options)
                .OrderBy(file => file)
                .ToList();

            return files;
        }
    }
}
