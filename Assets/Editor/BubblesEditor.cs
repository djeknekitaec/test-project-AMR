using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(BubblesManager))]
public class BubblesEditor : Editor
{
    bool foldOut = false;
    List<bool> foldOutsList = new List<bool>();
    Texture bubbleTexture;

    private void Awake()
    {
        bubbleTexture = Resources.Load("bubble") as Texture;
        foldOutsList = new List<bool>();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BubblesManager bubblesManager = (BubblesManager)target;
        if (bubblesManager.bubbleSettings.Count > 0)
        {
            foldOut = EditorGUILayout.Foldout(foldOut, "Bubble Types");
        }

        if (foldOut)
        {
            if (bubblesManager.bubbleSettings.Count < foldOutsList.Count)
            {
                if (bubblesManager.bubbleSettings.Count == 0)
                {
                    foldOutsList.RemoveRange(0, foldOutsList.Count);
                }
                else
                {
                    foldOutsList.RemoveRange(bubblesManager.bubbleSettings.Count - 1, foldOutsList.Count - bubblesManager.bubbleSettings.Count);
                }
            }

            for (int i = 0; i < bubblesManager.bubbleSettings.Count; i++)
            {
                if (foldOutsList.Count - 1 < i)
                {
                    foldOutsList.Add(true);
                }

                foldOutsList[i] = EditorGUILayout.Foldout(foldOutsList[i], "Bubble: " + i.ToString());

                if (foldOutsList[i])
                {
                    GUILayout.Space(20);
                    BubblesManager.BubbleSettings bubbleSetting = bubblesManager.bubbleSettings[i];

                    GUILayout.BeginHorizontal();

                    GUIStyle style = new GUIStyle();
                    float size = 50f * bubbleSetting.size / bubblesManager.maxSize;
                    style.fixedHeight = size;
                    style.fixedWidth = size;
                    GUI.contentColor = bubbleSetting.color;
                    GUILayout.Label(bubbleTexture, style);

                    GUILayout.BeginVertical();

                    GUILayout.Label("Bubble Color:");
                    bubbleSetting.color = EditorGUILayout.ColorField(bubblesManager.bubbleSettings[i].color);

                    GUILayout.EndVertical();

                    GUILayout.EndHorizontal();


                    GUILayout.Label("Bubble Size:");
                    bubbleSetting.size = EditorGUILayout.Slider(bubbleSetting.size, bubblesManager.minSize, bubblesManager.maxSize);
                    GUILayout.Label("Bubble Speed:");
                    bubbleSetting.speed = EditorGUILayout.Slider(bubbleSetting.speed, bubblesManager.minSpeed, bubblesManager.maxSpeed);
                    bubblesManager.bubbleSettings[i] = bubbleSetting;
                }
            }
        }

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Bubble"))
        {
            bubblesManager.AddNewBubbleType();
        }
        if (bubblesManager.bubbleSettings.Count > 0)
        {
            if (GUILayout.Button("Remove Bubble"))
            {
                bubblesManager.RemoveBubbleType();
            }
        }

        GUILayout.EndHorizontal();
    }

}
