using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
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
