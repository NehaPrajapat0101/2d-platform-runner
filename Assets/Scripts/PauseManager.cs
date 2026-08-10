using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public Button crossButton;
    public Button continueButton;
    public Button lobbyButton;
    public Button quitButton;

    public bool isPaused = false;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject blurPanel;

    void Awake()
    {
        crossButton.onClick.AddListener(CrossButton);
        continueButton.onClick.AddListener(ContinueButton);
        lobbyButton.onClick.AddListener(LobbyButton);
        quitButton.onClick.AddListener(QuitButton);
    }

    void CrossButton()
    {
        if(isPaused == false)
        {
            Time.timeScale = 0f;  // this will pause the physics and animations
            FindObjectOfType<AudioManager>().Play(SoundNames.MenuButtonQuit);
            pauseMenu.SetActive(true);
            blurPanel.SetActive(true);
            isPaused = true;
        }
        else if(isPaused == true)
        {
            Time.timeScale = 1f;   // this will allow u to use physics and the animations
            FindObjectOfType<AudioManager>().Play(SoundNames.MenuButtonQuit);
            pauseMenu.SetActive(false);
            blurPanel.SetActive(false);
            isPaused = false;   
        }
    }

    void ContinueButton()
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play(SoundNames.MenuButtonPlay);
        pauseMenu.SetActive(false);
        blurPanel.SetActive(false);
        isPaused = false;
    }

    void LobbyButton()
    {
        Time.timeScale = 1f;

        FindObjectOfType<AudioManager>().Play(SoundNames.LevelSelection);
        FindObjectOfType<AudioManager>().Stop(SoundNames.Background);

        LevelManager.Instance.SetSceneToLoad(0);
        SceneManager.LoadScene("LoadingScreen");
    }

    void QuitButton()
    {
        FindObjectOfType<AudioManager>().Play(SoundNames.MenuButtonQuit);
        Application.Quit();
    }
}
