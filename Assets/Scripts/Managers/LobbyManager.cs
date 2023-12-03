using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public void OnClickSelectClass(Class playerClass)
    {
        Save save = new Save();
        DatabaseManager.SetSave(save);
        Player player = new Player(playerClass);
        DatabaseManager.SaveNewPartyMember(player);
        
        DatabaseManager.SaveData();
        SceneManager.LoadScene("Game");
    }
}
