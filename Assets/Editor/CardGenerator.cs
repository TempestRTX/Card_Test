using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class CardGenerator : EditorWindow
{
    private static string[] cardCodes = new string[]
    {
        "HA", "H7", "S4", "C7", "SJ", "S7", "C2", "SQ", "H8", "S8",
        "D5", "DK", "D10", "HQ", "CJ", "H6", "C4", "D7", "D4", "H5",
        "C10", "D9", "D8", "C9"
    };

    private const string spriteFolderPath = "Assets/Sprites/Cards";
    private const string outputFolderPath = "Assets/Cards/Generated";

    [MenuItem("Tools/Generate Playing Cards")]
    public static void GenerateCards()
    {
        if (!Directory.Exists(outputFolderPath))
        {
            Directory.CreateDirectory(outputFolderPath);
        }

        Dictionary<string, Sprite> spriteDict = LoadAllSprites(spriteFolderPath);

        foreach (string code in cardCodes)
        {
            Sprite sprite = spriteDict.ContainsKey(code) ? spriteDict[code] : null;

            if (sprite == null)
            {
                Debug.LogWarning($"Sprite not found for card code: {code}");
                continue;
            }

            PlayingCard card = ScriptableObject.CreateInstance<PlayingCard>();
            SetCardValues(card, code, sprite);

            string assetPath = Path.Combine(outputFolderPath, code + ".asset");
            AssetDatabase.CreateAsset(card, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Card generation complete.");
    }

    private static Dictionary<string, Sprite> LoadAllSprites(string folderPath)
    {
        string[] spriteGUIDs = AssetDatabase.FindAssets("t:Sprite", new[] { folderPath });
        Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

        foreach (string guid in spriteGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            string name = Path.GetFileNameWithoutExtension(path);
            if (!sprites.ContainsKey(name))
            {
                sprites[name] = sprite;
            }
        }

        return sprites;
    }

    private static void SetCardValues(PlayingCard card, string code, Sprite sprite)
    {
        typeof(PlayingCard).GetField("cardName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(card, code);
        typeof(PlayingCard).GetField("cardCode", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(card, code);
        typeof(PlayingCard).GetField("cardSprite", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(card, sprite);
    }
}
