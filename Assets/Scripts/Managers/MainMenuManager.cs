using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _continueBtn;


    private void Start()
    {
        DatabaseManager.LoadData();
        if (DatabaseManager.Save != null)
        {
            _continueBtn.interactable = true;
        }
    }
    
    public void OnClickNewGame()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnClickContinueGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickSettings()
    {
        
    }

    public void OnClickQuit() {
        Application.Quit();
    }
}
