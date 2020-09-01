﻿//
//  DirectoryHelpers.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.IO;
using System.Reflection;
using Launchpad.Launcher.Handlers;

namespace Launchpad.Launcher.Utility
{
	/// <summary>
	/// Helper methods for common paths and directories.
	/// </summary>
	public static class DirectoryHelpers
	{
		private const string ConfigurationFolderName = "Config";
		private const string ConfigurationFileName = "LauncherConfig";
		private const string GameArgumentsFileName = "GameArguments";

  /// <summary>
  /// Fixes URL if it has any error.
  /// </summary>
  /// <param name="url">in string url that will fix. </param>
	/// <returns>The fixed url.</returns>
		public static string FixURL(string url)
  {
  url = url.Replace("//", "/");
  url = url.Replace(":/", "://");
  return url;
  /*
  if (string.IsNullOrEmpty(url))
	{
  return;
  }
  int index = url.IndexOf("://");
  if (index < 0)
  {
  return;
  }
  else
  {
  string substring = url.Substring(0, index + "://".Length);
  substring.Replace("//", "/");
  url = url.Substring(0, index + "://".Length) + substring;
  }
  */
	}

		/// <summary>
		/// Gets the expected path to the config file on disk.
		/// </summary>
		/// <returns>The config path.</returns>
		public static string GetConfigPath()
		{
			return Path.Combine(GetConfigDirectory(), $"{ConfigurationFileName}.ini");
		}

		/// <summary>
		/// Gets the path to the config directory.
		/// </summary>
		/// <returns>The path.</returns>
		public static string GetConfigDirectory()
		{
			return Path.Combine(GetLocalLauncherDirectory(), ConfigurationFolderName);
		}

		/// <summary>
		/// Gets the path to the launcher cookie on disk.
		/// </summary>
		/// <returns>The launcher cookie.</returns>
		public static string GetLauncherTagfilePath()
		{
			return Path.Combine(GetLocalLauncherDirectory(), ".launcher");
		}

		/// <summary>
		/// Gets the install cookie.
		/// </summary>
		/// <returns>The install cookie.</returns>
		public static string GetGameTagfilePath()
		{
			return Path.Combine(GetLocalLauncherDirectory(), ".game");
		}

		/// <summary>
		/// Gets the local directory where the launcher is stored.
		/// </summary>
		/// <returns>The local directory.</returns>
		public static string GetLocalLauncherDirectory()
		{
			var codeBaseURI = new UriBuilder(Assembly.GetExecutingAssembly().Location).Uri;
			return Path.GetDirectoryName(Uri.UnescapeDataString(codeBaseURI.AbsolutePath));
		}

		/// <summary>
		/// Gets the temporary launcher download directory.
		/// </summary>
		/// <returns>A full path to the directory.</returns>
		public static string GetTempLauncherDownloadPath()
		{
			return Path.Combine(Path.GetTempPath(), "launchpad", "launcher");
		}

		/// <summary>
		/// Gets the expected path to the argument file on disk.
		/// </summary>
		/// <returns>The path.</returns>
		public static string GetGameArgumentsPath()
		{
			return Path.Combine(GetConfigDirectory(), $"{GameArgumentsFileName}.txt");
		}

		/// <summary>
		/// Gets the game directory.
		/// </summary>
		/// <returns>The directory.</returns>
		public static string GetLocalGameDirectory()
		{
			var config = ConfigHandler.Instance.Configuration;
			return Path.Combine(GetLocalLauncherDirectory(), "Game", config.SystemTarget.ToString());
		}

		/// <summary>
		/// Gets the game version path.
		/// </summary>
		/// <returns>The game version path.</returns>
		public static string GetLocalGameVersionPath()
		{
			return Path.Combine(GetLocalGameDirectory(), "GameVersion.txt");
		}

		/// <summary>
		/// Gets the remote path to where launcher binaries are stored.
		/// </summary>
		/// <returns>The path.</returns>
		public static string GetRemoteLauncherBinariesPath()
		{
			var config = ConfigHandler.Instance.Configuration;
			return $"{config.RemoteAddress}/launcher/bin/";
		}

		/// <summary>
		/// Gets the remote path of the launcher version.
		/// </summary>
		/// <returns>
		/// The path to either the official launchpad binaries or a custom launcher, depending on the settings.
		/// </returns>
		public static string GetRemoteLauncherVersionPath()
		{
			var config = ConfigHandler.Instance.Configuration;

			return $"{config.RemoteAddress}/launcher/LauncherVersion.txt";
		}

		/// <summary>
		/// Gets the remote path where the game is stored..
		/// </summary>
		/// <returns>The path.</returns>
		public static string GetRemoteGamePath()
		{
			var config = ConfigHandler.Instance.Configuration;
			return $"{config.RemoteAddress}/game/{config.SystemTarget}/bin/";
		}
	}
}
