using UnityEngine;
using TMPro;

public class CoinDataFetcher : MonoBehaviour
{
    public TMP_Text coins;

    void Start()
    {
        this.coins.text = "" + GameData.Instance.saveData.Coins;
    }
}
