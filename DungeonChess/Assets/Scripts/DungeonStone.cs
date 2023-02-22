using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStone : MonoBehaviour
{
    public Vector3 Position;
    private Vector3 _oldPosition;


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
            if (playerPosition.x < Position.x)
                Position.x++; //move up

            if (playerPosition.x > Position.x)
                Position.x--; //move down

            if (playerPosition.z < Position.z)
                Position.z++; //move left

            if (playerPosition.z > Position.z)
                Position.z--; //move right


        foreach (var stoneTile in stoneTiles)
        {
            stoneTile.GetComponent<DungeonTile>().HasStone = false;
        }
        foreach (var tile in DMScript.tiles)
        {
            if( tile.GetComponent<DungeonTile>().Position == Position)
            transform.position = tile.GetComponent<DungeonTile>().transform.position;
        }

    }
}
