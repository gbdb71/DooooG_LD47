using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using Deirin.CustomButton;
using TMPro;

public class AutoLoadLevel : MonoBehaviour
{
	//public List<Scene> levels;
	//public Scene level;
	private const string ScenePattern = @".+\/(.+)\.unity";
	private const string LevelPattern = "Level";

	public GameObject buttonPrefab;

	[Sirenix.OdinInspector.ReadOnly, HideInEditorMode]
	public List<string> levels = new List<string>();


	public string[] Scenes {
		get {
			return EditorBuildSettings.scenes
									  .Where(scene => scene.enabled)
									  .Select(scene => Regex.Match(scene.path, ScenePattern).Groups[1].Value)
									  .ToArray();
		}
	}


	public void Awake()
	{
		//load scene
		if (levels == null || levels.Count == 0)
		{
			string[] tmpLevels = Scenes;
			for (int i = 0; i < tmpLevels.Length; i++)
			{
				if (tmpLevels[i].Contains(LevelPattern))
				{
					levels.Add(tmpLevels[i]);
				}
			}
		}

		foreach(string level in levels)
		{
			GameObject tmp = (GameObject)PrefabUtility.InstantiatePrefab(buttonPrefab);
			tmp.name = "Load \"" + level + "\"";
			tmp.transform.SetParent(this.transform, false);
			tmp.GetComponentInChildren<TextMeshProUGUI>().text = level;
			//Debug.Log(tmp.GetComponentInChildren<TextMeshPro>().text);
			tmp.GetComponentInChildren<CustomButton_Canvas>().OnButtonClick.DynamicCalls += () => SceneManager.LoadScene(level);
		}

	}

}
