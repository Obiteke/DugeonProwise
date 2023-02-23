using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStone : MonoBehaviour
{
    public Vector3 Position;


    [HideInInspector]
    public List<GameObject> stoneTiles = new List<GameObject>();

    private DungeonManager DMScript;

    public bool isStoneMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        DMScript = FindObjectOfType<DungeonManager>();
        foreach(var tile in DMScript.tiles)
        {
            var tileObject = tile.gameObject.GetComponent<DungeonTile>();

            if(tileObject.Position == Position)
                AddStoneTile(tile, tileObject);
            if (tileObject.Position == new Vector3(Position.x + 1, 0, Position.z))
                AddStoneTile(tile, tileObject);
            if (tileObject.Position == new Vector3(Position.x, 0, Position.z + 1))
                AddStoneTile(tile, tileObject);
            if (tileObject.Position == new Vector3(Position.x + 1, 0, Position.z + 1))
                AddStoneTile(tile, tileObject);
        }
    }

    private void AddStoneTile(GameObject tile, DungeonTile tileObject)
    {
        tileObject.HasStone = true;
        stoneTiles.Add(tile);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void MoveStone(Vector3 playerPosition)
    {
        Debug.Log("plswork");
        //if (isStoneMoving)
        //{
        if((playerPosition.x - Position.x) <= (playerPosition.z - Position.z))
        {
            if (playerPosition.x < Position.x)
                Position.x++; //move up

            else// (playerPosition.x > Position.x)
                Position.x--; //move down
        }
        else
        {
            if (playerPosition.z < Position.z)
                Position.z++; //move left

            else//if (playerPosition.z > Position.z)
                Position.z--; //move right
        }


        foreach (var stoneTile in stoneTiles)
        {
            stoneTile.GetComponent<DungeonTile>().HasStone = false;
            
        }
        stoneTiles.Clear();
        foreach (var tile in DMScript.tiles)
        {
            if( tile.GetComponent<DungeonTile>().Position == Position)
            transform.position = tile.GetComponent<DungeonTile>().transform.position;

            var tileObject = tile.gameObject.GetComponent<DungeonTile>();

            if (tileObject.Position == Position)
                AddStoneTile(tile, tileObject);
            if (tileObject.Position == new Vector3(Position.x + 1, 0, Position.z))
                AddStoneTile(tile, tileObject);
            if (tileObject.Position == new Vector3(Position.x, 0, Position.z + 1))
                AddStoneTile(tile, tileObject);
            if (tileObject.Position == new Vector3(Position.x + 1, 0, Position.z + 1))
                AddStoneTile(tile, tileObject);


        }
        var stoneCount = 0;
        foreach (var stoneTile in stoneTiles)
        {
            
            if( stoneTile.GetComponent<DungeonTile>().Type == DungeonTileType.Type.Empty)
            {
                stoneCount++;
                if(stoneCount == stoneTiles.Count)
                {
                    foreach (var sTile in stoneTiles)
                    {
                        sTile.GetComponent<DungeonTile>().Walkable = true;
                        sTile.GetComponent<DungeonTile>().TilesAppear();
                        sTile.GetComponent<DungeonTile>().HasStone = false;
                    }
                    Destroy(this.gameObject);
                    //this.gameObject.SetActive(false);
                }
            }
        }
    }
}
