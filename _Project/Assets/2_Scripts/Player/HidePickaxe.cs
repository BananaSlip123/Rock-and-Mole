using UnityEngine;

public class HidePickaxe : MonoBehaviour
{
    public static HidePickaxe instance;

    [SerializeField] GameObject walkingPickaxe;
    [SerializeField] GameObject attackPickaxe;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //Debug.Log("HOLAAAAAAAA");
        }
    }

    public void HidePickaxeAnimation(bool attack)
    {
        walkingPickaxe.SetActive(!attack);
        attackPickaxe.SetActive(attack);        
    }
}
