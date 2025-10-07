using UnityEngine;
using TMPro;
public class CoinsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt_coins;

    int Coins
    {
        set => txt_coins.text = value.ToString();
    }
    private void Awake()
    {
        Coins = GameData.Coins;
        GameData.OnCoinsChange += (int newCoins) => Coins = newCoins;
    }
}
