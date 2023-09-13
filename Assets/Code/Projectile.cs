using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;
    private Vector3 target;
    private Action onHit;

    public void MoveTo(Vector3 source, Vector3 target, Action onHit)
    { 
        transform.position = source;    
        this.target = target;
        this.onHit = onHit;
    }

    void Update()
    {
        if (transform.position == target)
        {
            return;  
        }
        Vector3 dist = target - transform.position;
        Vector3 velocity = dist.normalized * Speed * Time.deltaTime;

        if (velocity.magnitude > dist.magnitude)
        { 
            transform .position = target;
            onHit();
            Destroy(gameObject, 1);
        }
        else
        {
            transform.position += velocity;
        }
    }
}
