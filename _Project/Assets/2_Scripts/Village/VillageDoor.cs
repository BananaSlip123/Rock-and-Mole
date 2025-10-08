using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class VillageDoor : MonoBehaviour
{
    [SerializeField] DoorType doorType;

    static readonly Dictionary<DoorType, Action> _doorActions = new Dictionary<DoorType, Action>
    {
        { DoorType.shopEntry, ()=>{ VillageNavigation.Instance?.OnShopEntry(); } },
        { DoorType.forgeEntry, ()=>{ VillageNavigation.Instance?.OnForgeEntry(); } },
        { DoorType.villageEntry, () => { VillageNavigation.Instance ?.OnVillageEntry(); } },
        { DoorType.minesEntry, () => { VillageNavigation.Instance ?.OnMineEntry(); } },
    };
    public enum DoorType
    {
        shopEntry,
        forgeEntry,
        villageEntry,
        minesEntry,

    }
    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    // Este método se llama automáticamente cuando un objeto entra en el collider trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _doorActions[doorType].Invoke();
        }
    }
}
