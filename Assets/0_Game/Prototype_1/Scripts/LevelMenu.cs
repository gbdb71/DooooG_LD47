using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour {
    public LevelButton levelButtonPrefab;
    public Transform buttonContainer;

    private void Awake () {
        int count = SceneManager.sceneCountInBuildSettings;

        for ( int i = 1; i < count; i++ ) {
            Instantiate( levelButtonPrefab, buttonContainer ).SetData( "Level " + i, i );
        }
    }
}