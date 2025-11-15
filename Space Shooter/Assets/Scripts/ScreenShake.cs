using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;

    Vector3 originalPos;
    Coroutine running;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        originalPos = transform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        if (running != null) StopCoroutine(running);
        running = StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float percent = elapsed / duration;
            float damper = 1f - Mathf.Clamp01(percent);
            float x = (Random.value * 2f - 1f) * magnitude * damper;
            float y = (Random.value * 2f - 1f) * magnitude * damper;
            transform.localPosition = originalPos + new Vector3(x, y, 0f);
            yield return null;
        }
        transform.localPosition = originalPos;
        running = null;
    }
}
