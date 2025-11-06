using System.Collections;
using UnityEngine;

public class ExplosionDisappear : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyExplosion());
    }

    private IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(5f);

        Destroy(this.gameObject);
    }
}
