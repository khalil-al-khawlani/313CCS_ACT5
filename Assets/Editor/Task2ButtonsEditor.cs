using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(Task2Buttons))]
public class Task2ButtonsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10f);

        if (GUILayout.Button("Create Task 2 Interface"))
        {
            Task2Buttons task2Buttons = (Task2Buttons)target;
            task2Buttons.CreateTask2Interface();
            EditorUtility.SetDirty(task2Buttons);
            if (task2Buttons.gameObject.scene.IsValid())
            {
                EditorSceneManager.MarkSceneDirty(task2Buttons.gameObject.scene);
            }
        }
    }
}
