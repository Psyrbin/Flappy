using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Game : MonoBehaviour
{
    [SerializeField]
    Bird bird;

    [SerializeField]
    Obstacles obstacles;

    [SerializeField]
    TextMeshPro scoreText;

    int score;

    bool started;

    void Start() {
        started = false;
        scoreText.SetText("PRESS SPACE");
    }

    void StartNewGame() {
        obstacles.Dispose();
        bird.Reset();
        obstacles.Initialize(Camera.main.orthographicSize * Screen.width / Screen.height + 0.5f);
        score = 0;
        scoreText.SetText("{0}", score);
        started = true;
    }

    void Update() {
        if (!started) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                StartNewGame();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            bird.Flap();
        }

        bool edgeCollision = bird.MoveAndCheckEdgeCollision();

        obstacles.MovePipes();
        if (edgeCollision || obstacles.CheckCollisionSquare(bird.transform.position, bird.transform.localScale.x / 2)) {
            started = false;
            scoreText.SetText("{0}\nGAME OVER", score);
            return;
        } 
        score += obstacles.ScorePoints(bird.transform.position.x);
        scoreText.SetText("{0}", score);
    }
}
