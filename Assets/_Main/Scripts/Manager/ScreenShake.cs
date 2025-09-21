using UnityEngine;
using System.Collections;

public static class ScreenShake
{
    private static MonoBehaviour coroutineHost;

    private static Camera cam;

    /// <summary>
    /// Initialize with a MonoBehaviour host that can run coroutines (e.g. GameManager).
    /// </summary>
    public static void Initialize(Camera _cam, MonoBehaviour host)
    {
        cam = _cam;
        coroutineHost = host;
    }

    /// <summary>
    /// Shake the given camera for duration and magnitude, without altering its true position.
    /// </summary>
    public static void Shake(float duration, float magnitude)
    {
        if (coroutineHost == null)
        {
            Debug.LogWarning("ScreenShake2D not initialized! Call Initialize() first.");
            return;
        }

        coroutineHost.StartCoroutine(DoShake(duration, magnitude));
    }

    private static IEnumerator DoShake(float duration, float magnitude)
    {
        Transform camTransform = cam.transform;
        Vector3 originalPos = camTransform.position;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            camTransform.position = originalPos + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Restore exact original position
        camTransform.position = originalPos;
    }
}
