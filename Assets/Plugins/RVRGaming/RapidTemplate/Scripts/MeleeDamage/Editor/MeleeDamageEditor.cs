using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(MeleeDamage))]
public class MeleeDamageEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        root.Add(new Label(" "));

        root.Add(new PropertyField(serializedObject.FindProperty("maxHitValue"), "Max Hit Value"));
        root.Add(new PropertyField(serializedObject.FindProperty("recoveryRate"), "Recovery Rate"));

        root.Add(new VisualElement { style = { height = 10 } });

        VisualElement row = new VisualElement();
        row.style.flexDirection = FlexDirection.Row;
        row.style.alignItems = Align.Center;

        Label label = new Label("Current Value:");
        label.style.flexBasis = 150;
        label.style.marginRight = 4;
        row.Add(label);

        ProgressBar progressBar = new ProgressBar();
        progressBar.style.flexGrow = 1;
        progressBar.style.height = 18;
        progressBar.style.minWidth = 150;
        row.Add(progressBar);

        root.Add(row);

        root.schedule.Execute(() =>
        {
            MeleeDamage meleeDamage = (MeleeDamage)target;
            float maxHit = meleeDamage.maxHitValue > 0 ? meleeDamage.maxHitValue : 1f;
            float percentage = meleeDamage.currentValue / maxHit;

            progressBar.value = percentage * 100f;
            progressBar.title = $"{meleeDamage.currentValue:F2} / {meleeDamage.maxHitValue:F2}";
        }).Every(100);

        return root;
    }
}
