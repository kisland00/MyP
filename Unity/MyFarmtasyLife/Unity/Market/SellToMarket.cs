using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SellToMarket : MonoBehaviour
{
    private MarketSlot asset;
    private TMP_InputField count;
    private TMP_Text price;
    public GameObject pannel;
    public void putOrder()
    {
        asset = GetComponentInParent<MarketSlot>();
        count = transform.parent.Find("Count").GetComponent<TMP_InputField>();
        if (asset.maxCount < int.Parse(count.text))
        {
            Debug.Log("count is over MAX");
            return;
        }
        price = transform.parent.Find("Price").GetComponent<TMP_Text>();

        string userID = MarketClient.instance.id;
        if (asset == null) return;
        string upd = $"UPDATE Account SET umoney = umoney + {int.Parse(price.text) * int.Parse(count.text)} WHERE usid = '{MarketClient.instance.id}'";
        MarketClient.SendCMD(upd, 'I');
        InventoryManager.Instance.RemoveItem(asset.item, int.Parse(count.text));
        MarketClient.instance.SetMoney();
        pannel.SetActive(false);
    }
}
