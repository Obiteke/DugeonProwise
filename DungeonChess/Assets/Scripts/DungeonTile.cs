using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonTile : MonoBehaviour
{
    public DungeonTileType.Type Type;
    public Vector3 Position;

    [HideInInspector]
    public bool HasPlayerL, HasPlayerR;

    [HideInInspector]
    public bool OpenDoor, Walkable, HasStone;

    [SerializeField]
    private GameObject[] _buttonTile;


    // Start is called before the first frame update
    void Start()
    {
        switch (Type)
        {
            case DungeonTileType.Type.Default:
                {
                    Walkable = true;
                    break;
                }

            case DungeonTileType.Type.Button:
                {
                    Walkable = true;
                    break;
                }
            case DungeonTileType.Type.Door:
                {
                    Walkable = false;
                    _buttonTile = GameObject.FindGameObjectsWithTag("Button");//.GetComponent<DungeonTile>();
                    
                    break;
                }
        }
    }


    // Update is called once per frame
    void Update()
    {
        switch (Type)
        {
            case DungeonTileType.Type.Default:
                {
                    break;
                }

            case DungeonTileType.Type.Button:
                {
                    if (HasPlayerL || HasPlayerR)
                    {
                        OpenDoor = true;
                    }
                    else
                    {
                        OpenDoor = false;
                    }

                    break;
                }
                case DungeonTileType.Type.Door:
                {
                    if (IsDoorOpen())
                    {
                        Walkable = true;
                        gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    else
                    {
                        Walkable = false;
                        gameObject.transform.GetChild(1).gameObject.SetActive(true);

                    }

                    break;
                }
        }
    }
    public bool IsDoorOpen()
    {
        var buttons = _buttonTile.Where(b => b.gameObject.GetComponent<DungeonTile>().OpenDoor);
        if(buttons != null && buttons.Count() > 0)
            return true;
        else
            return false;
    }
}
