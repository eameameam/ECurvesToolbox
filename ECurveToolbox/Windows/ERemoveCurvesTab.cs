using UnityEditor;
using UnityEngine;

namespace Editor.CurveToolbox
{
    public static class ERemoveCurvesTab
    {
        private static string _curvePath = "";
        private static bool _includeChildren;
        private static bool _deletePosition = true;
        private static bool _deleteRotation = true;
        private static bool _deleteScale = true;
        private static bool _cutCurveAndChangeHierarchy;
        private static bool _saveAssets = true;

        public static void OnGUI()
        {
            GUILayout.Space(10);
            GUILayout.Label("Remove Curves", EditorStyles.boldLabel);
            GUILayout.Space(5);

            GUILayout.Label("Curve Path", EditorStyles.helpBox);
            _curvePath = EditorGUILayout.TextField(_curvePath);

            GUILayout.Space(10);

            _includeChildren = EditorGUILayout.Toggle("Include Children", _includeChildren);
            _cutCurveAndChangeHierarchy = EditorGUILayout.Toggle("Cut Curve/Change Hierarchy", _cutCurveAndChangeHierarchy);

            GUILayout.Space(10);

            _deletePosition = EditorGUILayout.Toggle("Delete Position Curves", _deletePosition);
            _deleteRotation = EditorGUILayout.Toggle("Delete Rotation Curves", _deleteRotation);
            _deleteScale = EditorGUILayout.Toggle("Delete Scale Curves", _deleteScale);

            GUILayout.Space(10);

            _saveAssets = EditorGUILayout.Toggle("Save Assets", _saveAssets);

            GUILayout.Space(20);

            if (GUILayout.Button("Delete Curves", GUILayout.Height(40)))
            {
                CurveRemoverUtility.DeleteCurveFromSelectedClips(_curvePath, _includeChildren, _deletePosition, _deleteRotation, _deleteScale, _cutCurveAndChangeHierarchy, _saveAssets);
            }
        }
    }
}