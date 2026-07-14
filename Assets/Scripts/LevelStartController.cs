using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("Start");
        }
    }
}
