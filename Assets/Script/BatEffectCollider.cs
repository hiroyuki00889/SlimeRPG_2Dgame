using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEffectCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.CompareTag("Player")) 
        {
            collider.gameObject.GetComponent<PlayerController>().down =true;
        }
    }
}
