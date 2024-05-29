using UnityEditor;
using UnityEngine;

namespace Editor.CurveToolbox
{
    public static class EClipsAdderUtility
    {
        public static void AddCurvesFromClip(AnimationClip sourceClip, AnimationClip targetClip)
        {
            if (sourceClip == null)
            {
                Debug.LogError("Source clip is null.");
                return;
            }

            if (targetClip == null)
            {
                Debug.LogError("Target clip is null.");
                return;
            }

            Undo.RecordObject(targetClip, "Add Curves from Clip");

            foreach (EditorCurveBinding binding in AnimationUtility.GetCurveBindings(sourceClip))
            {
                AnimationCurve curve = AnimationUtility.GetEditorCurve(sourceClip, binding);
                AnimationUtility.SetEditorCurve(targetClip, binding, curve);
            }

            AnimationEvent[] events = AnimationUtility.GetAnimationEvents(sourceClip);
            foreach (var evt in events)
            {
                targetClip.AddEvent(evt);
            }

            EditorUtility.SetDirty(targetClip);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("Curves added from " + sourceClip.name + " to " + targetClip.name);
        }
    }
}