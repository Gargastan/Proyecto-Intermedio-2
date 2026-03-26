using UnityEditor;
using UnityEngine;

public class CameraEditorTools : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] debuggingElements;

    [ContextMenu("Toggle Debugging Elements")]
    public void ToggleDebuggingElements()
    {
        if (debuggingElements.Length == 0) return;

        bool newState = !debuggingElements[0].enabled;

        foreach (var element in debuggingElements)
        {
            if (element != null)
                element.enabled = newState;
        }
    }
}
#if UNITY_EDITOR

[CustomEditor(typeof(CameraEditorTools))]
public class CameraEditorToolsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CameraEditorTools script = (CameraEditorTools)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Toggle Debugging Elements"))
        {
            script.ToggleDebuggingElements();
        }
    }
}
#endif
