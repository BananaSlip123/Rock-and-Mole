using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{ 
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float i = 0f;
        while(i < duration)
        {
            float x = Random.Range(originalPosition.x - magnitude, originalPosition.x + magnitude);
            float y = Random.Range(originalPosition.y - magnitude, originalPosition.y - magnitude);

            transform.localPosition = new Vector3(x, y, originalPosition.z);
            i += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
