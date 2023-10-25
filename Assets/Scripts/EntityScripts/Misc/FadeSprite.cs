using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSprite : MonoBehaviour
{

    public void StartFade(float initialDelay, float startOpacity)
    {
        StartCoroutine(Fade(initialDelay, startOpacity));
    }
    private IEnumerator Fade(float initialDelay, float startOpacity)
    {
        yield return new WaitForSeconds(initialDelay);
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        for(float i = startOpacity; i > 0; i -= 0.05f)
        {
            sprite.color = new(1, 1, 1, i);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
