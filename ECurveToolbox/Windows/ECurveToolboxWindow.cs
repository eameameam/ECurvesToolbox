using UnityEditor;
using UnityEngine;

namespace Editor.CurveToolbox
{
    public class ECurveToolboxWindow : EditorWindow
    {
        private enum Tab
        {
            AddCurves,
            RemoveCurves,
            RenameCurves,
            MergeClips
        }

        private Tab _currentTab = Tab.AddCurves;

        [MenuItem("Escripts/ECurve Toolbox")]
        public static void ShowWindow()
        {
            GetWindow<ECurveToolboxWindow>("ECurve Toolbox");
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            _currentTab = (Tab)GUILayout.Toolbar((int)_currentTab, new string[] { "Add", "Remove", "Rename", "Merge" });

            switch (_currentTab)
            {
                case Tab.AddCurves:
                    EAddCurvesTab.OnGUI();
                    break;
                case Tab.RemoveCurves:
                    ERemoveCurvesTab.OnGUI();
                    break;
                case Tab.RenameCurves:
                    ERenameCurvesTab.OnGUI();
                    break;
                case Tab.MergeClips:
                    EMergeClipsTab.OnGUI();
                    break;
            }
        }
    }
}