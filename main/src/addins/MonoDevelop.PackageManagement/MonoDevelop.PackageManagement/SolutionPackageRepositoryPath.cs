﻿// 
// SolutionPackageRepositoryPath.cs
// 
// Author:
//   Matt Ward <ward.matt@gmail.com>
// 
// Copyright (C) 2012 Matthew Ward
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.IO;
using MonoDevelop.Projects;
using NuGet;

namespace ICSharpCode.PackageManagement
{
	public class SolutionPackageRepositoryPath
	{
		string packagesRelativeDirectory;
		Solution solution;
		DefaultPackagePathResolver pathResolver;
		
		public SolutionPackageRepositoryPath(Project project)
			: this(project, new PackageManagementOptions())
		{
		}
		
		public SolutionPackageRepositoryPath(Project project, PackageManagementOptions options)
			: this(project.ParentSolution, options)
		{
		}
		
		public SolutionPackageRepositoryPath(Solution solution, PackageManagementOptions options)
		{
			packagesRelativeDirectory = options.PackagesDirectory;
			this.solution = solution;
			GetSolutionPackageRepositoryPath();
		}
		
		void GetSolutionPackageRepositoryPath()
		{
			PackageRepositoryPath = Path.Combine(solution.BaseDirectory, packagesRelativeDirectory);
		}
		
		public string PackageRepositoryPath { get; private set; }
		
		public string GetInstallPath(IPackage package)
		{
			pathResolver = new DefaultPackagePathResolver(PackageRepositoryPath);
			return pathResolver.GetInstallPath(package);
		}
	}
}
