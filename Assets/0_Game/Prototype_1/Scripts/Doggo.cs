using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using System.Collections.Generic;
using UltEvents;
using PathCreation;

public class Doggo : MonoBehaviour {
    [Header("References")]
    public Transform booty;
    public Transform bodyPrefab;
    public ProceduralDogBody proceduralDogBody;
    public Material dogBodyMaterial;
    [Header("Params")]
    public LayerMask wallMask;
    [Min(2)]
    public int doggoLength;
    public float cellSize;
    public float moveSpeed;
    public float rotationTime = .2f;
    public float inputTreshold;
    public Ease movementEase;
    public Ease rotationEase;
    [Header("Events")]
    public UltEvent OnWin;
    public UltEvent<VertexPath> OnPathUpdate;

    [Space, Space]
    public Direction currentDirection;
    [ReadOnly]
    [SerializeField] private bool moving;
    [ReadOnly]
    [SerializeField] private bool wallForward, wallRight, wallLeft;
    [ReadOnly]
    [SerializeField] private bool bootyForward, bootyRight, bootyLeft;

    private GameObject pathHolder;
    private Vector3 input;
    private List<Transform> bodyParts = new List<Transform>();
    [SerializeField]
    [ReadOnly]
    private int bootyFollowMoves = 0;
    private VertexPath vertexPath;
    private BezierPath bezierPath;
    private Vector3[] points;

    private void Start () {
        CreatePathHolder();

        CreateBody();

        Raycast();
    }

    private void Update () {
        ReadInput();

        if ( moving )
            UpdatePath();
    }

    private void CreatePathHolder () {
        pathHolder = new GameObject( "Path Holder" );
        pathHolder.transform.SetPositionAndRotation( Vector3.zero, Quaternion.identity );

        proceduralDogBody.meshFilter = pathHolder.AddComponent<MeshFilter>();
        pathHolder.AddComponent<MeshRenderer>().material = dogBodyMaterial;
    }

    private void UpdatePath () {
        points = new Vector3[doggoLength];

        bezierPath = new BezierPath( bodyParts, false, PathSpace.xyz );
        vertexPath = new VertexPath( bezierPath, pathHolder.transform );

        OnPathUpdate.Invoke( vertexPath );
    }

    private void Move ( Direction dir ) {
        moving = true;
        Tween mt = null;
        Tween rt = null;

        switch ( currentDirection ) {
            case Direction.up:
            switch ( dir ) {
                case Direction.up:
                if ( wallForward )
                    break;
                mt = transform.DOBlendableLocalMoveBy( Vector3.forward * cellSize, moveSpeed );
                break;
                case Direction.down:
                //cannot go back
                break;
                case Direction.right:
                if ( wallRight )
                    break;
                mt = transform.DOBlendableLocalMoveBy( Vector3.right * cellSize, moveSpeed );
                rt = transform.DOLookAt( transform.position + Vector3.right, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
                case Direction.left:
                if ( wallLeft )
                    break;
                mt = transform.DOBlendableLocalMoveBy( -Vector3.right * cellSize, moveSpeed );
                rt = transform.DOLookAt( transform.position - Vector3.right, rotationTime, AxisConstraint.Y );
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
                mt = transform.DOBlendableLocalMoveBy( -Vector3.forward * cellSize, moveSpeed );
                break;
                case Direction.right:
                if ( wallLeft )
                    break;
                mt = transform.DOBlendableLocalMoveBy( Vector3.right * cellSize, moveSpeed );
                rt = transform.DOLookAt( transform.position + Vector3.right, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
                case Direction.left:
                if ( wallRight )
                    break;
                mt = transform.DOBlendableLocalMoveBy( -Vector3.right * cellSize, moveSpeed );
                rt = transform.DOLookAt( transform.position - Vector3.right, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
            }
            break;
            case Direction.right:
            switch ( dir ) {
                case Direction.up:
                if ( wallLeft )
                    break;
                mt = transform.DOBlendableLocalMoveBy( Vector3.forward * cellSize, moveSpeed );
                rt = transform.DOLookAt( transform.position + Vector3.forward, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
                case Direction.down:
                if ( wallRight )
                    break;
                mt = transform.DOBlendableLocalMoveBy( -Vector3.forward * cellSize, moveSpeed );
                rt = transform.DOLookAt( transform.position - Vector3.forward, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
                case Direction.right:
                if ( wallForward )
                    break;
                mt = transform.DOBlendableLocalMoveBy( Vector3.right * cellSize, moveSpeed );
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
                mt = transform.DOBlendableLocalMoveBy( Vector3.forward * cellSize, moveSpeed );
                rt = transform.DOLookAt( transform.position + Vector3.forward, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
                case Direction.down:
                if ( wallLeft )
                    break;
                mt = transform.DOBlendableLocalMoveBy( -Vector3.forward * cellSize, moveSpeed );
                rt = transform.DOLookAt( transform.position - Vector3.forward, rotationTime, AxisConstraint.Y );
                currentDirection = dir;
                break;
                case Direction.right:
                //cannot go back
                break;
                case Direction.left:
                if ( wallForward )
                    break;
                mt = transform.DOBlendableLocalMoveBy( -Vector3.right * cellSize, moveSpeed );
                break;
            }
            break;
        }

        if ( mt == null ) {
            moving = false;
        }
        else {
            rt.SetEase( rotationEase );

            mt.SetSpeedBased( true );
            mt.SetEase( movementEase );
            mt.onComplete += MoveEndHandler;

            UpdateBody();
        }
    }

    private void UpdateBody () {
        Transform currentBP = bodyParts[1];
        Transform previousBP;

        currentBP.DOMove( transform.position, moveSpeed ).SetSpeedBased( true ).SetEase( movementEase );
        currentBP.DORotateQuaternion( transform.rotation, rotationTime ).SetEase( rotationEase );

        for ( int i = 2; i < doggoLength; i++ ) {
            currentBP = bodyParts[i];
            previousBP = bodyParts[i - 1];

            currentBP.DOMove( previousBP.position, moveSpeed ).SetSpeedBased( true ).SetEase( movementEase );
            currentBP.DORotateQuaternion( previousBP.rotation, rotationTime ).SetEase( rotationEase );
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
        //reset wall status
        wallForward = wallRight = wallLeft = false;

        #region Booty Raycasting
        //raycast for booty
        Ray fr = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if ( Physics.Raycast( fr, out hit, 200f ) ) {
            if ( hit.collider.transform == booty ) {
                bootyForward = true;
                BootySeenSequence();
                return;
            }
        }
        bootyForward = false;
        Ray rr = new Ray( transform.position, transform.right );
        if ( Physics.Raycast( rr, out hit, 200f ) ) {
            if ( hit.collider.transform == booty ) {
                bootyRight = true;
                BootySeenSequence();
                return;
            }
        }
        bootyRight = false;
        Ray lr = new Ray( transform.position, -transform.right );
        if ( Physics.Raycast( lr, out hit, 200f ) ) {
            if ( hit.collider.transform == booty ) {
                bootyLeft = true;
                BootySeenSequence();
                return;
            }
        }
        bootyLeft = false;
        #endregion
        //not moving anymore
        moving = false;
        bootyFollowMoves = 0;

        //forward wall
        wallForward = Physics.Raycast( fr, cellSize, wallMask );

        //right wall
        wallRight = Physics.Raycast( rr, cellSize, wallMask );

        //left wall
        wallLeft = Physics.Raycast( lr, cellSize, wallMask );
    }

    private void MoveEndHandler () {
        Raycast();
    }

    private void BootySeenSequence () {
        if ( bootyForward ) {
            UpdateBootyFollowMoves();
            Move( currentDirection );
        }
        else if ( bootyRight ) {
            UpdateBootyFollowMoves();
            switch ( currentDirection ) {
                case Direction.up:
                Move( Direction.right );
                break;
                case Direction.down:
                Move( Direction.left );
                break;
                case Direction.right:
                Move( Direction.down );
                break;
                case Direction.left:
                Move( Direction.up );
                break;
            }
        }
        else if ( bootyLeft ) {
            UpdateBootyFollowMoves();
            switch ( currentDirection ) {
                case Direction.up:
                Move( Direction.left );
                break;
                case Direction.down:
                Move( Direction.right );
                break;
                case Direction.right:
                Move( Direction.up );
                break;
                case Direction.left:
                Move( Direction.down );
                break;
            }
        }
    }

    private void UpdateBootyFollowMoves () {
        bootyFollowMoves++;
        if ( bootyFollowMoves >= doggoLength + 1 )
            OnWin.Invoke();
    }

    private void CreateBody () {
        bodyParts.Add( transform );
        for ( int i = 0; i < doggoLength - 2; i++ ) {
            bodyParts.Add( Instantiate( bodyPrefab, transform.position, transform.rotation ) );
        }
        bodyParts.Add( booty );
    }

    public enum Direction {
        NONE = 0,
        up = 1,
        right = 2,
        down = 3,
        left = 4
    }
}