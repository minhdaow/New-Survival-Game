using UnityEditor;
using UnityEngine;

public class ToggleActiveShortcut
{
    [MenuItem("Tools/Toggle Active %#&a")] // Shift + Alt + A
    private static void ToggleActive()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Undo.RecordObject(obj, "Toggle Active");
            obj.SetActive(!obj.activeSelf);
            EditorUtility.SetDirty(obj);
        }
    }
}
