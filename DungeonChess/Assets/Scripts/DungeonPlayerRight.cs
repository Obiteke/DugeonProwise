using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DungeonPlayerRight : MonoBehaviour
{

    private Vector2 _moveInput;
    private bool _isMoving = false;

    private DungeonManager DMScript;
    private GameObject _player;

    public Vector3 Position;

    public bool IsMoving { get => _isMoving; set => _isMoving = value; }

    // Start is called before the first frame update
    void Start()
    {
        DMScript = FindObjectOfType<DungeonManager>();
        _player = DMScript.playerR;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovementInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        if (context.performed && !IsMoving)
        {
            if (_moveInput.x > 0.8)
            {
                Position.z = Position.z - 1;
                IsMoving = true;
            }
            if (_moveInput.x < -0.8)
            {
                Position.z = Position.z + 1;
                IsMoving = true;
            }
            if (_moveInput.y > 0.8)
            {
                Position.x = Position.x + 1;
                IsMoving = true;
            }
            if (_moveInput.y < -0.8)
            {
                Position.x = Position.x - 1;
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
            if (tile.GetComponent<DungeonTile>().HasPlayerR)
            {
                tile.GetComponent<DungeonTile>().HasPlayerR = false;
            }
            if (tile.GetComponent<DungeonTile>().Position == Position)
            {
                tile.GetComponent<DungeonTile>().HasPlayerR = true;
                transform.position = tile.GetComponent<DungeonTile>().transform.position;
            }
        }
    }

}
