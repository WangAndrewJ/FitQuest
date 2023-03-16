using UnityEngine;
using UnityEditor;

public class DeletePlayerPrefsEditorWindow : EditorWindow
{
    [MenuItem("Window/Delete All PlayerPrefs")]
    static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}