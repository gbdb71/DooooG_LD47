﻿using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using System.Collections.Generic;

public class Doggo : MonoBehaviour {
    [Header("References")]
    public Transform booty;
    public Transform bodyPrefab;
    [Header("Params")]
    public LayerMask wallMask;
    public int doggoLength;
    public float cellSize;
    public float moveSpeed;
    public float rotationTime = .2f;
    public float inputTreshold;

    [SerializeField] private Direction currentDirection;
    [ReadOnly]
    [SerializeField] private bool moving;
    [ReadOnly]
    [SerializeField] private bool wallForward, wallRight, wallLeft;
    [ReadOnly]
    [SerializeField] private bool bootySeen;

    private Vector3 input;
    private List<Transform> bodyParts = new List<Transform>();

    private void Start () {
        CreateBody();

        Raycast();
    }

    private void Update () => ReadInput();

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
            t.SetSpeedBased( true );
            t.SetEase( Ease.OutBack );
            t.onComplete += MoveEndHandler;

            UpdateBody();
        }
    }

    private void UpdateBody () {
        Transform currentBP = bodyParts[0];
        Transform previousBP;

        currentBP.DOMove( transform.position, moveSpeed ).SetSpeedBased( true ).SetEase(Ease.OutBack);
        currentBP.DORotateQuaternion( transform.rotation, rotationTime );

        for ( int i = 1; i < doggoLength; i++ ) {
            currentBP = bodyParts[i];
            previousBP = bodyParts[i - 1];

            currentBP.DOMove( previousBP.position, moveSpeed ).SetSpeedBased( true ).SetEase( Ease.OutBack );
            currentBP.DORotateQuaternion( previousBP.rotation, rotationTime );
        }

        booty.DOMove( currentBP.position, moveSpeed ).SetSpeedBased( true ).SetEase( Ease.OutBack );
        booty.DORotateQuaternion( currentBP.rotation, rotationTime );
    }

    private void ReadInput () {
        if ( moving || bootySeen )
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
        //for booty
        Ray fr = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if ( Physics.Raycast( fr, out hit, 200f ) ) {
            if ( hit.collider.transform == booty ) {
                BootySeenSequence();
                return;
            }
        }
        Ray rr = new Ray( transform.position, transform.right );
        if ( Physics.Raycast( rr, out hit, 200f ) ) {
            if ( hit.collider.transform == booty ) {
                BootySeenSequence();
                return;
            }
        }
        Ray lr = new Ray( transform.position, -transform.right );
        if ( Physics.Raycast( lr, out hit, 200f ) ) {
            if ( hit.collider.transform == booty ) {
                BootySeenSequence();
                return;
            }
        }

        //forward wall
        wallForward = Physics.Raycast( fr, cellSize, wallMask );

        //right wall
        wallRight = Physics.Raycast( rr, cellSize, wallMask );

        //left wall
        wallLeft = Physics.Raycast( lr, cellSize, wallMask );
    }

    private void MoveEndHandler () {
        moving = false;
        Raycast();
    }

    private void BootySeenSequence () {
        bootySeen = true;

        //each body part goes to the next body part position
    }

    private void CreateBody () {
        for ( int i = 0; i < doggoLength; i++ ) {
            bodyParts.Add( Instantiate( bodyPrefab, transform.position, transform.rotation ) );
        }
    }

    public enum Direction { up, down, right, left }
}