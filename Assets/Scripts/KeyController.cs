using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<PlayerController>() != null)
        {
            player = collider.gameObject.GetComponent<PlayerController>();
            player.PickupKey();
            Destroy(gameObject);
        }
    }
}
