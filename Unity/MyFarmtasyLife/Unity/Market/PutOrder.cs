using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PutOrder : MonoBehaviour
{
    private MarketSlot asset;
    private TMP_InputField count;
    private TMP_InputField price;
    public GameObject pannel;
    public void putOrder()
    {
        asset = GetComponentInParent<MarketSlot>();
        count = transform.parent.Find("Count").GetComponent<TMP_InputField>();
        if(asset.maxCount < int.Parse(count.text))
        {
            Debug.Log("count is over MAX");
            return;
        }
        price = transform.parent.Find("Price").GetComponent<TMP_InputField>();

        string userID = MarketClient.instance.id;
        if (asset == null) return;
        string ins = "INSERT INTO MarketTrade(markId, itemId, seller, count, price)" +
        $" VALUES( (SELECT NVL(MAX(markId), 0) + 1 FROM MarketTrade WHERE seller = '{userID}'), '{asset.item.id}'," +
        $" '{userID}', '{count.text}', '{price.text}')";
        MarketClient.SendCMD(ins, 'I');
        InventoryManager.Instance.RemoveItem(asset.item, int.Parse(count.text));
        Debug.Log(asset.item.id + count.text + price.text);
        MarketClient.instance.SetMoney();
        pannel.SetActive(false);
    }
}
