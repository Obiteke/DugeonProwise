using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<DungeonPlayerLeft>() != null)
            FindObjectOfType<DungeonEndScreen>().YouLostScreen();

        Destroy(gameObject);
    }
}
