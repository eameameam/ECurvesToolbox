using UnityEditor;
using UnityEngine;

namespace Editor.CurveToolbox
{
    public static class EAddCurvesTab
    {
        private static AnimationClip _sourceClip;
        private static AnimationClip _targetClip;

        public static void OnGUI()
        {
            GUILayout.Space(10);
            GUILayout.Label("Add Curves From Source Clip to Target Clip", EditorStyles.boldLabel);
            GUILayout.Space(5);

            GUILayout.Label("Source Animation Clip", EditorStyles.helpBox);
            _sourceClip = (AnimationClip)EditorGUILayout.ObjectField(_sourceClip, typeof(AnimationClip), false);

            GUILayout.Space(10);

            GUILayout.Label("Target Animation Clip", EditorStyles.helpBox);
            _targetClip = (AnimationClip)EditorGUILayout.ObjectField(_targetClip, typeof(AnimationClip), false);

            GUILayout.Space(20);

            if (GUILayout.Button("Add Curves", GUILayout.Height(40)))
            {
                if (_sourceClip != null && _targetClip != null)
                {
                    EClipsAdderUtility.AddCurvesFromClip(_sourceClip, _targetClip);
                }
                else
                {
                    Debug.LogError("Please specify both source and target clips.");
                }
            }
        }
    }
}