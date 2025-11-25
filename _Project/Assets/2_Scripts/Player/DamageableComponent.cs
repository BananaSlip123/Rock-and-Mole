using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageableComponent : MonoBehaviour, IDamageableComponent
{
    private bool hasBeenDamaged = false;

    [SerializeField] private int health = 0;
    [SerializeField] private bool isInmortal = false; //en algunas salas de tutorial ser inmortal
    public int Health
    {
        get => health;
        private set
        {
            health = value;
            OnHealthChange?.Invoke(value);
        }
    }

    public Action<int> OnHealthChange;
    public Action OnDamageReceive;
    public Action OnDeath;
    PlayerStats player;
    [SerializeField]CameraShake camera;

    private void Awake()
    {
        player = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        if (!GameData.NeedsTutorial && isInmortal) isInmortal = false;
    }

    public void SetHealth(int health)
    {
        Health = health;
    }

    public void RecieveDamage(int damage, float duration, float magnitude)
    {
        if (isInmortal && (Health <= 35 || Health <= damage)) return;

        player.HealPlayer(-damage);
        OnDamageReceive?.Invoke();

        StartCoroutine(camera.Shake(duration,magnitude));

        if(!hasBeenDamaged)
        {
            hasBeenDamaged = true;
            ResetHasBeenDamaged();
        }
        

        if(Health <= 0)
            Death();
        Debug.Log("Me han quitado vida :" + damage + " me queda: "+ Health);
    }

    public void SetHasBeenDamaged(bool change)
    {
        hasBeenDamaged = change;
    }

    public bool GetHasBeenDamaged()
    {
        return hasBeenDamaged;
    }

    public void ResetHasBeenDamaged()
    {
        StartCoroutine(InvencivilityTime());
    }

    IEnumerator InvencivilityTime()
    {
        yield return new WaitForSeconds(2f);

        hasBeenDamaged = false;
    }

    private void Death()
    {
        Destroy(this.gameObject);
        OnDeath?.Invoke();
    }
}
