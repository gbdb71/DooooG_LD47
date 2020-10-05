using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour {
    private LevelButton[] levelButtons;

    private void Awake () {
        levelButtons = GetComponentsInChildren<LevelButton>();

        int count = SceneManager.sceneCountInBuildSettings;

        for ( int i = 0; i < levelButtons.Length; i++ ) {
            levelButtons[i].SetData( ( i + 1 ).ToString(), i + 1 );
        }
    }
}