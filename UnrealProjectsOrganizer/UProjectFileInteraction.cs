
using System.Diagnostics;
using UnrealProjectsOrganizer.Models;

namespace UnrealProjectsOrganizer
{
    public static class UProjectFileInteraction
    {
        private const string BinariesFolderPath = "Binaries";
        private const string IntermediateFolderPath = "Intermediate";
        private const string SolutionExtension = ".sln";


        /// <summary>
        /// Open the unreal project in engine
        /// </summary>
        public static void OpenProject(UnrealProject project)
        {
            using Process projectOpener = new Process();
            projectOpener.StartInfo.FileName = project.FullUProjectPath;
            projectOpener.StartInfo.UseShellExecute = true;
            projectOpener.Start();
        }

        /// <summary>
        /// Delete the project entirely
        /// </summary>
        public static void DeleteProject(UnrealProject unrealProject)
        {
            try
            {
                var projectDirectory = UProjectFileUtils.GetProjectDirectory(unrealProject);
                if (Directory.Exists(projectDirectory))
                {
                    Directory.Delete(projectDirectory);
                }
            }
            catch (IOException ex)
            {
                return;
            }
        }

        /// <summary>
        /// Clean the necessary files and folders after refactoring c++ classes
        /// </summary>
        public static void CleanForSolutionRebuild(UnrealProject unrealProject)
        {
            DeleteBinariesFolder(unrealProject);
            DeleteIntermediateFolder(unrealProject);
            DeleteSolutionFile(unrealProject);
        }

        private static void DeleteBinariesFolder(UnrealProject unrealProject)
        {
            try
            {
                var projectDirectory = UProjectFileUtils.GetProjectDirectory(unrealProject);
                var binariesDirectory = Path.Combine(projectDirectory, BinariesFolderPath);
                if (Directory.Exists(binariesDirectory))
                {
                    Directory.Delete(binariesDirectory);
                }
            }
            catch (IOException ex)
            {
                return;
            }
        }

        private static void DeleteIntermediateFolder(UnrealProject unrealProject)
        {
            try
            {
                var projectDirectory = UProjectFileUtils.GetProjectDirectory(unrealProject);
                var intermediateDirectory = Path.Combine(projectDirectory, IntermediateFolderPath);
                if (Directory.Exists(intermediateDirectory))
                {
                    Directory.Delete(intermediateDirectory);
                }
            }
            catch (IOException ex)
            {
                return;
            }
        }

        private static void DeleteSolutionFile(UnrealProject unrealProject)
        {
            try
            {
                var projectDirectory = UProjectFileUtils.GetProjectDirectory(unrealProject);
                var solutionFile = Path.Combine(projectDirectory, unrealProject.ProjectFolderName, SolutionExtension);
                if (File.Exists(solutionFile))
                {
                    File.Delete(solutionFile);
                }
            }
            catch (IOException ex)
            {
                return;
            }
        }


        #region WishList Items

        public static void RenameProject(UnrealProject project, string newName)
        {
            throw new NotImplementedException();
        }

        public static void ConvertToTemplate(UnrealProject project)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
