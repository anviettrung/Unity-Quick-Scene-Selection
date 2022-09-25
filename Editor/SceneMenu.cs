using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Packages.com.avt.tool.scene.selection.Editor
{
	[InitializeOnLoad]
	public class SceneSwitchRightButton
	{
		static SceneSwitchRightButton()
		{
			ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
		}

		private const string UnknownString = "<-unknown->";

		private static void OnToolbarGUI()
		{
			var count = SceneManager.sceneCountInBuildSettings;
			var scenePaths = new string[count];
			var names = new string[count+1];
			
			var currentSceneId = count;
			var currentSceneName = SceneManager.GetActiveScene().name;

			// Init scene paths and names
			for (var i = 0; i < count; i++)
			{
				scenePaths[i] = SceneUtility.GetScenePathByBuildIndex(i);
				names[i] = System.IO.Path.GetFileNameWithoutExtension(scenePaths[i]);
				
				if (names[i] == currentSceneName)
					currentSceneId = i;
			}
			names[count] = UnknownString;
			
			
			var sceneIndex = EditorGUILayout.Popup(
				currentSceneId, names,GUILayout.Width(100));
			
			if (sceneIndex != currentSceneId && sceneIndex != count)
			{
				if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
				{
					EditorSceneManager.OpenScene(scenePaths[sceneIndex]);
				}
			}
			GUILayout.FlexibleSpace();
		}
	}
}