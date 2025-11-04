using PlayerComponents;
using System.Collections.Generic;
using UnityEngine;

public class ChestLoot : MonoBehaviour
{
    PlayerController player;

    bool opened = false;

    void Awake()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponent<PlayerController>();
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
        Dictionary<MaterialName, int> materialsGenerated = GameData.MaterialsChest(5);

        foreach(MaterialName material in materialsGenerated.Keys)
        {
            GameData.RunInventory.AddObject(material, materialsGenerated[material]);
        }
        
        player.pressButtonA = null;
        opened = true;
    }
}
