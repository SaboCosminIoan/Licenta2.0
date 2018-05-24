using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameUserInterface : NetworkBehaviour {

    public GameObject inventoryUI;
    public GameObject statsUI;

    public void AccesInventory()
    {
        if(!isLocalPlayer)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }      
    }

    public void AccesStats()
    {
        if (!isLocalPlayer)
        {
            statsUI.SetActive(!statsUI.activeSelf);
        }   
    }
}
