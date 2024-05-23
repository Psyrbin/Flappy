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
        velocity = Mathf.Max(terminalVelocity, velocity - gravity * Time.deltaTime);
        Vector3 position = UpdatePositionAndRotation();
        
        return position.y < -4.5 || position.y > 4.5;
    }

    public void StartFreeFall() {
        freeFalling = true;
        velocity = terminalVelocity;
    }

    public void Fall() {
        if (freeFalling) {
            UpdatePositionAndRotation();
        }
    }

    Vector3 UpdatePositionAndRotation() {
        Vector2 position = transform.position;
        position.y += velocity * Time.deltaTime;
        transform.position = position;

        Vector3 rotAngle = new Vector3(0, 0, -velocity / terminalVelocity * 80);
        transform.rotation = Quaternion.Euler(rotAngle);
        return position;
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
