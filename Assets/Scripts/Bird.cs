using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    float flapSpeed, gravity, terminalVelocity, startY, animationSpeed;

    float velocity;

    bool freeFalling;

    public void Flap() {
        velocity = flapSpeed;
    }

    public void Reset() {
        freeFalling = false;
        velocity = 0;
        Vector2 position = transform.position;
        position.y = startY;
        transform.position = position;
    }

    public bool MoveAndCheckEdgeCollision() {
        Vector2 position = transform.position;
        velocity = Mathf.Max(terminalVelocity, velocity - gravity * Time.deltaTime);
        position.y += velocity * Time.deltaTime;

        transform.position = position;
        
        return position.y < -4.5 || position.y > 4.5;
    }

    public void StartFreeFall() {
        freeFalling = true;
        velocity = terminalVelocity;
    }

    public void Fall() {
        if (freeFalling) {
            Vector2 position = transform.position;
            position.y += velocity * Time.deltaTime;
            transform.position = position;
        }
    }

    public void StopAnimating() {
        Animator animator= GetComponent<Animator>();
        animator.speed = 0;
    }

    public void StartAnimating() {
        Animator animator = GetComponent<Animator>();
        animator.speed = animationSpeed;
    }
}
