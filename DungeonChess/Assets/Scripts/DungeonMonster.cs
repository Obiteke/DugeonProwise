using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMonster : MonoBehaviour
{
    public Vector3 Position;


    [HideInInspector]
    public List<GameObject> monsterTiles = new List<GameObject>();

    private DungeonManager DMScript;
    void Start()
    {
        DMScript = FindObjectOfType<DungeonManager>();
        foreach (var tile in DMScript.tiles)
        {
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
    }

    private void AddStoneTile(GameObject tile, DungeonTile tileObject)
    {
        tileObject.HasMonster = true;
        monsterTiles.Add(tile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
