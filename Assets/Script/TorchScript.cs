using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchScript : MonoBehaviour
{
    public Image gimmic;
    public Color color;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) 
        {
            color.a = 0;
            gimmic.color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            color.a = 1;
            gimmic.color = color;
        }
    }
}
