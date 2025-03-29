using System.IO;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class FolderIconEditor
{
    static FolderIconEditor()
    {
        SetFolderIcon("Assets/Art", "Assets/Editor/PalThing.png");
        SetFolderIcon("Assets/Editor", "Assets/Editor/TerrierSwag.png");
        SetFolderIcon("Assets/Imports", "Assets/Editor/finDerp.png");
        SetFolderIcon("Assets/ParrelSync", "Assets/Editor/BubbleGumOk.png");
        SetFolderIcon("Assets/Plugins", "Assets/Editor/SansDunk.png");
        SetFolderIcon("Assets/Prefabs", "Assets/Editor/Tryanno.png");
        SetFolderIcon("Assets/Scenes", "Assets/Editor/Spectating.png");
        SetFolderIcon("Assets/ScriptableObjects", "Assets/Editor/ClaraShy.png");
        SetFolderIcon("Assets/Scripts", "Assets/Editor/Scissor7What.png");
        SetFolderIcon("Assets/Scripts/AdversaryScripts", "Assets/Editor/ariaSus.png");
        SetFolderIcon("Assets/Scripts/DataStuffs", "Assets/Editor/NotesPerson.png");
        SetFolderIcon("Assets/Scripts/Management", "Assets/Editor/DigimonCoolio.png");
        SetFolderIcon("Assets/Scripts/OnlineStuffs", "Assets/Editor/ArcadeWut.png");
        // SetFolderIcon("Assets/Scripts/PlayerScripts", "Assets/Editor/ariaSus.png");
        SetFolderIcon("Assets/Scripts/UIManagement", "Assets/Editor/AA_Spin.gif");
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
