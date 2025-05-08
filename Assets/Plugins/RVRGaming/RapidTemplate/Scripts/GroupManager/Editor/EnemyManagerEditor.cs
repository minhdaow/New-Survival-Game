using GameCreator.Editor.Common;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using ETK;

[CustomEditor(typeof(EnemyManager))]
public class MyScriptEditorEnemyManager : Editor
{
    Texture2D headerImage;

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        var header = new Image() { image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Plugins/RVRGaming/RapidTemplate/Textures/Editor/HeaderImageGroupManager.png") };
        header.style.maxHeight = 34;
        header.style.maxWidth = 200;
        header.style.marginLeft = 0;
        root.Add(header);

        root.Add(new SpaceSmall());

        root.Add(new PropertyField(serializedObject.FindProperty("detectionRadius")));
        root.Add(new PropertyField(serializedObject.FindProperty("aggression")));
        root.Add(new PropertyField(serializedObject.FindProperty("m_Target")));
        root.Add(new PropertyField(serializedObject.FindProperty("enemies")));

        root.Add(new PropertyField(serializedObject.FindProperty("enableAwareness")));

        root.Add(new PropertyField(serializedObject.FindProperty("Communicate")));
        root.Add(new PropertyField(serializedObject.FindProperty("CommunicationRange")));
        root.Add(new PropertyField(serializedObject.FindProperty("CommunicationMask")));

        root.Add(new SpaceSmall());

        return root;
    }
}