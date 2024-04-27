using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Game : MonoBehaviour
{
    [SerializeField]
    Bird bird;

    [SerializeField]
    Obstacles obstacles;

    void Start() {
        obstacles.Initialize(Camera.main.orthographicSize * Screen.width / Screen.height + 0.5f);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            bird.Flap();
        }

        obstacles.MovePipes();
        if (obstacles.CheckCollisionSquare(bird.transform.position, bird.transform.localScale.x / 2)) {
        }
    }
}
