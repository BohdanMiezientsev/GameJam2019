using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private bool topdown;
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<PlayerController>().changeStress(70);
            other.gameObject.GetComponentInParent<PlayerController>().BounceFrom(topdown);
            
        }

    }
}
