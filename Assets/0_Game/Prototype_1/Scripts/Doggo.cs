using UnityEngine;
using DG.Tweening;

public class Doggo : MonoBehaviour {
    [Header("References")]
    //public Transform cam;
    public Transform booty;
    [Header("Params")]
    public float cellSize;
    public float moveSpeed;
    public float rotationTime = .2f;
    public float inputTreshold;

    private Vector3 input;
    private bool moving;
    //private Vector3 camF;
    //private Vector3 camR;

    private void Update () {
        if ( moving )
            return;

        input = new Vector2( Input.GetAxisRaw( "Horizontal" ), Input.GetAxisRaw( "Vertical" ) ).normalized;

        //go right
        if ( Mathf.Abs( input.x ) > inputTreshold ) {
            if ( Mathf.Sign( input.x ) > 0 )
                Move( Direction.right );
            else
                Move( Direction.left );
        }
        else if ( Mathf.Abs( input.y ) > inputTreshold ) {
            if ( Mathf.Sign( input.y ) > 0 )
                Move( Direction.up );
            else
                Move( Direction.down );
        }

        //camF = cam.forward;
        //camR = cam.right;
        //camF.y = 0;
        //camR.y = 0;
        //camF.Normalize();
        //camR.Normalize();
    }

    private void Move ( Direction dir ) {
        moving = true;
        Tween t = null;

        switch ( dir ) {
            case Direction.up:
            t = transform.DOBlendableLocalMoveBy( Vector3.forward * cellSize, moveSpeed );
            transform.DOLookAt( transform.position + Vector3.forward, rotationTime, AxisConstraint.Y );
            break;                                                                               
            case Direction.down:                                                                 
            t = transform.DOBlendableLocalMoveBy( -Vector3.forward * cellSize, moveSpeed );        
            transform.DOLookAt( transform.position - Vector3.forward, rotationTime, AxisConstraint.Y );
            break;
            case Direction.right:
            t = transform.DOBlendableLocalMoveBy( Vector3.right * cellSize, moveSpeed );
            transform.DOLookAt( transform.position + Vector3.right, rotationTime, AxisConstraint.Y );
            break;
            case Direction.left:
            t = transform.DOBlendableLocalMoveBy( -Vector3.right * cellSize, moveSpeed );
            transform.DOLookAt( transform.position - Vector3.right, rotationTime, AxisConstraint.Y );
            break;
        }

        t.SetSpeedBased( true );
        t.SetEase( Ease.OutBack );
        t.onComplete += MoveEndHandler;
    }

    private void MoveEndHandler () => moving = false;

    enum Direction { up, down, right, left }

    //private void LateUpdate () {
    //    //Vector3 velocity = ( camF * input.y + camR * input.x ) * speed * Time.deltaTime;
    //    //transform.position += velocity;

    //    //transform.LookAt( transform.position + velocity );
    //}
}