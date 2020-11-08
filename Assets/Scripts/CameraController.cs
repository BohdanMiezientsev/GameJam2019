using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
  
    private void LateUpdate()
    {
        if(objectToFollow != null)transform.position = new Vector3(objectToFollow.position.x+8,0,  -10);
        
    }
}
