using UnityEditor;
using UnityEditor.Callbacks;
using System;

public static class KinectFacePostBuildCopyPluginData
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string path)
    {
        KinectCopyPluginDataHelper.CopyPluginData (target, path, "NuiDatabase");
    }
}
