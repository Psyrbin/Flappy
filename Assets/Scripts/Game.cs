using System.IO;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{

    struct State {
        public int highScore, numRuns;
    }

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

    State state;

    AudioSource audioSource;

    string savePath = "save.txt";

    void Start() {
        LoadState(savePath);
        started = false;
        pauseScreen.enabled = false;
        paused = false;
        bird.StopAnimating();
        scoreText.SetText("PRESS SPACE");
    }

    void StartNewGame() {
        obstacles.Dispose();
        bird.Reset();
        bird.StartAnimating();
        obstacles.Initialize(Camera.main.orthographicSize * Screen.width / Screen.height + 0.5f);
        score = 0;
        scoreText.SetText("{0}", score);
        started = true;
    }

    void EndGame() {
        started = false;
        bird.StartFreeFall();
        state.highScore = Mathf.Max(state.highScore, score);
        state.numRuns += 1;
        SaveState(savePath);
        SoundManager.Instance.PlaySound(crashSound, 0.31f);
        bird.StopAnimating();
        scoreText.SetText("{0}\nBEST: {1}\nTOTAL TRIES: {2}\nGAME OVER", score, state.highScore, state.numRuns);
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
            bird.Fall();
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
            EndGame();
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
        bird.StopAnimating();
        pauseScreen.enabled = true;
    }

    void UnPause() {
        paused = false;
        bird.StartAnimating();
        pauseScreen.enabled = false;
    }

    void SaveState(string path) {
        string serializedState = JsonUtility.ToJson(state);
        File.WriteAllText(path, serializedState);
    }

    void LoadState(string path) {
        if (!File.Exists(path)) {
            state = new State{highScore = 0, numRuns = 0};
        } else {
            string serializedState = File.ReadAllText(path);
            state = JsonUtility.FromJson<State>(serializedState);
        }
    }
}
