using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Globalization;


public class serapionBuild : EditorWindow {

    string androidVersion;
    string lastAndroidVersion;
    int androidBuildNum;
    int lastAndroidBuildNum;
    string iosVersion;
    string lastIosVersion;
    string iosBuildNum;
    string lastIosBuildNum;
    string androidPackage;
    string iosPackage;
    bool buildAndroid = false;
    bool buildIOS = false;
    bool changed;
    string git;
    string gitAdd;
    string gitCommit;
    string gitPush;
    string gitUser = "mirnes.halilovic";
    string gitPass = "nekiPass";
    string commit;
    string repoUrl;
    string path;



	[MenuItem("Serapion Build/BUILD")]
	public static void ShowWindow ()
	{
		GetWindow<serapionBuild>("SERAPION AUTOMATION");
	}

	void OnGUI ()
	{
		GUILayout.Label("App Information", EditorStyles.boldLabel);

        if (!changed) {

            androidVersion = PlayerSettings.bundleVersion;
            lastAndroidVersion = androidVersion;
            androidBuildNum = PlayerSettings.Android.bundleVersionCode; 
            iosVersion = PlayerSettings.bundleVersion;
            lastIosVersion = iosVersion;
            iosBuildNum = PlayerSettings.iOS.buildNumber;
            
        }

        androidVersion = EditorGUILayout.TextField("Android Version", androidVersion);
        androidBuildNum = int.Parse(EditorGUILayout.TextField("Android Build Number", androidBuildNum.ToString()).ToString());
        iosVersion = EditorGUILayout.TextField("IOS Version", iosVersion);
        iosBuildNum = EditorGUILayout.TextField("IOS Build Number", iosBuildNum);

        GUILayout.Label("GIT Information", EditorStyles.boldLabel);

        commit = ("New release! Version: " + PlayerSettings.bundleVersion + "-"  + DateTime.Now.ToString("dd/MM/yyyy"));
        repoUrl = "https://gitlab.com/serapion/kennig";
        git = "git";
        gitAdd = @"add -A";
        gitCommit = (@"commit -m" + commit);
        gitPush = @"push";
        path =  Directory.GetCurrentDirectory();

        gitUser = EditorGUILayout.TextField("Username", gitUser);
        gitPass = EditorGUILayout.TextField("Password", gitPass);
        commit = EditorGUILayout.TextField("Commit", commit);
        repoUrl = EditorGUILayout.TextField("Repository", repoUrl);

        
    
        GUILayout.Label("Check platform to build", EditorStyles.boldLabel);    
        buildAndroid = EditorGUILayout.Toggle("Android", buildAndroid);
        buildIOS = EditorGUILayout.Toggle("IOS", buildIOS);
        if(lastAndroidVersion != androidVersion)
        {
            changed = true;
        }
    
        if(lastIosVersion != iosVersion)
        {
            changed = true;
        }

        GUI.enabled = buildAndroid || buildIOS;
        if(GUILayout.Button("Build and Deploy"))
        {
            if( buildIOS){
                 PlayerSettings.bundleVersion = iosVersion;
                 PlayerSettings.iOS.buildNumber = iosBuildNum;
                using (System.Diagnostics.Process myProcess = new System.Diagnostics.Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    // You can start any process, HelloWorld is a do-nothing example.
                    myProcess.StartInfo.FileName = @"git-cmd";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.Start();
                    // This code assumes the process you are starting will terminate itself. 
                    // Given that is is started without a window so you cannot terminate it 
                    // on the desktop, it must terminate itself or you can do it programmatically
                    // from this application using the Kill method.
                }
                Debug.Log("Sucessfully builded IOS Application. Version: " + PlayerSettings.bundleVersion);
            }
            if( buildAndroid){
                 PlayerSettings.bundleVersion = androidVersion;
                PlayerSettings.Android.bundleVersionCode = androidBuildNum;
                Debug.Log("Sucessfully builded Android Application. Version: " + PlayerSettings.bundleVersion);
            }
        }
         GUI.enabled = true;
                    
	}


}
