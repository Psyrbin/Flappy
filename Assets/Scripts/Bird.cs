using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    float flapSpeed, gravity, terminalVelocity;

    float velocity;

    public void Flap() {
        velocity = flapSpeed;
    }

    void Update() {
        Vector2 position = transform.position;
        velocity = Mathf.Max(terminalVelocity, velocity - gravity * Time.deltaTime);
        position.y += velocity * Time.deltaTime;

        if (position.y < -4.5) position.y = 4.5f;
        transform.position = position;
    }
}
