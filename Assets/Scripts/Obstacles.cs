using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField]
    Pipe pipePrefab;

    [SerializeField]
    int maxPipes;

    [SerializeField]
    float speed, pipeDistance, minGap, maxGap, minGapY, maxGapY;

    Pipe [] pipes;

    int createdPipes, leftmostPipeIdx;

    float lastPipeDistance, spawnX;

    public void Initialize(float spawnX) {
        this.spawnX = spawnX;

        pipes = new Pipe[maxPipes];
        Pipe pipe = Instantiate(pipePrefab);
        pipe.CreateBlocks();
        pipe.ResetBlocks(spawnX, Random.Range(minGap, maxGap), Random.Range(minGapY, maxGapY));
        pipes[0] = pipe;

        createdPipes = 1;
        leftmostPipeIdx = 0;
        lastPipeDistance = 0;
    }

    public void MovePipes() {
        float moveDistance = speed * Time.deltaTime;
        for (int i = 0; i < createdPipes; i++) {
            Pipe pipe = pipes[i];
            pipe.X -= moveDistance;
        }

        lastPipeDistance += moveDistance;
        if (lastPipeDistance > pipeDistance) {
            AddPipe();
        }
    }

    void AddPipe() {
        if (pipes[leftmostPipeIdx].X < -spawnX - 0.5f) {
            pipes[leftmostPipeIdx].ResetBlocks(spawnX, Random.Range(minGap, maxGap), Random.Range(minGapY, maxGapY));
            leftmostPipeIdx = (leftmostPipeIdx + 1) % createdPipes;
        } else {
            if (createdPipes == maxPipes) {
                Debug.Log("Maximum pipes reached");
                return;
            }
            Pipe pipe = Instantiate(pipePrefab);
            pipe.CreateBlocks();
            pipe.ResetBlocks(spawnX, Random.Range(minGap, maxGap), Random.Range(minGapY, maxGapY));
            pipes[createdPipes] = pipe;
            createdPipes += 1;
        }
        lastPipeDistance = 0;
    }

    public bool CheckCollisionSquare(Vector2 position, float size) {
        for (int i = 0; i < createdPipes; i++) {
            if (pipes[i].CheckCollisionSquare(position, size)) {
                return true;
            }
        }
        return false;
    }
}
