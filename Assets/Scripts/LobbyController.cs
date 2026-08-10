using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public Button crossButton;
    [SerializeField] private GameObject levelOptions;

    void Awake()
    {
        playButton.onClick.AddListener(PlayButton);
        quitButton.onClick.AddListener(QuitGame);
        crossButton.onClick.AddListener(HomePage);
    }
    
    void Start()
    {
        FindObjectOfType<AudioManager>().Play(SoundNames.HomePage);
    }

    void PlayButton()
    {
        FindObjectOfType<AudioManager>().Play(SoundNames.MenuButtonPlay);
        LevelSelection();
    }

    internal void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play(SoundNames.MenuButtonQuit);
        Application.Quit();
    }

    internal void HomePage()
    {
        FindObjectOfType<AudioManager>().PlayOnce(SoundNames.MenuButtonQuit);
        FindObjectOfType<AudioManager>().Play(SoundNames.HomePage);
        FindObjectOfType<AudioManager>().Stop(SoundNames.LevelSelectionBackground);
        levelOptions.SetActive(false);
    }

    void LevelSelection()
    {
        FindObjectOfType<AudioManager>().Stop(SoundNames.HomePage);
        FindObjectOfType<AudioManager>().Play(SoundNames.LevelSelectionBackground);
        levelOptions.SetActive(true);
    }
}
