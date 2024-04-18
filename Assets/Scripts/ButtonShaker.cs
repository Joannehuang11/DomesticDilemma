using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShaker : MonoBehaviour
{
    public float shakeAmount = 5f; // Amount of movement
    public float duration = 0.5f; // Duration of shaking
    private Vector3 originalPosition;

    void Awake()
    {
    }

    public void ShakeButton()
    {
        StopAllCoroutines();
        originalPosition = transform.localPosition;
        StartCoroutine(startShake());
    }

    private IEnumerator startShake()
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
            yield return null; // Wait until the next frame
        }

        transform.localPosition = originalPosition; // Reset to the original position
    }
}
