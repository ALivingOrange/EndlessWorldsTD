/***************************************************************
*file: ScriptStylePostProcessor.cs
*author: E. Buff (in a sense)
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: 11/4/2026
*
*purpose: Make scripts adhere to class C# style sheet on creation
*
****************************************************************/
using System;
using System.IO;
using UnityEditor;

public class ScriptStylePostProcessor : UnityEditor.AssetModificationProcessor
{
    // function: OnWillCreateAsset
    // purpose: Puts some comment blocks in to match the class' C# style guide
    // when a monobehaviour script is created.
    // Has zero effect on the game itself.
    public static void OnWillCreateAsset(string assetPath)
    {
        assetPath = assetPath.Replace(".meta", "");

        if (!assetPath.EndsWith(".cs"))
            return;

        if (!File.Exists(assetPath))
            return;

        string className = Path.GetFileNameWithoutExtension(assetPath);
        string currentDate = DateTime.Now.ToString("MM/dd/yyyy");

        string template =
@"/***************************************************************
*file: #SCRIPTNAME#.cs
*author: (FILL THIS IN!)
*class: CS 4700 – Game Development
*assignment: program 4
*date last modified: #DATE#
*
*purpose: (FILL THIS IN!)
*
****************************************************************/
using UnityEngine;

public class #SCRIPTNAME# : MonoBehaviour
{
    // function: Start
    // purpose: Called on the frame when this script is enabled.
    // (SAY WHAT IT DOES SPECIFICALLY IN THIS CLASS!)
    void Start()
    {
        
    }

    // function: Update
    // purpose: Called every frame when the script is active.
    // (SAY WHAT IT DOES SPECIFICALLY IN THIS CLASS!)
    void Update()
    {
        
    }
}
";

        string finalContent = template
            .Replace("#SCRIPTNAME#", className)
            .Replace("#DATE#", currentDate);

        File.WriteAllText(assetPath, finalContent);
        AssetDatabase.Refresh();
    }
}
