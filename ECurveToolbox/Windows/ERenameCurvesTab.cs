using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.CurveToolbox
{
    public static class ERenameCurvesTab
    {
        private static string _oldName = "";
        private static string _newName = "";
        private static bool _renameAll = true;
        private static bool _setDirty = true;
        private static bool _scaleToggle = true;
        private static bool _rotationToggle = true;
        private static bool _positionToggle = true;
        private static readonly bool[] _scaleAxes = new bool[] { true, true, true };
        private static readonly bool[] _rotationAxes = new bool[] { true, true, true };
        private static readonly bool[] _positionAxes = new bool[] { true, true, true };
        private static List<AnimationClip> _clipsToRename = new List<AnimationClip>();

        public static void OnGUI()
        {
            GUILayout.Space(10);
            GUILayout.Label("Rename Curves", EditorStyles.boldLabel);
            GUILayout.Space(5);

            GUILayout.Label("Old Name", EditorStyles.helpBox);
            _oldName = EditorGUILayout.TextField(_oldName);

            GUILayout.Space(10);

            GUILayout.Label("New Name", EditorStyles.helpBox);
            _newName = EditorGUILayout.TextField(_newName);

            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            _renameAll = EditorGUILayout.ToggleLeft("Rename All", _renameAll, GUILayout.Width(100));
            _setDirty = EditorGUILayout.ToggleLeft("Save Assets Database", _setDirty, GUILayout.Width(150));
            EditorGUILayout.EndHorizontal();
            
            if (!_renameAll)
            {
                GUILayout.Label("Individual Curves Selection", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                
                // Scale
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                _scaleToggle = EditorGUILayout.Toggle(_scaleToggle, GUILayout.Width(20));
                GUILayout.Label("Scale", GUILayout.Width(60));
                EditorGUILayout.EndHorizontal();
                if (_scaleToggle)
                {
                    GUILayout.BeginHorizontal();
                    _scaleAxes[0] = EditorGUILayout.ToggleLeft("X", _scaleAxes[0], GUILayout.Width(30));
                    _scaleAxes[1] = EditorGUILayout.ToggleLeft("Y", _scaleAxes[1], GUILayout.Width(30));
                    _scaleAxes[2] = EditorGUILayout.ToggleLeft("Z", _scaleAxes[2], GUILayout.Width(30));
                    GUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
                
                GUILayout.Space(10);
                
                // Rotation
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                _rotationToggle = EditorGUILayout.Toggle(_rotationToggle, GUILayout.Width(20));
                GUILayout.Label("Rotation", GUILayout.Width(60));
                EditorGUILayout.EndHorizontal();
                if (_rotationToggle)
                {
                    GUILayout.BeginHorizontal();
                    _rotationAxes[0] = EditorGUILayout.ToggleLeft("X", _rotationAxes[0], GUILayout.Width(30));
                    _rotationAxes[1] = EditorGUILayout.ToggleLeft("Y", _rotationAxes[1], GUILayout.Width(30));
                    _rotationAxes[2] = EditorGUILayout.ToggleLeft("Z", _rotationAxes[2], GUILayout.Width(30));
                    GUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();

                GUILayout.Space(10);

                // Position
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal();
                _positionToggle = EditorGUILayout.Toggle(_positionToggle, GUILayout.Width(20));
                GUILayout.Label("Position", GUILayout.Width(60));
                EditorGUILayout.EndHorizontal();
                if (_positionToggle)
                {
                    GUILayout.BeginHorizontal();
                    _positionAxes[0] = EditorGUILayout.ToggleLeft("X", _positionAxes[0], GUILayout.Width(30));
                    _positionAxes[1] = EditorGUILayout.ToggleLeft("Y", _positionAxes[1], GUILayout.Width(30));
                    _positionAxes[2] = EditorGUILayout.ToggleLeft("Z", _positionAxes[2], GUILayout.Width(30));
                    GUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
                
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Animation Clips", EditorStyles.helpBox);
            if (GUILayout.Button("+", GUILayout.Width(20)))
            {
                _clipsToRename.Add(null);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical();
            for (int i = 0; i < _clipsToRename.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                _clipsToRename[i] = (AnimationClip)EditorGUILayout.ObjectField(_clipsToRename[i], typeof(AnimationClip), false);

                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    _clipsToRename.RemoveAt(i);
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

                        _clipsToRename.Clear();
                        foreach (Object draggedObject in DragAndDrop.objectReferences)
                        {
                            AnimationClip clip = draggedObject as AnimationClip;
                            if (clip != null)
                            {
                                _clipsToRename.Add(clip);
                            }
                        }
                    }
                    break;
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Rename Curves", GUILayout.Height(40)))
            {
                if (!string.IsNullOrEmpty(_oldName) && !string.IsNullOrEmpty(_newName) && _clipsToRename.Count > 0)
                {
                    CurveRenamerUtility.RenameCurves(_clipsToRename, _oldName, _newName, _setDirty, _renameAll, _scaleAxes, _rotationAxes, _positionAxes);
                }
                else
                {
                    Debug.LogError("Please specify old and new names, and add clips to rename.");
                }
            }

            if (GUILayout.Button("Undo Rename", GUILayout.Height(40)))
            {
                foreach (AnimationClip clip in _clipsToRename)
                {
                    Undo.PerformUndo();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
