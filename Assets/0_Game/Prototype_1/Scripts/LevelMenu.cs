using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelMenu : MonoBehaviour {
    [Header("Refs")]
    public LevelButton levelButtonPrefab;
    public Transform levelContainer;
    [Header("Params")]
    public float perButtonDelay = .2f;
    public float scaleUpDuration = .5f;
    public Ease scaleUpEase;

    private LevelButton[] levelButtons;

    private void Awake () {
        int count = SceneManager.sceneCountInBuildSettings;
        levelButtons = new LevelButton[count - 1];

        for ( int i = 1; i < count; i++ ) {
            LevelButton lb = Instantiate (levelButtonPrefab, levelContainer);
            lb.SetData( i.ToString(), i );
            levelButtons[i - 1] = lb;
        }
    }

    public void ShowButtons () {
        float delay = 0;
        foreach ( var item in levelButtons ) {
            item.transform.DOScale( 1, scaleUpDuration ).SetEase( scaleUpEase ).SetDelay(delay).Play();
            delay += perButtonDelay;
        }
    }
}