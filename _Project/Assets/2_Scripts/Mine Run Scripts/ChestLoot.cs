using PlayerComponents;
using UnityEngine;

public class ChestLoot : MonoBehaviour
{
    Animator animator;

    bool opened = false;

    void Awake()
    {

        animator = GetComponent<Animator>();
    }

    public void OpenChest()
    {
        if (opened) return;

        Debug.Log("Estoy abriendo el cofre");

        animator.SetBool("isOpened", true);

        GameData.MaterialsChest(5);

        opened = true;
    }
}
