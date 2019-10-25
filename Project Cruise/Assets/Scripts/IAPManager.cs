using UnityEngine;

public class IAPManager : MonoBehaviour
{
    public void AddCoinPurchase(int coin)
    {
        GameData.Instance.coin += coin;
    }
}
