using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class Doggo : MonoBehaviour {
    [Header("References")]
    //public Transform cam;
    public Transform bodyPrefab;
    [Header("Params")]
    public LayerMask wallMask;
    public float cellSize;
    public float moveSpeed;
    public float rotationTime = .2f;
    public float inputTreshold;

    [ReadOnly]
    [SerializeField] private Direction currentDirection;
    [ReadOnly]
    [SerializeField] private bool moving;
    [ReadOnly]
    [SerializeField] private bool wallForward, wallRight, wallLeft;

    private Vector3 input;
    //private Vector3 camF;
    //private Vector3 camR;

    private void Update () {
        //raycast
        Raycast();

        //input
        ReadInput();

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

        switch ( currentDirection ) {
            case Direction.up:
            switch ( dir ) {
                case Direction.up:
                if ( wallForward )
                    break;
                t = transform.DOBlendableLocalMoveBy( Vector3.forward * cellSize, moveSpeed );
                break;
                case Direction.down:
                //cannot go back
                break;
                case Direction.right:
                if ( wallRight )
                    break;
                t = transform.DOBlendableLocalMoveBy( Vector3.right * cellSize, moveSpeed );
                transform.DOLookAt( transform.position + Vector3.right, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
                case Direction.left:
                if ( wallLeft )
                    break;
                t = transform.DOBlendableLocalMoveBy( -Vector3.right * cellSize, moveSpeed );
                transform.DOLookAt( transform.position - Vector3.right, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
            }
            break;
            case Direction.down:
            switch ( dir ) {
                case Direction.up:
                //cannot go back
                break;
                case Direction.down:
                if ( wallForward )
                    break;
                t = transform.DOBlendableLocalMoveBy( -Vector3.forward * cellSize, moveSpeed );
                break;
                case Direction.right:
                if ( wallLeft )
                    break;
                t = transform.DOBlendableLocalMoveBy( Vector3.right * cellSize, moveSpeed );
                transform.DOLookAt( transform.position + Vector3.right, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
                case Direction.left:
                if ( wallRight )
                    break;
                t = transform.DOBlendableLocalMoveBy( -Vector3.right * cellSize, moveSpeed );
                transform.DOLookAt( transform.position - Vector3.right, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
            }
            break;
            case Direction.right:
            switch ( dir ) {
                case Direction.up:
                if ( wallLeft )
                    break;
                t = transform.DOBlendableLocalMoveBy( Vector3.forward * cellSize, moveSpeed );
                transform.DOLookAt( transform.position + Vector3.forward, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
                case Direction.down:
                if ( wallRight )
                    break;
                t = transform.DOBlendableLocalMoveBy( -Vector3.forward * cellSize, moveSpeed );
                transform.DOLookAt( transform.position - Vector3.forward, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
                case Direction.right:
                if ( wallForward )
                    break;
                t = transform.DOBlendableLocalMoveBy( Vector3.right * cellSize, moveSpeed );
                break;
                case Direction.left:
                //cannot go back
                break;
            }
            break;
            case Direction.left:
            switch ( dir ) {
                case Direction.up:
                if ( wallRight )
                    break;
                t = transform.DOBlendableLocalMoveBy( Vector3.forward * cellSize, moveSpeed );
                transform.DOLookAt( transform.position + Vector3.forward, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
                case Direction.down:
                if ( wallLeft )
                    break;
                t = transform.DOBlendableLocalMoveBy( -Vector3.forward * cellSize, moveSpeed );
                transform.DOLookAt( transform.position - Vector3.forward, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
                case Direction.right:
                //cannot go back
                break;
                case Direction.left:
                if ( wallForward )
                    break;
                t = transform.DOBlendableLocalMoveBy( -Vector3.right * cellSize, moveSpeed );
                break;
            }
            break;
        }

        if ( t == null ) {
            moving = false;
        }
        else {
            Instantiate( bodyPrefab, transform.position, Quaternion.identity );

            t.SetSpeedBased( true );
            t.SetEase( Ease.OutBack );
            t.onComplete += MoveEndHandler;
        }
    }

    private void ReadInput () {
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
    }

    private void Raycast () {
        //forward
        Ray r = new Ray(transform.position, transform.forward);
        wallForward = Physics.Raycast( r, cellSize, wallMask );

        //right
        r = new Ray( transform.position, transform.right );
        wallRight = Physics.Raycast( r, cellSize, wallMask );

        //left
        r = new Ray( transform.position, -transform.right );
        wallLeft = Physics.Raycast( r, cellSize, wallMask );
    }

    private void MoveEndHandler () => moving = false;

    public enum Direction { up, down, right, left }

    //private void LateUpdate () {
    //    //Vector3 velocity = ( camF * input.y + camR * input.x ) * speed * Time.deltaTime;
    //    //transform.position += velocity;

    //    //transform.LookAt( transform.position + velocity );
    //}
}