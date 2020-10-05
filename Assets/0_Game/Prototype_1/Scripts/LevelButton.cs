using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {
    [Header("Refs")]
    public TextMeshProUGUI tmpText;

    private string text;
    private int sceneIndex;

    public void SetData( string text, int sceneIndex ) {
        this.text = text;
        this.sceneIndex = sceneIndex;

        tmpText.text = text;
    }

    public void Click () {
        SceneManager.LoadScene( sceneIndex );
    }
}