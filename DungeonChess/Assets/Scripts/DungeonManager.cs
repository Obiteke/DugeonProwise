using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    GameObject TilePrefab, TileButtonPrefab, TileDoorPrefab, TileWallPrefab, TileEmptyPrefab,
               PlayerLPrefab, PlayerRPrefab, StonePrefab, ShooterPrefab, MonsterPrefab, ChessPrefab, _board;

    //[SerializeField]
    public float rows, columns = 0;

    [HideInInspector]
    public GameObject playerL, playerR ,Stone, Monster, Chess;

    [HideInInspector]
    public List<GameObject> tiles = new List<GameObject>();




    private Vector2 _moveInput;
    private bool _isMoving = false;
    //public Vector3 Position;

    public bool IsMoving { get => _isMoving; set => _isMoving = value; }

    // Start is called before the first frame update
    private void Awake()
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


                //if (i == 1 && j == 9)
                if (i == 1 && j == 2)
                {
                    tile.GetComponent<DungeonTile>().HasPlayerL = true;
                    playerL = Instantiate(PlayerLPrefab, new Vector3(i, 0, j), this.transform.rotation);
                    playerL.GetComponent<DungeonPlayerLeft>().Position = new Vector3(i, 0, j);
                }

                //if (i == 1 && j == 8)
                if (i == 1 && j == 1)
                {
                    tile.GetComponent<DungeonTile>().HasPlayerR = true;
                    playerR = Instantiate(PlayerRPrefab, new Vector3(i, 0, j), this.transform.rotation);
                    playerR.GetComponent<DungeonPlayerRight>().Position = new Vector3(i, 0, j);
                }
                if (i == 2 && j == 8)
                {
                    tile.GetComponent<DungeonTile>().HasStone = true;
                    Stone = Instantiate(StonePrefab, new Vector3(i, 0, j), this.transform.rotation);
                    Stone.GetComponent<DungeonStone>().Position = new Vector3(i, 0, j);

                }
                
                if(i == 4)
                {
                    tiles.Remove(tile);
                    Destroy(tile);
                    if (j != 8 && j!= 9)
                    {
                        tile = Instantiate(TileWallPrefab, new Vector3(i, 0, j), this.transform.rotation, _board.transform);
                        tile.GetComponent<DungeonTile>().Position = new Vector3(i, 0, j);
                        tiles.Add(tile);
                    }
                    else
                    {
                        tile = Instantiate(TileEmptyPrefab, new Vector3(i, 0, j), this.transform.rotation, _board.transform);
                        tile.GetComponent<DungeonTile>().Position = new Vector3(i, 0, j);
                        tiles.Add(tile);
                    }
                }

                if (i == 5)
                {
                    if(j == 8 || j == 9)
                    {
                        tiles.Remove(tile);
                        Destroy(tile);
                        tile = Instantiate(TileEmptyPrefab, new Vector3(i, 0, j), this.transform.rotation, _board.transform);
                        tile.GetComponent<DungeonTile>().Position = new Vector3(i, 0, j);
                        tiles.Add(tile);
                    }
                }

                if (j == 4)
                {
                    if (i != 4 && i != 5)
                    {
                        tiles.Remove(tile);
                        Destroy(tile);
                        tile = Instantiate(TileWallPrefab, new Vector3(i, 0, j), this.transform.rotation, _board.transform);
                        tile.GetComponent<DungeonTile>().Position = new Vector3(i, 0, j);
                        tiles.Add(tile);
                    }
                }
                if(j == 5 && i == 9)
                {
                    Instantiate(ShooterPrefab, new Vector3(i, 1, j), this.transform.rotation);
                }
                if (i == 0 && j == 0)
                {
                    tiles.Remove(tile);
                    Destroy(tile);
                    tile = Instantiate(TileButtonPrefab, new Vector3(i, 0, j), this.transform.rotation, _board.transform);
                    tile.GetComponent<DungeonTile>().Position = new Vector3(i, 0, j);
                    tiles.Add(tile);
                }
                if (i == 0 && j == 5)
                {
                    tiles.Remove(tile);
                    Destroy(tile);
                    tile = Instantiate(TileButtonPrefab, new Vector3(i, 0, j), this.transform.rotation, _board.transform);
                    tile.GetComponent<DungeonTile>().Position = new Vector3(i, 0, j);
                    tiles.Add(tile);
                }

                if (i == 3 && j == 4)
                {
                    tiles.Remove(tile);
                    Destroy(tile);
                    var tileDoor = Instantiate(TileDoorPrefab, new Vector3(i, 0, j), this.transform.rotation, _board.transform);
                    tileDoor.GetComponent<DungeonTile>().Position = new Vector3(i, 0, j);
                    tiles.Add(tileDoor);
                }
                
                if (i == 7 && j == 1)
                {
                    tile.GetComponent<DungeonTile>().HasMonster = true;
                    Monster = Instantiate(MonsterPrefab, new Vector3(i, 0, j), this.transform.rotation);
                    Monster.GetComponent<DungeonMonster>().Position = new Vector3(i, 0, j);
                }

                if (i == 9 && j == 0)
                {
                    tile.GetComponent<DungeonTile>().HasChess = true;
                    Chess = Instantiate(ChessPrefab, new Vector3(i, 0, j), this.transform.rotation);
                    Chess.GetComponent<DungeonChest>().Position = new Vector3(i, 0, j);
                }
            }
        }
    }
}
