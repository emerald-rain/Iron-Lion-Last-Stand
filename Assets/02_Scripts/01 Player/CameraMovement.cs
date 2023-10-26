using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // ttps://www.youtube.com/watch?v=TYefpMf0cJ8&ab_channel=MoreBBlakeyyy
    // thank you very much for this camera movement tutorial

    public Transform player;
    public Vector3 offset;
    public float speed;

    void FixedUpdate() // made it FixedUpdate for better smoothness on high values
    {
        Vector3 desiredPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, speed * Time.deltaTime);
    }
}