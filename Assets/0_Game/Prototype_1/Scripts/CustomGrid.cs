using UnityEngine;

public class CustomGrid : MonoBehaviour {
    [Header("Params")]
    public Vector2Int size;
    public Vector2Int cellSize;
    public Vector2Int spacing;
    [Header("Params")]
    public Transform cellPrefab;

    public void Generate () {
        DestroyChildren();

        for ( int x = 0; x < size.x; x++ ) {
            for ( int y = 0; y < size.y; y++ ) {

            }
        }
    }

    private void DestroyChildren () {
        int count = transform.childCount;

        if ( Application.isPlaying ) {
            for ( int i = 0; i < count; i++ ) {
                Destroy( transform.GetChild( i ).gameObject );
            }
        }
        else {
            for ( int i = 0; i < count; i++ ) {
                DestroyImmediate( transform.GetChild( 0 ).gameObject);
            }
        }
    }
}