using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Cuy), true)]
[CanEditMultipleObjects]
public class CuyEditor : Editor
{
    private SerializedProperty cuyNameProperty;
    private SerializedProperty initSpeedProperty;
    private SerializedProperty fatigueProperty;
    private SerializedProperty imageProperty;

    private void OnEnable()
    {
        cuyNameProperty = serializedObject.FindProperty("cuyName");
        initSpeedProperty = serializedObject.FindProperty("initSpeed");
        fatigueProperty = serializedObject.FindProperty("fatigue");
        imageProperty = serializedObject.FindProperty("image");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(cuyNameProperty);
        EditorGUILayout.PropertyField(initSpeedProperty);
        EditorGUILayout.PropertyField(fatigueProperty);
        EditorGUILayout.PropertyField(imageProperty);

        GUILayout.Space(20);

        if (GUILayout.Button("Set Cuy Sprite"))
        {
            foreach (var obj in targets)
            {
                Cuy cuy = (Cuy)obj;
                Image cuyImage = cuy.GetComponent<Image>();
                if (cuyImage != null)
                {
                    cuyImage.sprite = cuy.Image;
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}