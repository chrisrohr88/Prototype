using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using CWDev.SLNTools.Core;

public class PostProcessVisualStudioCSProject : AssetPostprocessor
{
	static public void OnGeneratedCSProjectFiles()
	{
		string projectDirectory = System.IO.Directory.GetParent(Application.dataPath).FullName;
		string projectName = Path.GetFileName(projectDirectory);
		string slnFile = Path.Combine(projectDirectory, string.Format("{0}.sln", projectName));
		string unitTestsSlnFile = Path.Combine(projectDirectory+"/UnitTests/", string.Format("{0}.sln", "UnitTests"));
		
		SolutionFile unitTestsSln = SolutionFile.FromFile(unitTestsSlnFile);
		SolutionFile sln = SolutionFile.FromFile(slnFile);
		foreach(var project in unitTestsSln.Projects)
		{
			project.RelativePath = projectDirectory+"/UnitTests/" + string.Format("{0}.csproj", project.ProjectName);
			sln.Projects.Add(unitTestsSln.Projects[0]);
		}
		sln.Save();
	}
}