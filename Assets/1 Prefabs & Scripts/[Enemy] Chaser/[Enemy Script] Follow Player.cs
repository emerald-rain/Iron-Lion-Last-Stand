using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    // https://www.youtube.com/watch?v=dmQyfWxUNPw&t=98s

    public float speed;
    public Transform target;
    public float minimumDistance;

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            // ATTACK FUNC
        }
    }
}
