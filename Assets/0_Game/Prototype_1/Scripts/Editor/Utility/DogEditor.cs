using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Doggo;

public class DogEditor : SerializedMonoBehaviour
{
    [SerializeField, ReadOnly] private Doggo _dogPointer;
    [SerializeField] private Transform _booty;
    [SerializeField, ReadOnly] private Direction _dogDirection;
    [SerializeField, ReadOnly] private int _lenght;


    public Doggo Dog 
    {
        get 
        {
            if (_dogPointer == null)
                _dogPointer = GetComponentInChildren<Doggo>();
            return _dogPointer;
		}
	}

    [ShowInInspector]
    public Direction DogDirection
    {
        get
        {
            return _dogDirection;
        }
        set
        {
            _dogDirection = value;
            Dog.currentDirection = value;
			switch (value)
			{
                case Direction.up:
                    Dog.transform.rotation = Quaternion.Euler(0, 0, 0);
                    _booty.rotation = Quaternion.Euler(0, 0, 0);
                    break;
				case Direction.right:
                    Dog.transform.rotation = Quaternion.Euler(0, 90, 0);
                    _booty.rotation = Quaternion.Euler(0, 90, 0);
                    break;
                case Direction.down:
                    Dog.transform.rotation = Quaternion.Euler(0, 180, 0);
                    _booty.rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case Direction.left:
                    Dog.transform.rotation = Quaternion.Euler(0, 270, 0);
                    _booty.rotation = Quaternion.Euler(0, 270, 0);
                    break;
			}
        }
    }


    [MinValue(4), ShowInInspector]
    public int Lenght 
    {
        get 
        {
            return _lenght;
		}
        set
        {
            _lenght = value;
            Dog.doggoLength = value;
		}
	}

	public void Awake()
	{
        Lenght = _lenght;
        DogDirection = _dogDirection;
    }

}
