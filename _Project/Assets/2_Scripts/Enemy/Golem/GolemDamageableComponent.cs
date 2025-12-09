using System.Collections.Generic;
using UnityEngine;

public class GolemDamageableComponent : MonoBehaviour, IDamageableComponent
{
    private bool hasBeenDamaged = false;

    [SerializeField] private int health = 50;
    [SerializeField] Animator animator;
    private float timeToDeath = 0f;
    const float TIME_TO_DEATH = 1f;
    [SerializeField] EnemyName tipoEnemigo;
    MaterialChanger changer;

    private void Awake()
    {
        if (BiomeManager.CurrentBiome == BiomeName.undergroundForest)
            health = (int)(health*1.5f);
    }

    void Start()
    {
        changer = GetComponent<MaterialChanger>();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) return;
    }

    private void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null) return;
        if (animator.GetBool("Morir"))
        {
            timeToDeath += Time.fixedDeltaTime;

            if(timeToDeath >= TIME_TO_DEATH)
                DeathLogic();
        }
    }

    public void RecieveDamage(int damage, float duration, float magnitude)
    {
        health -= damage;
        hasBeenDamaged = true;

        if(health <= 0)
            Death();
        changer.AssignTemporalMaterial();
        Debug.Log("Me han quitado vida");
    }

    public bool GetHasBeenDamaged()
    {
        return hasBeenDamaged;
    }

    public void ResetHasBeenDamaged()
    {
        hasBeenDamaged = false;
        Debug.Log("He salido del area");
    }

    private void Death()
    {
        animator.SetBool("Morir", true); 

        //Reproducir sonido de muerte
        AudioManager.Instance.PlayAudio(AudioManager.AudioType.DeathEnemySound);       
    }

    private void DeathLogic()
    {
        
        if (LevelManager.instance != null)
        {
            timeToDeath = 0;
            LevelManager.instance.EnemyDead();

            //Dictionary<MaterialName, int> materialsGenerated = GameData.EnemyLoot(UnityEngine.Random.Range(2,4),tipoEnemigo);
            GameData.EnemyLoot(UnityEngine.Random.Range(2,4),tipoEnemigo);

            //int i = 0;
            //foreach (MaterialName material in materialsGenerated.Keys)
            //{
            //    GameData.RunInventory.AddObject(material, materialsGenerated[material]);
            //    Debug.Log("HE AÑADIDO: " + material.ToString() + " " + i);
            //    i++;
            //}
        }
        Destroy(this.gameObject);
    }
}
