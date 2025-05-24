#if EBS_GAMECREATORV2_INVENTORY
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

using UnityEngine.Events;
using UnityEditor.Events;

using EasyBuildSystem.Features.Runtime.Buildings.Manager;
using EasyBuildSystem.Features.Runtime.Buildings.Part;

using EasyBuildSystem.Features.Editor.Extensions;
using EasyBuildSystem.Features.Editor.Extensions.ReorderableList;

[CustomEditor(typeof(UICircularBuildingMenuForGCInventory))]
public class UICircularBuildingMenuEditorForGCInventory : UnityEditor.Editor
{
    #region Fields

    bool m_GeneralFoldout = true;
    bool m_InputFoldout;
    bool m_CategoryFoldout = true;
    bool m_UIFoldout = true;

    readonly bool[] m_CategoriesFoldout = new bool[999];

    ReorderableList[] m_ReorderableList = new ReorderableList[128];

    int m_Counter;

    UICircularBuildingMenuForGCInventory Target
    {
        get
        {
            return ((UICircularBuildingMenuForGCInventory)target);
        }
    }

    #endregion

    #region Unity Methods

    void OnEnable()
    {
        m_ReorderableList = new ReorderableList[128];

        for (int i = 0; i < serializedObject.FindProperty("m_Categories").arraySize; i++)
        {
            m_ReorderableList[i] =
                new ReorderableList(serializedObject.FindProperty("m_Categories").GetArrayElementAtIndex(i).FindPropertyRelative("m_Buttons"));
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUIUtilityExtension.DrawHeader("Easy Build System - UI Building Circular Menu",
            "Restricts the building placing, destroying and editing.\n" +
            "Find more information about the Building Area component in the documentation.");

        EditorGUILayout.HelpBox("Circular Building Menu can update automatically during edit mode.\n" +
            "You can break the connection with the root prefab during the modifications to refresh the menu.", MessageType.Info);

        m_GeneralFoldout = EditorGUIUtilityExtension.BeginFoldout(new GUIContent("General Settings"), m_GeneralFoldout);

        if (m_GeneralFoldout)
        {
#if EBS_INPUT_SYSTEM_SUPPORT
        EditorGUILayout.HelpBox("New Input System was detecting, you can change the inputs directly in the input action file.\n" +
                                "You can follow the documentation to have more information about New Input System.", MessageType.Info);

        if (GUILayout.Button("Edit Input Action Settings..."))
        {
            if (Resources.Load<UnityEngine.InputSystem.InputActionAsset>("Input Actions") != null)
            {
                Selection.activeObject = Resources.Load<UnityEngine.InputSystem.InputActionAsset>("Input Actions");
            }
            else
            {
                Debug.LogWarning("The default input profile <b>Input Actions</b> could be not found, the file not existing or has been renamed.");
            }
        }
#endif

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_PlatformTarget"), new GUIContent("Platform Target"));

            if ((UICircularBuildingMenuForGCInventory.PlatformTarget)serializedObject.FindProperty("m_PlatformTarget").enumValueIndex == UICircularBuildingMenuForGCInventory.PlatformTarget.STANDALONE)
            {
#if !ENABLE_INPUT_SYSTEM
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_KeyboardToggleKey"), new GUIContent("Toggle Action Key"));
#endif
            }
            else if ((UICircularBuildingMenuForGCInventory.PlatformTarget)serializedObject.FindProperty("m_PlatformTarget").enumValueIndex == UICircularBuildingMenuForGCInventory.PlatformTarget.GAMEPAD)
            {
                EditorGUILayout.HelpBox("Gamepad support work only with the New Input System.\n" +
                    "You can find more information about New Input System support on the documentation.", MessageType.Warning);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_DisableGameObjectsWhenOpen"), new GUIContent("Disable GameObjects When Open"));
        }

        EditorGUIUtilityExtension.EndFoldout();

#if ENABLE_INPUT_SYSTEM
        m_InputFoldout = EditorGUIUtilityExtension.BeginFoldout(new GUIContent("Input Settings"), m_InputFoldout);

        if (m_InputFoldout)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_ToggleInputReference"),
                new GUIContent("Toggle Input Reference"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_SelectInputReference"),
                new GUIContent("Select Input Reference"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_ValidateInputReference"),
                new GUIContent("Validate Input Reference"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_CancelInputReference"),
                new GUIContent("Cancel Input Reference"));
        }

        EditorGUIUtilityExtension.EndFoldout();
#else
            m_InputFoldout = EditorGUIUtilityExtension.BeginFoldout(new GUIContent("Input Settings"), m_InputFoldout);

            if (m_InputFoldout)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_KeyboardToggleKey"),
                    new GUIContent("Toggle Input Reference"));
            }

            EditorGUIUtilityExtension.EndFoldout();
#endif

        m_CategoryFoldout = EditorGUIUtilityExtension.BeginFoldout(new GUIContent("Category Settings"), m_CategoryFoldout);

        if (m_CategoryFoldout)
        {
            string[] names = new string[Target.Categories.Count];
            int[] sizes = new int[names.Length];

            for (int i = 0; i < Target.Categories.Count; i++)
            {
                names[i] = Target.Categories[i].Name;
                sizes[i] = i;
            }

            serializedObject.FindProperty("m_DefaultCategory").intValue =
                EditorGUILayout.IntPopup("Default Category", serializedObject.FindProperty("m_DefaultCategory").intValue,
                names, sizes);

            EditorGUIUtilityExtension.BeginVertical();

            GUILayout.Space(5f);

            GUI.enabled = false;

            serializedObject.FindProperty("m_Categories").arraySize =
                EditorGUILayout.IntField("Category Size", serializedObject.FindProperty("m_Categories").arraySize);

            GUI.enabled = true;

            EditorGUILayout.Separator();

            for (int i = 0; i < serializedObject.FindProperty("m_Categories").arraySize; i++)
            {
                m_CategoriesFoldout[i] =
                    EditorGUIUtilityExtension.BeginFoldout(new GUIContent(serializedObject.FindProperty("m_Categories").GetArrayElementAtIndex(i).displayName),
                    m_CategoriesFoldout[i]);

                GUILayout.Space(-18);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Duplicate", GUILayout.Width(80)))
                {
                    UICircularBuildingMenuForGCInventory.CircularCategory duplicateCateogry = Target.Categories[i];
                    Target.Categories.Add(duplicateCateogry);
                }
                if (GUILayout.Button("Remove", GUILayout.Width(80)))
                {
                    DestroyImmediate(Target.Categories[i].ContentTransform);
                    Target.Categories.RemoveAt(i);
                }
                GUILayout.EndHorizontal();

                if (m_CategoriesFoldout[i])
                {
                    EditorGUI.BeginChangeCheck();

                    EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Categories").GetArrayElementAtIndex(i).FindPropertyRelative("m_Name"));

                    if (EditorGUI.EndChangeCheck())
                    {
                        Target.Categories[i].ContentTransform.name =
                            serializedObject.FindProperty("m_Categories").GetArrayElementAtIndex(i).FindPropertyRelative("m_Name").stringValue;
                    }

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(13f);
                    GUILayout.BeginVertical();

                    if (m_ReorderableList[i] != null)
                    {
                        m_ReorderableList[i].Layout();
                    }

                    GUILayout.EndVertical();
                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Load Building Part Reference As Buttons..."))
                    {
                        bool canLoad = true;

                        for (int x = 0; x < BuildingManager.Instance.BuildingPartReferences.Count; x++)
                        {
                            if (BuildingManager.Instance.BuildingPartReferences[x].GetComponent<GameCreatorBuildingEntity>() == null)
                            {
                                canLoad = false;
                            }
                        }

                        if (canLoad)
                        {
                            Debug.LogError("<b>Easy Build System</b> Please make sure that your Building Parts have the component (RPGBuilderBuildingEntity) attached to them!");
                            return;
                        }

                        for (int x = 0; x < BuildingManager.Instance.BuildingPartReferences.Count; x++)
                        {
                            BuildingPart buildingPart = BuildingManager.Instance.BuildingPartReferences[x];

                            UICircularBuildingMenuForGCInventory.CircularButtonSettings instancedButton = new UICircularBuildingMenuForGCInventory.CircularButtonSettings()
                            {
                                Name = buildingPart.GetGeneralSettings.Name,
                                Description = buildingPart.GetGeneralSettings.Type,
                                Icon = buildingPart.GetGeneralSettings.Thumbnail,
                                BuildingEntity = buildingPart.GetComponent<GameCreatorBuildingEntity>(),
                                Action = new UnityEvent()
                            };

                            UnityEventTools.AddStringPersistentListener(instancedButton.Action,
                                new UnityAction<string>(Target.SelectBuilding),
                                BuildingManager.Instance.BuildingPartReferences[x].GetGeneralSettings.Identifier);

                            Target.Categories[i].Buttons.Add(instancedButton);
                            Target.RefreshMenu();
                        }
                    }

                    if (GUILayout.Button("Clear All Buttons..."))
                    {
                        Target.Categories[i].Buttons = new List<UICircularBuildingMenuForGCInventory.CircularButtonSettings>();
                    }
                }

                EditorGUIUtilityExtension.EndFoldout();
            }

            EditorGUIUtilityExtension.EndVertical();

            if (GUILayout.Button("Create Category..."))
            {
                GameObject parent = new GameObject("New Container");
                parent.transform.SetParent(Target.transform);

                Target.Categories.Add(new UICircularBuildingMenuForGCInventory.CircularCategory()
                { Name = "New Category", ContentTransform = parent });
            }
        }

        EditorGUIUtilityExtension.EndFoldout();

        m_UIFoldout = EditorGUIUtilityExtension.BeginFoldout(new GUIContent("UI References"), m_UIFoldout);

        if (m_UIFoldout)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_CanvasGroup"), new GUIContent("Canvas Group"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_UICircularSelectionFillImage"), new GUIContent("Circular Selection Fill Image"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_UICircularSelectionCenterFillImage"), new GUIContent("Circular Selection Center Image"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_UICircularSelectionIcon"), new GUIContent("Circular Selection Icon"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_UICircularSelectionText"), new GUIContent("Circular Selection Text"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_UICircularSelectionDescription"), new GUIContent("Circular Selection Description"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_UICircularButtonPrefab"), new GUIContent("Circular Button Prefab"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_UICircularButtonNormalColor"), new GUIContent("Circular Button Normal Color"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_UICircularButtonNormalScale"), new GUIContent("Circular Button Normal Scale"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_UICircularButtonHoverColor"), new GUIContent("Circular Button Hover Color"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_UICircularButtonHoverScale"), new GUIContent("Circular Button Hover Scale"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_UICircularButtonSpacing"), new GUIContent("Circular Button Spacing"));
        }

        EditorGUIUtilityExtension.EndFoldout();

        if (Target.CanvasGroup != null && Target.CanvasGroup.alpha == 0)
        {
            if (GUILayout.Button("Show Circular Building Menu..."))
            {
                Target.CanvasGroup.alpha = 1f;
            }
        }
        else
        {
            if (GUILayout.Button("Hide Circular Building Menu..."))
            {
                Target.CanvasGroup.alpha = 0f;
            }
        }

        m_Counter++;
        if (m_Counter > 8)
        {
            m_Counter = 0;
            Target.RefreshMenu();
        }

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }

    #endregion
}
#endif