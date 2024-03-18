using UnityEngine;
using System.Collections;

public class VisualizationShake : MonoBehaviour
{
    public float shakeAmount = 5.0f; // Besarnya getaran dalam derajat
    public float shakeDuration = 1.0f; // Durasi getaran

    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.localRotation;
    }

    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeAmount;
            float y = Random.Range(-1f, 1f) * shakeAmount;
            float z = Random.Range(-1f, 1f) * shakeAmount;

            transform.localRotation = Quaternion.Euler(x, y, z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localRotation = originalRotation;
    }
}
