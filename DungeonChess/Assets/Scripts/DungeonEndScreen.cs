using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEndScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject WinScreen, LostScreen;

    public void YouWonScreen()
    {
        WinScreen.gameObject.SetActive(true);
    }
    public void YouLostScreen()
    {
        LostScreen.gameObject.SetActive(true);
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
