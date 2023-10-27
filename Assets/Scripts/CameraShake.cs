using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 velocity;
    float smoothTime = 0.1f;
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 pos = transform.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = pos.x + Random.Range(-1f, 1f) * magnitude;
            float y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(x, y, transform.position.z), ref velocity, smoothTime);
            
            transform.position = new Vector3(x, y, pos.z);
            yield return null;
            elapsed += Time.deltaTime;
        }
        transform.localPosition = pos;
    }

    public IEnumerator Shake2(float singleDuration, float magnitude, int times)
    {
        Camera cam = GetComponent<Camera>();

        float size = cam.orthographicSize;
        float v = magnitude / singleDuration * 2f;
        for (int i = 0; i < times; ++i)
        {
            float elapsed = 0f;
            while (elapsed < singleDuration / 2f)
            {
                float sz = size - v * Time.deltaTime;
                cam.orthographicSize = sz;
                yield return null;
                elapsed += Time.deltaTime;
            }
            elapsed = 0f;
            while (elapsed < singleDuration / 2f)
            {
                float sz = size + v * Time.deltaTime;
                cam.orthographicSize = sz;
                yield return null;
                elapsed += Time.deltaTime;
            }
        }
        cam.orthographicSize = size;
    }
}
