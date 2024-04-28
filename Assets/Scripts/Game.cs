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

    void Start() {
        obstacles.Initialize(Camera.main.orthographicSize * Screen.width / Screen.height + 0.5f);
        score = 0;
        scoreText.SetText("{0}", score);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            bird.Flap();
        }

        obstacles.MovePipes();
        if (obstacles.CheckCollisionSquare(bird.transform.position, bird.transform.localScale.x / 2)) {
            scoreText.SetText("COLLISION");
            return;
        } 
        score += obstacles.ScorePoints(bird.transform.position.x);
        scoreText.SetText("{0}", score);
    }
}
