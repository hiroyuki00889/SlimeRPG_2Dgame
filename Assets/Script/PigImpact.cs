using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigImpact : MonoBehaviour
{
    private void Hakai()
    {
        Destroy(this.gameObject,1f);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerController>().down = true;
        }
    }
}
