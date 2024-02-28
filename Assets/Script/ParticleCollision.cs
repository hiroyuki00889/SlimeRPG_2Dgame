using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            Debug.Log("ƒ_ƒ[ƒW");
            //player‚ÌhpŒ¸‚ç‚·ˆ—
        }
    }
}
