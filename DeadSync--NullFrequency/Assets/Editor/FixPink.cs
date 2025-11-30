using UnityEngine;
using UnityEditor;

public class FixPink : EditorWindow
{
    [MenuItem("Tools/Fix Pink Materials (URP)")]
    public static void FixAllPink()
    {
        var mats = Resources.FindObjectsOfTypeAll<Material>();
        int fixedCount = 0;

        foreach (var mat in mats)
        {
            if (mat != null && mat.shader != null && mat.shader.name == "Hidden/InternalErrorShader")
            {
                mat.shader = Shader.Find("Universal Render Pipeline/Lit");
                fixedCount++;
            }
        }

        Debug.Log("Fixed " + fixedCount + " pink materials!");
    }
}
