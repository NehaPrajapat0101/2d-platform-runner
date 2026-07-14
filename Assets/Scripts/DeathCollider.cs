using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("DeathBed activated");
            PlayerController playerController = collider.gameObject.GetComponent<PlayerController>();

            playerController.TakeDamage();
        }
    }
}
