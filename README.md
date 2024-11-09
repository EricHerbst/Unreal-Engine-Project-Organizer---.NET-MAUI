I wanted a better solution to finding my Unreal Engine projects than what the Epic Launcher provides. I also wanted an excuse to try out .Net Maui.

There are two main problems I'm trying to solve for myself:
1) Moving a project seems to permanently remove it from the epic launcher's ability to detect it, even after opening it manually from the launcher. So then I have to manually find it in the file explorer.
2) I have projects in multiple folder structures. I often can't remember the name of the project I want to reference, but I know the context of the project, which means I usually have to use the file explorer anyway.

Therefore I created a core library that can fetch all of the unreal project folders, identified by their .uproject files, on my chosen directory (for me, I hard coded the P: drive where all my projects are).
This creates UnrealProject objects that can be collected, displayed, and interacted with using a .Net Maui project that references the library.

Functionality:
- The ability to see all unreal projects, organized by root folder alphabetically
- The ability to string search project names to narrow down display
- The ability to open, delete, or "clean" the projects via buttons in the UI
    - Cleaning in this case means after a C++ code reoganization. This requires deleting the Binaries and Intermediate folders, as well as the solution file for project regeneration.
