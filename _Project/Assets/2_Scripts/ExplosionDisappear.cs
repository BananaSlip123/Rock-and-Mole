using System.Collections;
using UnityEngine;

public class ExplosionDisappear : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyExplosion());

        //reproducir sonido de explosion
        AudioManager.Instance.PlayAudio(AudioManager.AudioType.Explosion);
    }

    private IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);
    }
}
