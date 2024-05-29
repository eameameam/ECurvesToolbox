using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.CurveToolbox
{
    public static class EMergeClipsTab
    {
        private static List<AnimationClip> _clipsToMerge = new List<AnimationClip>();
        private static string _savePath = "Assets/";
        private static string _mergedClipName = "MergedClip";
        private static int _priorityClipIndex;

        public static void OnGUI()
        {
            GUILayout.Space(10);
            GUILayout.Label("Merge Clips", EditorStyles.boldLabel);
            GUILayout.Space(5);

            GUILayout.Label("Save Path", EditorStyles.helpBox);
            EditorGUILayout.BeginHorizontal();
            _savePath = EditorGUILayout.TextField(_savePath);
            if (GUILayout.Button("Browse", GUILayout.ExpandWidth(false)))
            {
                _savePath = EditorUtility.SaveFolderPanel("Save merged clip to folder", _savePath, "");
                if (!string.IsNullOrEmpty(_savePath))
                {
                    int assetsIndex = _savePath.IndexOf("Assets", System.StringComparison.Ordinal);
                    if (assetsIndex >= 0)
                    {
                        _savePath = _savePath.Substring(assetsIndex);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.Label("Merged Clip Name", EditorStyles.helpBox);
            _mergedClipName = EditorGUILayout.TextField(_mergedClipName);

            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Animation Clips", EditorStyles.helpBox);
            if (GUILayout.Button("+", GUILayout.Width(20)))
            {
                _clipsToMerge.Add(null);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical();
            for (int i = 0; i < _clipsToMerge.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                _clipsToMerge[i] = (AnimationClip)EditorGUILayout.ObjectField(_clipsToMerge[i], typeof(AnimationClip), false);

                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    _clipsToMerge.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndVertical();

            GUILayout.Space(10);
            Event evt = Event.current;
            Rect dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.Width(120));
            GUI.Box(dropArea, "Drag\nTo Fill\nThe List of Clips");

            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!dropArea.Contains(evt.mousePosition))
                        break;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        _clipsToMerge.Clear();
                        foreach (Object draggedObject in DragAndDrop.objectReferences)
                        {
                            AnimationClip clip = draggedObject as AnimationClip;
                            if (clip != null)
                            {
                                _clipsToMerge.Add(clip);
                            }
                        }
                    }
                    break;
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (_clipsToMerge.Count > 0)
            {
                GUILayout.Space(10);
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Select Priority Clip", EditorStyles.helpBox);
                _priorityClipIndex = EditorGUILayout.Popup(_priorityClipIndex, _clipsToMerge.ConvertAll(c => c != null ? c.name : "NULL").ToArray());
                EditorGUILayout.EndHorizontal();
            }
            
            GUILayout.Space(10);

            if (GUILayout.Button("Merge Clips", GUILayout.Height(40)))
            {
                if (_clipsToMerge.Count > 0 && !string.IsNullOrEmpty(_savePath))
                {
                    EClipsMergerUtility.MergeClips(_clipsToMerge, _savePath, _mergedClipName, _priorityClipIndex);
                }
                else
                {
                    Debug.LogError("Please add clips to merge and specify a save path.");
                }
            }
        }
    }
}
