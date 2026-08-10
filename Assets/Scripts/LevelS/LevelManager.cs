using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get { return instance; }
    }    // to acess functions in other files but in a controlled way

    private LevelStatus levelStatus;
    public LevelStatus LevelStatus
    {
        get { return levelStatus; }
        set { levelStatus = value; }
    }

    public int sceneToLoad
    {
        get; private set;
    }


    void Awake()
    {
        if(instance  == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        instance.SetLevelStatus("L1" , LevelStatus.Unlocked);
    }

    internal LevelStatus GetLevelStatus(string level)
    {
        levelStatus = (LevelStatus) PlayerPrefs.GetInt(level, 0);  // explicit conversion to levelstatus
        return levelStatus;
    }

    void SetLevelStatus(string level, LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt(level, (int)levelStatus);
    }

    public void LoadLevel(int sceneName)
    {
        sceneToLoad = sceneName;
        SceneManager.LoadScene("LoadingScreen");
        FindObjectOfType<AudioManager>().Play(SoundNames.Background);
    }

    public void SetSceneToLoad(int sceneName)
    {
        sceneToLoad = sceneName;
    }
}
