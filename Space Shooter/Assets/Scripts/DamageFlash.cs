using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.25f;
    public float maxAlpha = 0.6f;

    Coroutine running;

    void Awake()
    {
        if (flashImage != null)
        {
            var c = flashImage.color;
            c.a = 0f;
            flashImage.color = c;
        }
    }

    public void PlayFlash()
    {
        if (flashImage == null) return;
        if (running != null) StopCoroutine(running);
        running = StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        float half = flashDuration * 0.5f;
        // in
        float t = 0f;
        while (t < half)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(0f, maxAlpha, t / half);
            SetAlpha(a);
            yield return null;
        }
        // out
        t = 0f;
        while (t < half)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(maxAlpha, 0f, t / half);
            SetAlpha(a);
            yield return null;
        }
        SetAlpha(0f);
        running = null;
    }

    void SetAlpha(float a)
    {
        var c = flashImage.color;
        c.a = a;
        flashImage.color = c;
    }
}
