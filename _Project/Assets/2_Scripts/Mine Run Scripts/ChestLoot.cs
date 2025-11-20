using PlayerComponents;
using System.Collections.Generic;
using UnityEngine;

public class ChestLoot : MonoBehaviour
{
    PlayerController player;
    Animator animator;

    bool opened = false;

    void Awake()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponent<PlayerController>();

        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !opened)
        {
            Debug.Log("Asigno abrir cofre");
            player.pressButtonA = OpenChest;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Quito abrir cofre");
            player.pressButtonA = null;
        }
    }

    private void OpenChest()
    {
        Debug.Log("Estoy abriendo el cofre");

        animator.SetBool("isOpened", true);

        //Dictionary<MaterialName, int> materialsGenerated = GameData.MaterialsChest(5);
        GameData.MaterialsChest(5);

        //int i = 0;
        //foreach(MaterialName material in materialsGenerated.Keys)
        //{
        //    GameData.RunInventory.AddObject(material, materialsGenerated[material]);
        //    Debug.Log("HE AÑADIDO: " + material.ToString() + " " + i);
        //    i++;
        //}
        
        player.pressButtonA = null;
        opened = true;
    }
}
