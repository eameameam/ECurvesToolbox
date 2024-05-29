using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.CurveToolbox
{
    public static class CurveRemoverUtility
    {
        public static void DeleteCurveFromSelectedClips(string path, bool includeChildren, bool deletePosition, bool deleteRotation, bool deleteScale, bool cutCurveAndChangeHierarchy, bool saveAssets)
        {
            foreach (Object obj in Selection.objects)
            {
                if (obj is AnimationClip clip)
                {
                    var bindings = AnimationUtility.GetCurveBindings(clip);
                    var pathsToDelete = new List<EditorCurveBinding>();
                    var curvesToRebind = new Dictionary<EditorCurveBinding, AnimationCurve>();

                    foreach (var binding in bindings)
                    {
                        bool isChild = IsChildOfPath(binding.path, path);
                        bool shouldModifyHierarchy = cutCurveAndChangeHierarchy && (binding.path == path || isChild);
                        bool shouldDelete = binding.path == path || (includeChildren && isChild);

                        if (shouldModifyHierarchy)
                        {
                            var modifiedPath = ModifyPath(binding.path, path);
                            var curve = AnimationUtility.GetEditorCurve(clip, binding);
                            var newBinding = new EditorCurveBinding { path = modifiedPath, propertyName = binding.propertyName, type = binding.type };
                            curvesToRebind[newBinding] = curve;
                        }
                        else if (shouldDelete && ShouldDeleteCurve(binding, deletePosition, deleteRotation, deleteScale))
                        {
                            pathsToDelete.Add(binding);
                        }
                    }

                    ProcessCurves(clip, pathsToDelete, curvesToRebind, saveAssets);
                }
            }
        }

        private static bool ShouldDeleteCurve(EditorCurveBinding binding, bool deletePosition, bool deleteRotation, bool deleteScale)
        {
            return (deletePosition && binding.propertyName.Contains("m_LocalPosition")) ||
                   (deleteRotation && binding.propertyName.Contains("m_LocalRotation")) ||
                   (deleteScale && binding.propertyName.Contains("m_LocalScale"));
        }

        private static void ProcessCurves(AnimationClip clip, List<EditorCurveBinding> pathsToDelete, Dictionary<EditorCurveBinding, AnimationCurve> curvesToRebind, bool saveAssets)
        {
            bool changesMade = false;

            Undo.RecordObject(clip, "Modify Animation Curves");

            foreach (var binding in pathsToDelete)
            {
                AnimationUtility.SetEditorCurve(clip, binding, null);
                changesMade = true;
            }

            foreach (var kvp in curvesToRebind)
            {
                AnimationUtility.SetEditorCurve(clip, kvp.Key, kvp.Value);
                changesMade = true;
            }

            if (changesMade)
            {
                EditorUtility.SetDirty(clip);

                if (saveAssets)
                {
                    AssetDatabase.SaveAssets();
                    Debug.Log("Assets saved.");
                }

                Undo.FlushUndoRecordObjects();
            }
        }

        private static string ModifyPath(string originalPath, string segmentToRemove)
        {
            if (!originalPath.Contains(segmentToRemove)) return originalPath;

            var pattern = segmentToRemove + (segmentToRemove.EndsWith("/") ? "" : "/");
            var modifiedPath = originalPath.Replace(pattern, "");
            return modifiedPath;
        }

        private static bool IsChildOfPath(string childPath, string parentPath)
        {
            return childPath.StartsWith(parentPath + "/", System.StringComparison.Ordinal);
        }
    }
}
