using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    GameObject TilePrefab, PlayerLPrefab, PlayerRPrefab, _board;

    [SerializeField]
    float rows, columns = 0;

    [HideInInspector]
    public GameObject playerL, playerR;

    [HideInInspector]
    public List<GameObject> tiles = new List<GameObject>();

    

    private Vector2 _moveInput;
    private bool _isMoving = false;
    public Vector3 Position;

    public bool IsMoving { get => _isMoving; set => _isMoving = value; }

    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();
    }

    private void CreateBoard()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var tile = Instantiate(TilePrefab, new Vector3(i, 0, j), this.transform.rotation, _board.transform);
                tile.GetComponent<DungeonTile>().Position = new Vector3(i, 0, j);
                tiles.Add(tile);

                if (i == 5 && j == 5)
                {
                    tile.GetComponent<DungeonTile>().HasPlayerL = true;
                    playerL = Instantiate(PlayerLPrefab, new Vector3(i, 0, j), this.transform.rotation);
                    playerL.GetComponent<DungeonPlayerLeft>().Position = new Vector3(i, 0, j);
                }

                if (i == 5 && j == 4)
                {
                    tile.GetComponent<DungeonTile>().HasPlayerR = true;
                    playerR = Instantiate(PlayerRPrefab, new Vector3(i, 0, j), this.transform.rotation);
                    playerR.GetComponent<DungeonPlayerRight>().Position = new Vector3(i, 0, j);
                }
            }
        }
    }
}
