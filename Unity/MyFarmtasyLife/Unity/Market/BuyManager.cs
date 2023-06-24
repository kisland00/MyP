using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    public string selectedMarketId;
    public string selectedSellerId;
    public int selMaxCount;
    public int selPrice;
    public GameObject buyItemPref;
    public GameObject content;
    public ItemAsset selectedItem;

    void AddItemToList(string markID, string seller, string count, string price)
    {
        GameObject obj = Instantiate(buyItemPref, content.transform);
        obj.SetActive(true);
        MarketSlot slot = obj.GetComponent<MarketSlot>();
        slot.marketID = markID;
        slot.item = selectedItem;
        slot.maxCount = int.Parse(count);
        slot.SellerName = seller;
        slot.price = price;
    }
    private void OnEnable()
    {
        selectedItem = ItemCollecter.instance.items[ItemCollecter.instance.selected];
        Transform[] childs = content.GetComponentsInChildren<Transform>();
        foreach (Transform child in childs)
        {
            if (child == content.transform)
                continue;
            Destroy(child.gameObject);
        }
        AddItemToList("", "NPC", "99", selectedItem.marketPrice.ToString());
        string selstring = $"SELECT markId, seller, count, price " +
            $"FROM MarketTrade WHERE itemId = '{selectedItem.id}' AND count > 0";
        MarketClient.SendCMD(selstring, 'O');
        MarketClient.OnRecv += MarketClient_OnRecv;
    }

    private void MarketClient_OnRecv(List<string> arg)
    {
        AddList(arg);
        MarketClient.OnRecv -= MarketClient_OnRecv;
    }

    public void AddList(List<string> buylist)
    {
        for(int i =0;i<buylist.Count;i++)
        {
            string[] x = buylist[i].Split(' ');
            if (x[0] == "null")
            {
                Debug.Log("불러올 리스트가 없음");
                return;
            }
            AddItemToList(x[0], x[1], x[2], x[3]);
        }
    }
    public void ClickBuySellect(MarketSlot slot)
    {
        selectedMarketId = slot.marketID;
        selectedSellerId = slot.SellerName;
        selMaxCount = slot.maxCount;
        selPrice = int.Parse(slot.price);
    }
    public void ClickBuy(TMP_InputField count)
    {
        int inputCount = int.Parse(count.text);
        if (inputCount > selMaxCount)
        {
            Debug.Log("count is Over Max");
            return;
        }
        if(MarketClient.instance.umoney < inputCount * selPrice)
        {
            Debug.Log("Not Enough Money");
            MarketClient.instance.SetMoney();
            return;
        }
        if (selectedSellerId == "NPC")
        {
            Debug.Log("NPC " + inputCount.ToString() + " " + selPrice);
            string userm = $"UPDATE Account SET umoney= umoney - {selPrice * inputCount}" +
            $" WHERE usid = '{MarketClient.instance.id}'";
            MarketClient.SendCMD(userm, 'I');
        }
        else
        {
            string updateMarket = $"UPDATE MarketTrade SET count='{selMaxCount - inputCount}'" +
                $" WHERE markId='{selectedMarketId}' AND seller = '{selectedSellerId}'";
            MarketClient.SendCMD(updateMarket, 'I');
            string updateUserMoney = $"UPDATE Account SET umoney= umoney - {selPrice * inputCount}" +
                $" WHERE usid = '{MarketClient.instance.id}'";
            MarketClient.SendCMD(updateUserMoney, 'I');
            string sellerMoney = $"UPDATE Account SET umoney= umoney + {selPrice * inputCount}" +
                $" WHERE usid = '{selectedSellerId}'";
            MarketClient.SendCMD(sellerMoney, 'I');
        }
        for(int i =0;i<inputCount;i++)
        {
            InventoryManager.Instance.AddItem(selectedItem);
        }
        MarketClient.instance.SetMoney();
        this.gameObject.SetActive(false);
    }
}
