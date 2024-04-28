using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField]
    PipeBlock pipeBlock;

    float x;
    
    PipeBlock top, bot;

    public bool Scored { get; set; }

    public float X { get { return x; } set { x = value; top.SetX(value); bot.SetX(value); } }

    public void CreateBlocks() {
        top = Instantiate(pipeBlock);
        bot = Instantiate(pipeBlock);
    }

    public void ResetBlocks(float x, float gapSize, float gapY) {
        top.SetY(gapY + gapSize + top.transform.localScale.y / 2f);
        bot.SetY(gapY - gapSize - top.transform.localScale.y / 2f);
        X = x;
        Scored = false;
    }

    public bool CheckCollisionSquare(Vector2 position, float size) {
        return top.CheckCollisionSquare(position, size) || bot.CheckCollisionSquare(position, size);
    }
}
