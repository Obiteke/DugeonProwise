using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NuitrackSDK;
using NuitrackSDK.Tutorials.FirstProject;

public class DungeonPlayerLeft : MonoBehaviour
{
    public GameObject SkeletonScript = null;
    public UserData.SkeletonData.Joint joint;
    private Vector3 oldJP = Vector3.zero;


    private Vector2 _moveInput;
    private bool _isMoving = false;

    private DungeonManager DMScript;
    private GameObject _player;

    public Vector3 Position;
    private Vector3 _oldPosition;

    public bool IsMoving { get => _isMoving; set => _isMoving = value; }

    private bool _timerEnd = false;
    private float _timerLeft = 0.6f;
    private float _currTimerLeft;

    private Vector3 JShoulderPosition;
    private Vector3 JHandPosition;

    // Start is called before the first frame update
    void Start()
    {
        DMScript = FindObjectOfType<DungeonManager>();
        SkeletonScript = FindObjectOfType<NativeAvatar>().gameObject;
        _player = DMScript.playerL;
        _currTimerLeft = _timerLeft;
    }

    // Update is called once per frame
    void Update()
    {
        _oldPosition = Position;
        if (NuitrackManager.Users != null)
        {
            JHandPosition = NuitrackManager.Users.Current.Skeleton.GetJoint(SkeletonScript.GetComponent<NativeAvatar>().typeJoint[8]).Position;
            JShoulderPosition = NuitrackManager.Users.Current.Skeleton.GetJoint(SkeletonScript.GetComponent<NativeAvatar>().typeJoint[4]).Position;
        }
        
        if (!_timerEnd)
        {
            _currTimerLeft -= Time.deltaTime;
            if (_currTimerLeft < 0)
            {
                _timerEnd = true;
                _currTimerLeft = _timerLeft;
            }
        }
        else
        {
            if(Mathf.Abs( Mathf.Abs(JHandPosition.x - JShoulderPosition.x) - Mathf.Abs(JHandPosition.y - JShoulderPosition.y)) < 0.2)
            {
                return;
            }
            if (Mathf.Abs(JHandPosition.x - JShoulderPosition.x) > Mathf.Abs(JHandPosition.y - JShoulderPosition.y))
            {
                if ((JHandPosition.x - JShoulderPosition.x) < 0)
                {
                    Position.z = Position.z - 1;        //Right
                    Position.z = Mathf.Clamp(Position.z, 0, DMScript.columns - 1);
                    IsMoving = true;
                }
                if ((JHandPosition.x - JShoulderPosition.x) > 0)
                {
                    Position.z = Position.z + 1;        //Left
                    Position.z = Mathf.Clamp(Position.z, 0, DMScript.columns - 1);
                    IsMoving = true;
                }
            }
            else
            {
                if ((JHandPosition.y - JShoulderPosition.y) > 0)
                {
                    Position.x = Position.x + 1;        //Up
                    Position.x = Mathf.Clamp(Position.x, 0, DMScript.rows - 1);
                    IsMoving = true;
                }
                if ((JHandPosition.y - JShoulderPosition.y) < 0)
                {
                    Position.x = Position.x - 1;        //Down
                    Position.x = Mathf.Clamp(Position.x, 0, DMScript.rows - 1);
                    IsMoving = true;
                }
            }
                MovingPlayer();
                _timerEnd = false;
        }
    }

    public void MovementInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        if (context.performed && !IsMoving)
        {
            if (_moveInput.x > 0.8)
            {
                Position.z = Position.z - 1;
                Mathf.Clamp(Position.z, 0, DMScript.columns);
                IsMoving = true;
            }
            if (_moveInput.x < -0.8)
            {
                Position.z = Position.z + 1;
                Mathf.Clamp(Position.z, 0, DMScript.columns);
                IsMoving = true;
            }
            if (_moveInput.y > 0.8)
            {
                Position.x = Position.x + 1;
                Mathf.Clamp(Position.x, 0, DMScript.rows);
                IsMoving = true;
            }
            if (_moveInput.y < -0.8)
            {
                Position.x = Position.x - 1;
                Mathf.Clamp(Position.x, 0, DMScript.rows);
                IsMoving = true;
            }

            MovingPlayer();
        }

        if (context.canceled)
            IsMoving = false;


    }

    private void MovingPlayer()
    {
        foreach (var tile in DMScript.tiles)
        {
            if (tile.GetComponent<DungeonTile>().HasPlayerL)
            {
                tile.GetComponent<DungeonTile>().HasPlayerL = false;
            }
            if (tile.GetComponent<DungeonTile>().Position == Position)
            {
                if (tile.GetComponent<DungeonTile>().HasPlayerR || !tile.GetComponent<DungeonTile>().Walkable)
                {
                    Position = _oldPosition;
                    return;
                }
                if (tile.GetComponent<DungeonTile>().HasStone)
                {
                    DMScript.Stone.GetComponent<DungeonStone>().MoveStone(_oldPosition);
                }
                if (tile.GetComponent<DungeonTile>().HasMonster)
                {
                    Destroy(DMScript.Monster);
                }
                if (tile.GetComponent<DungeonTile>().HasChess)
                {
                    Destroy(DMScript.Chess);
                    FindObjectOfType<DungeonEndScreen>().YouWonScreen();
                }
                tile.GetComponent<DungeonTile>().HasPlayerL = true;
                transform.position = tile.GetComponent<DungeonTile>().transform.position;
            }
        }
    }

}
