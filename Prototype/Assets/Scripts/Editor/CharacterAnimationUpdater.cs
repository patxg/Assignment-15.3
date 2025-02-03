using UnityEditor;
using UnityEngine;
using System.IO;

public class CharacterAnimationUpdater : EditorWindow
{
    private string folderPath = "Assets/Animations"; // Default folder to search
    private CharacterAnimationConfig animationConfig;

    [MenuItem("Tools/Update Character Animations")]
    public static void ShowWindow()
    {
        GetWindow<CharacterAnimationUpdater>("Update Character Animations");
    }

    void OnGUI()
    {
        GUILayout.Label("Update Character Animation Config", EditorStyles.boldLabel);

        // Select the CharacterAnimationConfig
        animationConfig = (CharacterAnimationConfig)EditorGUILayout.ObjectField("Animation Config", animationConfig, typeof(CharacterAnimationConfig), false);

        // Input field for folder path
        folderPath = EditorGUILayout.TextField("Animation Folder Path", folderPath);

        if (GUILayout.Button("Search and Update"))
        {
            if (animationConfig == null)
            {
                Debug.LogError("Please assign a CharacterAnimationConfig!");
                return;
            }

            UpdateAnimationConfig();
        }
    }

    void UpdateAnimationConfig()
    {
        if (!Directory.Exists(folderPath))
        {
            Debug.LogError($"Folder does not exist: {folderPath}");
            return;
        }

        string[] animationFiles = Directory.GetFiles(folderPath, "*.anim", SearchOption.AllDirectories);

        foreach (string filePath in animationFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(filePath);

            if (clip == null) continue;

            AssignAnimation(fileName, clip);
        }

        EditorUtility.SetDirty(animationConfig);
        AssetDatabase.SaveAssets();
        Debug.Log("CharacterAnimationConfig updated with animations!");
    }

    void AssignAnimation(string name, AnimationClip clip)
    {
        if (animationConfig == null) return;

        switch (name.ToLower())
        {
            case "idle": animationConfig.idle = clip; break;
            case "walk": animationConfig.walk = clip; break;
            case "run": animationConfig.run = clip; break;
            case "sprint": animationConfig.sprint = clip; break;
            case "jumprise": animationConfig.jumpRise = clip; break;
            case "jumpmid": animationConfig.jumpMid = clip; break;
            case "jumpfall": animationConfig.jumpFall = clip; break;
            case "landing": animationConfig.landing = clip; break;
            case "frontflip": animationConfig.frontFlip = clip; break;
            case "crouch": animationConfig.crouch = clip; break;
            case "roll": animationConfig.roll = clip; break;
            case "slide": animationConfig.slide = clip; break;
            case "dashloop": animationConfig.dashLoop = clip; break;
            case "ledgehang": animationConfig.ledgeHang = clip; break;
            case "attacking": animationConfig.swordAttackA = clip; break;
            case "gunfireonehanded": animationConfig.gunFireOneHanded = clip; break;
            case "gunreload": animationConfig.gunReload = clip; break;

            default:
                Debug.LogWarning($"Animation '{name}' not recognized. Add it to the switch statement if needed.");
                break;
        }
    }
}
