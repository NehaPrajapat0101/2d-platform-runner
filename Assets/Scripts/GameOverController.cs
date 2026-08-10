using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Button restartButton;
    public Button lobbyButton;

    void Start()
    {
        restartButton.onClick.AddListener(LevelReload);
        lobbyButton.onClick.AddListener(ShowLobby);
    }

    internal void GameOverUIActivate()
    {
        FindObjectOfType<AudioManager>().Play(SoundNames.LevelOverUI);
        FindObjectOfType<AudioManager>().Stop(SoundNames.Background);
        gameObject.SetActive(true);
    }

    internal void LevelReload()
    {

        Debug.Log("Reload Level fxn");
        FindObjectOfType<AudioManager>().Stop(SoundNames.LevelOverUI);
        FindObjectOfType<AudioManager>().Play(SoundNames.Background);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);

    }

    void ShowLobby()
    {
        SceneManager.LoadScene(0);
    }
}
