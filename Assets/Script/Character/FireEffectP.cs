using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffectP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("FireTarget"))
        {
            collider.gameObject.GetComponent<ObjectCollision>().step = true;
            Debug.Log("‚ ‚½‚è‚Ü‚µ‚½");
        }

    }
}
