using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBeforeLevel : MonoBehaviour
{
    public static LoadingBeforeLevel loadingBeforeLevelInstance;

    public Slider progressBar;
    public Animator animator;  

    IEnumerator Start()
    {

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        if (LevelManager.Instance == null)
        {
            Debug.LogError("LevelManager.Instance is NULL!");
            yield break;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(LevelManager.Instance.sceneToLoad);

        operation.allowSceneActivation = false;

        while(operation.progress < 0.9f)
        {
            progressBar.value = operation.progress;

            yield return null;
        }

        progressBar.value = 1f;

        animator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1f);

        operation.allowSceneActivation = true;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

    }
}
