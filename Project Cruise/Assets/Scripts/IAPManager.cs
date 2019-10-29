using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    /// <summary>
    /// Directly adds Coins the quantity in the payout from the corresponding product.
    /// </summary>
    /// <param name="bought">The product that will be taken from the IAP Button component</param>
    public void AddCoinPurchase(Product bought)
    {
        GameData.Instance.coin += (int)bought.definition.payout.quantity;
    }
}
