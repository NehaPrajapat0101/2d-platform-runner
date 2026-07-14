using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour
{
    [SerializeField] private Transform heartContainer;

    private int lives;
    private GameObject[] hearts;
    [SerializeField] private Sprite redHeart;
    [SerializeField] private Sprite greyHeart;
    [SerializeField] private RuntimeAnimatorController greyHeartAnimation;

    private void Awake()
    {
        hearts = new GameObject[heartContainer.childCount];

        for (int i = 0; i < heartContainer.childCount; i++)
        {
            hearts[i] = heartContainer.GetChild(i).gameObject;
            hearts[i].SetActive(true);
        }

        lives = hearts.Length;
    }

    internal bool DecreaseHearts()
    {
        Debug.Log("Lives: " + lives);
        if(lives > 0)
        {

            //hearts[lives-1].SetActive(false);
            GreyHeart();
            lives--;

        }

        return lives == 0;

    }

    void GreyHeart()
    {
        Image heartImage = heartContainer.GetChild(lives - 1).GetComponent<Image>(); 
        heartImage.sprite = greyHeart;

        Animator animator = heartContainer.GetChild(lives - 1).GetComponent<Animator>();
        animator.runtimeAnimatorController = greyHeartAnimation;
    }
}
