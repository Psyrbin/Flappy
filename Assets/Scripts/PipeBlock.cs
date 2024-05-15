using UnityEngine;

public class PipeBlock : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sprite;

    public bool CheckCollisionSquare(Vector2 position, float size) {
        float left = transform.position.x - transform.localScale.x / 2f;
        float right = transform.position.x + transform.localScale.x / 2f;
        float top = transform.position.y + transform.localScale.y / 2f;
        float bot = transform.position.y - transform.localScale.y / 2f;

        bool collision =  position.x + size > left && position.x - size < right && position.y + size > bot && position.y - size < top;

        // SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (collision) {
            sprite.color = Color.red;
        } else {
            sprite.color = Color.white;
        }

        return collision;
    }

    public void SetY(float y) {
        Vector2 position = transform.position;
        position.y = y;
        transform.position = position;
    }

    public void SetX(float x) {
        Vector2 position = transform.position;
        position.x = x;
        transform.position = position;
    }
}
