using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

    [SerializeField]
    Canvas pauseScreen;

    [SerializeField]
    Background background;

    int score;

    bool started, paused;

    AudioSource audioSource;

    void Start() {
        started = false;
        pauseScreen.enabled = false;
        paused = false;
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
        if (paused) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                UnPause();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
            return;
        }

        if (!started) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                StartNewGame();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            bird.Flap();
            SoundManager.Instance.PlaySound(flapSound);
        }

        bool edgeCollision = bird.MoveAndCheckEdgeCollision();

        background.Scroll();

        obstacles.MovePipes();
        if (edgeCollision || obstacles.CheckCollisionSquare(bird.transform.position, bird.transform.localScale.x / 2)) {
            started = false;
            SoundManager.Instance.PlaySound(crashSound, 0.31f);
            scoreText.SetText("{0}\nGAME OVER", score);
            return;
        } 
        int score_update = obstacles.ScorePoints(bird.transform.position.x);
        if (score_update > 0) {
            SoundManager.Instance.PlaySound(pointSound);
            score += score_update;
        }
        scoreText.SetText("{0}", score);
    }

    void Pause() {
        paused = true;
        pauseScreen.enabled = true;
    }

    void UnPause() {
        paused = false;
        pauseScreen.enabled = false;
    }
}
