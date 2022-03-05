using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
   
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y+2, -10);
    }
}
