using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Doggo;

public class DogEditor : SerializedMonoBehaviour
{
    private Doggo _dogPointer;
    public Doggo dog 
    {
        get 
        {
            if (_dogPointer == null)
                _dogPointer = GetComponentInChildren<Doggo>();
            return _dogPointer;
		}
	}

    [ShowInInspector]
    public Direction dogDirection
    {
        get
        {
            return dog.currentDirection;
        }
        set
        {
            dog.currentDirection = value;
			switch (value)
			{
                case Direction.up:
                    dog.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
				case Direction.right:
                    dog.transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;
                case Direction.down:
                    dog.transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case Direction.left:
                    dog.transform.rotation = Quaternion.Euler(0, 270, 0);
                    break;
			}
		}
	}


    [MinValue(4), ShowInInspector]
    public int Lenght 
    {
        get 
        {
            return dog.doggoLength;
		}
        set
        {
            dog.doggoLength = value;
		}
	}


}
