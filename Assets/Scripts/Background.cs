using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField]
    float scrollSpeed;

    public void Scroll() {
        RawImage image = GetComponent<RawImage>();
        var uvRect = image.uvRect;
        uvRect.x += scrollSpeed * Time.deltaTime;
        image.uvRect = uvRect;
    }
}
