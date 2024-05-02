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

    [SerializeField]
    AudioClip flapSound, crashSound, pointSound;

    int score;

    bool started;

    AudioSource audioSource;

    void Start() {
        started = false;
        scoreText.SetText("PRESS SPACE");
        audioSource = GetComponent<AudioSource>();
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
            if (!audioSource.isPlaying) {
                audioSource.clip = flapSound;
                audioSource.time = 0.1f;
                audioSource.Play();
            }
        }

        bool edgeCollision = bird.MoveAndCheckEdgeCollision();

        obstacles.MovePipes();
        if (edgeCollision || obstacles.CheckCollisionSquare(bird.transform.position, bird.transform.localScale.x / 2)) {
            started = false;
            audioSource.clip = crashSound;
            audioSource.time = 0.31f;
            audioSource.Play();
            scoreText.SetText("{0}\nGAME OVER", score);
            return;
        } 
        int score_update = obstacles.ScorePoints(bird.transform.position.x);
        if (score_update > 0) {
            audioSource.clip = pointSound;
            audioSource.Play();
            score += score_update;
        }
        scoreText.SetText("{0}", score);
    }
}
