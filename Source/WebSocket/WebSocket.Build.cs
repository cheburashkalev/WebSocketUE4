// Copyright 1998-2017 Epic Games, Inc. All Rights Reserved.

using System.IO;
using System;
using UnrealBuildTool;

public class WebSocket : ModuleRules
{
    private string ModulePath
    {
        get { return ModuleDirectory; }
    }

    private string EngineMajorVersion;
    private string EngineMinorVersion;
    private string EnginePatchVersion;

    string GetEngineDirectory()
    {
        string magicKey = "UE_ENGINE_DIRECTORY=";
        for (var i = 0; i < PublicDefinitions.Count; i++)
        {
            if (PublicDefinitions[i].IndexOf(magicKey) >= 0)
            {
                return PublicDefinitions[i].Substring(magicKey.Length + 1);
            }
        }

        return "";
    }

    public WebSocket(ReadOnlyTargetRules Target) : base(Target)
    {
        PrivatePCHHeaderFile = "Public/WebSocket.h";

        string strEngineDir = GetEngineDirectory();
        string strEngineVersion = ReadEngineVersion(strEngineDir);

        System.Console.WriteLine("version:" + strEngineVersion);

        PrivateIncludePaths.AddRange(
			new string[] {
				"WebSocket/Private",
				// ... add other private include paths required here ...
			}
			);
			
		
		PublicDependencyModuleNames.AddRange(
			new string[]
			{
				"Core",
                "Json",
                "JsonUtilities",
                "Sockets",
				// ... add other public dependencies that you statically link with here ...
			}
			);
			
		
		PrivateDependencyModuleNames.AddRange(
			new string[]
			{
				"CoreUObject",
				"Engine",
				"Slate",
				"SlateCore",
                "Json",
                "JsonUtilities",
				// ... add private dependencies that you statically link with here ...	
			}
			);
		
		
		DynamicallyLoadedModuleNames.AddRange(
			new string[]
			{
				// ... add any modules that your module loads dynamically here ...
			}
			);

        if(int.Parse(EngineMinorVersion) > 23)
        {
            PublicDefinitions.Add("PLATFORM_HTML5=0");
        }

        if (Target.Platform == UnrealTargetPlatform.Win64)
        {
            PublicDefinitions.Add("PLATFORM_UWP=0");
            PrivateDependencyModuleNames.Add("zlib");
            if (EngineMinorVersion == "21" || EngineMinorVersion == "20")
            {
                PrivateDependencyModuleNames.Add("OpenSSL");
            }
 PrivateDependencyModuleNames.Add("OpenSSL");
            PrivateIncludePaths.Add("WebSocket/ThirdParty/include/Win64");
            var thirdPartyDir = Path.GetFullPath(Path.Combine(ModuleDirectory, "ThirdParty/"));
            string strStaticPath = Path.GetFullPath(Path.Combine(thirdPartyDir, "lib/Win64/"));
            PublicLibraryPaths.Add(strStaticPath);
 PublicAdditionalLibraries.Add(Path.GetFullPath(Path.Combine(strStaticPath,"websockets_game_static422.lib")));
               
            

            
        }
        if (Target.Platform == UnrealTargetPlatform.Win32)
        {
            PublicDefinitions.Add("PLATFORM_UWP=0");
            PrivateDependencyModuleNames.Add("zlib");
            PrivateDependencyModuleNames.Add("OpenSSL");
            PrivateIncludePaths.Add("WebSocket/ThirdParty/include/Win32");

            string strStaticPath = Path.GetFullPath(Path.Combine(ModulePath, "ThirdParty/lib/Win32/"));
            PublicLibraryPaths.Add(strStaticPath);

            // 4.22 and 4.21
            if (EngineMinorVersion == "21" || EngineMinorVersion == "20")
            {
                string[] StaticLibrariesX32 = new string[] {
                    "websockets_static.lib",
                    //"libcrypto.lib",
                    //"libssl.lib",
                };

                foreach (string Lib in StaticLibrariesX32)
                {
                    PublicAdditionalLibraries.Add(Lib);
                }
            }
            /*else if(EngineMinorVersion == "22"|| EngineMinorVersion == "23")*/
            else
            {
                string[] StaticLibrariesX32 = new string[] {
                    "websockets_static422.lib",
                    //"libcrypto.lib",
                    //"libssl.lib",
                };

                foreach (string Lib in StaticLibrariesX32)
                {
                    PublicAdditionalLibraries.Add(Lib);
                }
            }
        }
        
        if (Target.Platform == UnrealTargetPlatform.Win64)
        {
            PublicDefinitions.Add("PLATFORM_UWP=0");
            PrivateDependencyModuleNames.Add("zlib");
            PrivateDependencyModuleNames.Add("OpenSSL");
            PrivateIncludePaths.Add("WebSocket/ThirdParty/include/Win32");

            string strStaticPath = Path.GetFullPath(Path.Combine(ModulePath, "ThirdParty/lib/Win32/"));
            PublicLibraryPaths.Add(strStaticPath);

            // 4.22 and 4.21
            if (EngineMinorVersion == "21" || EngineMinorVersion == "20")
            {
                string[] StaticLibrariesX32 = new string[] {
                    "websockets_static.lib",
                    //"libcrypto.lib",
                    //"libssl.lib",
                };

                foreach (string Lib in StaticLibrariesX32)
                {
                    PublicAdditionalLibraries.Add(Lib);
                }
            }
            /*else if(EngineMinorVersion == "22"|| EngineMinorVersion == "23")*/
            else
            {
                string[] StaticLibrariesX32 = new string[] {
                    "websockets_static422.lib",
                    //"libcrypto.lib",
                    //"libssl.lib",
                };

                foreach (string Lib in StaticLibrariesX32)
                {
                    PublicAdditionalLibraries.Add(Lib);
                }
            }
        }
        /*else if(Target.Platform == UnrealTargetPlatform.HTML5)
        {
            PublicDefinitions.Add("PLATFORM_UWP=0");
            string strStaticPath = Path.GetFullPath(Path.Combine(ModulePath, "ThirdParty/lib/HTML5/"));
            PublicLibraryPaths.Add(strStaticPath);

            string[] StaticLibrariesHTML5 = new string[] {
                "WebSocket.js",
            };

            foreach (string Lib in StaticLibrariesHTML5)
            {
                PublicAdditionalLibraries.Add(strStaticPath + Lib);
            }
        }*/
        else if(Target.Platform == UnrealTargetPlatform.Mac)
        {
            PublicDefinitions.Add("PLATFORM_UWP=0");
            PrivateIncludePaths.Add("WebSocket/ThirdParty/include/Mac");
            string strStaticPath = Path.GetFullPath(Path.Combine(ModulePath, "ThirdParty/lib/Mac/"));
            //PublicLibraryPaths.Add(strStaticPath);

            string[] StaticLibrariesMac = new string[] {
                "libwebsockets.a",
                "libssl.a",
                "libcrypto.a"
            };

            foreach (string Lib in StaticLibrariesMac)
            {
                PublicAdditionalLibraries.Add(Path.Combine(strStaticPath, Lib) );
            }
        }
        else if (Target.Platform == UnrealTargetPlatform.Linux)
        {
            PublicDefinitions.Add("PLATFORM_UWP=0");
            PrivateDependencyModuleNames.Add("OpenSSL");
            PrivateIncludePaths.Add("WebSocket/ThirdParty/include/Linux");
            string strStaticPath = Path.GetFullPath(Path.Combine(ModulePath, "ThirdParty/lib/Linux/"));
            PublicLibraryPaths.Add(strStaticPath);

            string[] StaticLibrariesMac = new string[] {
                "libwebsockets.a",
                //"libssl.a",
                //"libcrypto.a"
            };
            
            foreach (string Lib in StaticLibrariesMac)
            {
                PublicAdditionalLibraries.Add(Path.Combine(strStaticPath, Lib));
            }
        }
        else if(Target.Platform == UnrealTargetPlatform.IOS)
        {
            PublicDefinitions.Add("PLATFORM_UWP=0");
            PrivateIncludePaths.Add("WebSocket/ThirdParty/include/IOS");
            PrivateDependencyModuleNames.Add("OpenSSL");

            string PluginPath = Utils.MakePathRelativeTo(ModuleDirectory, Target.RelativeEnginePath + "/Source/");
            PluginPath = PluginPath.Replace("\\", "/");

            string strStaticPath = PluginPath + "/ThirdParty/lib/IOS/";// Path.GetFullPath(Path.Combine(ModulePath, "ThirdParty/lib/IOS/"));
            PublicLibraryPaths.Add(strStaticPath);

            string[] StaticLibrariesIOS = new string[] {
                "websockets",
                //"ssl",
                //"crypto"
            };

            foreach (string Lib in StaticLibrariesIOS)
            {
                PublicAdditionalLibraries.Add(Lib);
                PublicAdditionalShadowFiles.Add(Path.Combine(strStaticPath, "lib" + Lib + ".a") );
            }
        }
        else if(Target.Platform == UnrealTargetPlatform.Android)
        {
            PublicDefinitions.Add("PLATFORM_UWP=0");
            PrivateIncludePaths.Add("WebSocket/ThirdParty/include/Android");
            string strStaticPath = Path.GetFullPath(Path.Combine(ModulePath, "ThirdParty/lib/Android/armeabi-v7a"));
            PublicLibraryPaths.Add(strStaticPath);

            string strStaticArm64Path = Path.GetFullPath(Path.Combine(ModulePath, "ThirdParty/lib/Android/arm64-v8a"));
            PublicLibraryPaths.Add(strStaticArm64Path);


            string[] StaticLibrariesAndroid = new string[] {
                "websockets",
                "ssl",
                "crypto"
            };

            foreach (string Lib in StaticLibrariesAndroid)
            {
                PublicAdditionalLibraries.Add(Lib);
            }

            string PluginPath = Utils.MakePathRelativeTo(ModuleDirectory, Target.RelativeEnginePath);
            AdditionalPropertiesForReceipt.Add("AndroidPlugin", Path.Combine(PluginPath, "WebSocket_UPL.xml"));
        }
    }

    private string ReadEngineVersion(string EngineDirectory)
    {
        string EngineVersionFile = Path.Combine(EngineDirectory, "Runtime", "Launch", "Resources", "Version.h");
        string[] EngineVersionLines = File.ReadAllLines(EngineVersionFile);
        for (int i = 0; i < EngineVersionLines.Length; ++i)
        {
            if (EngineVersionLines[i].StartsWith("#define ENGINE_MAJOR_VERSION"))
            {
                EngineMajorVersion = EngineVersionLines[i].Split('\t')[1].Trim(' ');
            }
            else if (EngineVersionLines[i].StartsWith("#define ENGINE_MINOR_VERSION"))
            {
                EngineMinorVersion = EngineVersionLines[i].Split('\t')[1].Trim(' ');
            }
            else if (EngineVersionLines[i].StartsWith("#define ENGINE_PATCH_VERSION"))
            {
                EnginePatchVersion = EngineVersionLines[i].Split('\t')[1].Trim(' ');
            }

        }

        return EngineMajorVersion + "." + EngineMinorVersion + "." + EnginePatchVersion;
    }
}
