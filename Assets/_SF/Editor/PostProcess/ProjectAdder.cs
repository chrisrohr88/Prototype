using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using CWDev.SLNTools.Core;

namespace SF.Editor.PostProcesses
{
	public class PostProcessVisualStudioCSProject : AssetPostprocessor
	{
		private static void OnGeneratedCSProjectFiles()
		{
			string pathToProject = System.IO.Directory.GetParent(Application.dataPath).FullName;
			string projectName = Path.GetFileName(pathToProject);
			string slnFile = Path.Combine(pathToProject, string.Format("{0}.sln", projectName));
			string pathToUnitTestSolution = Path.Combine(pathToProject, "../SFUnitTest/");
			string unitTestsSlnFile = Path.Combine(pathToUnitTestSolution, string.Format("{0}.sln", "SFUnitTest"));
			
			SolutionFile unitTestsSln = SolutionFile.FromFile(unitTestsSlnFile);
			SolutionFile sln = SolutionFile.FromFile(slnFile);
			foreach(var project in unitTestsSln.Projects)
			{
				project.RelativePath = "../SFUnitTest/"+project.ProjectName+"/" + string.Format("{0}.csproj", project.ProjectName);
				sln.Projects.Add(project);
			}
			sln.Save();
		}
	}
}