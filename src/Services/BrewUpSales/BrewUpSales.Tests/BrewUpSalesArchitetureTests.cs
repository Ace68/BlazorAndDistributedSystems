using NetArchTest.Rules;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using XmasReceiver.Modules;

namespace XmasReceiver.Tests;

[ExcludeFromCodeCoverage]
public class XmasReceiverArchitetureTests
{
	[Fact]
	public void XmasReceiver_ShouldHaveDependency_OnlyWithFacadeAndShared()
	{
		var types = Types.InAssembly(typeof(IModule).Assembly);
		var forbiddenAssemblies = new List<string>
		{
			"XmasReceiver.Infrastructures",
			"XmasReceiver.Messages",
			"XmasReceiver.ReadModel",
			"XmasReceiver.Domain",
		};

		var result = types
			.ShouldNot()
			.HaveDependencyOnAny(forbiddenAssemblies.ToArray())
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}

	[Fact]
	public void XmasReceiverProjects_ShouldHave_Namespace_StartingWith_XmasReceiver()
	{
		var subFolders = Directory.GetDirectories(SolutionProvider.TryGetSolutionDirectoryInfo().FullName);
		subFolders = subFolders.Where(f => !f.EndsWith(".vs")).ToArray()
			.Where(f => !f.EndsWith("XmasReceiver")).ToArray();
		var netVersion = Environment.Version;

		var xmsReceiverAssemblies = (from folder in subFolders
									 let binFolder = Path.Join(folder, "bin", "Debug", $"net{netVersion.Major}.{netVersion.Minor}")
									 let files = Directory.GetFiles(binFolder)
									 let folderArray = folder.Split(Path.DirectorySeparatorChar)
									 select files.FirstOrDefault(f => f.EndsWith($"{folderArray[folderArray!.Length - 1]}.dll"))
			into assemblyFilename
									 where !assemblyFilename!.Contains("Test")
									 select Assembly.LoadFile(assemblyFilename!)).ToList();

		var xmasReceiverTypes = Types.InAssemblies(xmsReceiverAssemblies);
		var xmasReceiverResult = xmasReceiverTypes
			.Should()
			.ResideInNamespaceStartingWith("XmasReceiver")
			.GetResult();

		Assert.True(xmasReceiverResult.IsSuccessful);
	}

	[Fact]
	public void XmasReceiverDomain_ShouldHaveDependency_OnlyWithMessages()
	{
		var types = Types.InCurrentDomain()
			.That()
			.ResideInNamespace("XmasReceiver.Domain");
		var forbiddenAssemblies = new List<string>
		{
			"XmasReceiver",
			"XmasReceiver.Infrastructures",
			"XmasReceiver.Facade",
			"XmasReceiver.Shared",
			"XmasReceiver.ReadModel"
		};

		var result = types
			.ShouldNot()
			.HaveDependencyOnAny(forbiddenAssemblies.ToArray())
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}

	private static class SolutionProvider
	{
		public static DirectoryInfo TryGetSolutionDirectoryInfo(string? currentPath = null)
		{
			var directory = new DirectoryInfo(
				currentPath ?? Directory.GetCurrentDirectory());
			while (directory != null && !directory.GetFiles("*.sln").Any())
			{
				directory = directory.Parent;
			}
			return directory!;
		}
	}
}