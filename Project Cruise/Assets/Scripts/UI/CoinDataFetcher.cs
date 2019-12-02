using UnityEngine;
using TMPro;

public class CoinDataFetcher : MonoBehaviour
{
    public TMP_Text coins;

    void Update()
    {
        this.coins.text = "" + GameData.Instance.saveData.Coins;
    }
}
