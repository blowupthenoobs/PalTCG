using System.IO;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class FolderIconEditor
{
    static FolderIconEditor()
    {
        SetFolderIcon("Assets/Art", "Assets/Editor/PalThing.png");
    }

    private static void SetFolderIcon(string folderPath, string iconPath)
    {
        Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>(iconPath);
        if (icon == null) return;
        var iconContent = new GUIContent(icon);

        EditorApplication.projectWindowItemOnGUI += (guid, rect) =>
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (assetPath == folderPath)
            {
                Rect iconRect = rect;

                if (rect.height > 20)
                {
                    iconRect = new Rect(rect.x - 1, rect.y - 7, rect.width + 2, rect.height + 2);
                }
                else if (rect.x > 20)
                {
                    iconRect = new Rect(rect.x - 1, rect.y - 1, rect.height + 2, rect.height + 2);
                }
                else
                {
                    iconRect = new Rect(rect.x + 2, rect.y - 1, rect.height + 2, rect.height + 2);
                }
                
                GUI.Label(iconRect, iconContent);
            }
        };
    }
}
