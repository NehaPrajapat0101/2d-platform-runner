using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class LevelsLoad : MonoBehaviour
{
    public Button levelButton;
    TMP_Text levelName;
    [SerializeField] private LevelStatus levelStatus;

    void Awake()
    {
        levelButton = gameObject.GetComponent<Button>();
        levelName = levelButton.GetComponentInChildren<TMP_Text>();
        levelButton.onClick.AddListener(OnClick);
    }

    void Start()
    {
        CheckStatus();
    }

    void OnClick()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(levelName.text);

        switch(levelStatus)
        {
            case LevelStatus.Locked:
                {
                    FindObjectOfType<AudioManager>().Play(SoundNames.Faah);
                    Debug.Log("Complete previous levels to unlock this level.");
                    break;
                }

            case LevelStatus.Unlocked:
                {
                    FindObjectOfType<AudioManager>().Stop(SoundNames.LevelSelectionBackground);
                    FindObjectOfType<AudioManager>().PlayOnce(SoundNames.LevelSelection);
                    LevelManager.Instance.LoadLevel(int.Parse(levelName.text));
                    break;
                }

            case LevelStatus.Completed:
                {
                    LevelManager.Instance.LoadLevel(int.Parse(levelName.text));
                    break;
                }
        }
    }

    void CheckStatus()
    {
        ColorBlock buttonClr = levelButton.colors;
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(levelName.text);

        if (levelStatus == LevelStatus.Completed)
        {
            buttonClr.normalColor = new Color32(255, 255, 255, 255);
        }
        else if(levelStatus == LevelStatus.Unlocked)
        {
            buttonClr.normalColor = new Color32(85, 255, 0, 255);
        }
        else
        {
            buttonClr.normalColor = new Color32(108, 76, 76, 255);
            buttonClr.highlightedColor = new Color32(108, 76, 76, 255);
            buttonClr.pressedColor = new Color32(108, 76, 76, 255);
            buttonClr.selectedColor = new Color32(108, 76, 76, 255);
            buttonClr.disabledColor = new Color32(108, 76, 76, 255);
        }

        levelButton.colors = buttonClr;
    }
}
