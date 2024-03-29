using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float y0ffset = 1f;
    public Transform target;


    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + y0ffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
