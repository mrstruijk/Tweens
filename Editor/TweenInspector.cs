using UnityEngine;
using UnityEditor;
using Tweens.Core;

namespace Tweens.Editor {
  internal class TweenInspector : EditorWindow {
    Vector2 scrollPosition;

    [MenuItem("Window/Analysis/Tween Inspector", false, 1000)]
    internal static void ShowWindow() {
      GetWindow<TweenInspector>("Tween Inspector");
    }

    void OnGUI() {
      scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
      DrawRow("Target", "Layer", "Time", "Duration", "Delay", "Loops", "Direction");
      if (!Application.isPlaying) {
        EditorGUILayout.HelpBox("You can only inspect tweens while the app is running.", MessageType.Info);
        EditorGUILayout.EndScrollView();
        return;
      }
      for (var i = TweenEngine.active.Count - 1; i >= 0; i--) {
        var tweenInstance = TweenEngine.active[i];
        DrawRow(
          tweenInstance.target.name,
          tweenInstance.layer != null ? $"{tweenInstance.layer}" : "N/A",
          $"{tweenInstance.time:0.00}",
          $"{tweenInstance.duration:0.00}",
          tweenInstance.delay != null ? $"{tweenInstance.delay:0.00}" : "N/A",
          tweenInstance.loops != null ? $"{tweenInstance.loops}" : "N/A",
          tweenInstance.isForwards ? "Forwards" : "Backwards"
        );
      }
      EditorGUILayout.EndScrollView();
    }

    void Update() {
      if (!EditorApplication.isPlaying || EditorApplication.isPaused) {
        return;
      }
      Repaint();
    }

    void DrawRow(params string[] labels) {
      EditorGUILayout.BeginHorizontal();
      for (int i = 0; i < labels.Length; i++) {
        var label = labels[i];
        EditorGUILayout.LabelField(label, i == 0 ? EditorStyles.boldLabel : EditorStyles.label, GUILayout.Width(i == 0 ? 100 : 50));
      }
      EditorGUILayout.EndHorizontal();
    }
  }
}