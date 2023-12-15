using System;
using System.Collections;
using System.Collections.Generic;
using _PacMan;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private new SceneName name;

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        GameplayScene(name);
    }

    private void GameplayScene(SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.MainMenu:
                _button.onClick.AddListener(()=> SceneManager.LoadScene("MainMenu"));
                cursorON();
                break;
            case SceneName.Gameplay:
                _button.onClick.AddListener(()=> SceneManager.LoadScene("Area 1"));
                break;
            case SceneName.Win:
                _button.onClick.AddListener(()=> SceneManager.LoadScene("Win Scene"));
                cursorON();
                break;
            case SceneName.Lose:
                _button.onClick.AddListener(()=> SceneManager.LoadScene("Lose Scene"));
                cursorON();
                break;
            case SceneName.Exit:
                _button.onClick.AddListener(()=> Application.Quit());
                break;
            default:
                _button.onClick.AddListener(()=> SceneManager.LoadScene("MainMenu"));
                break;
        }
    }

    private void cursorON()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void LoseScene()
    {
        SceneManager.LoadScene("Lose Scene");
    }

    public void WinScene()
    {
        SceneManager.LoadScene("Win Scene");
    }
}
