using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
	public GameObject panel;
	public GameObject content;
	public GameObject itempanel;

	void Start()
	{
		panel.SetActive(false);
	}
    private void OnEnable()
    {
        Transform[] childs = content.GetComponentsInChildren<Transform>();
        foreach (Transform child in childs)
        {
            if (child == content.transform)
                continue;
            Destroy(child.gameObject);
        }
        AddAll();
    }
    void AddAll()
	{
		for(int i =0;i<ItemCollecter.instance.items.Count;i++)
		{
            GameObject obj = Instantiate(itempanel, content.transform);
			ItemAsset item = ItemCollecter.instance.items[i];
			obj.GetComponentInChildren<MarketSlot>().item = item;
            obj.GetComponentInChildren<MarketSlot>().maxCount = InventoryManager.Instance.ItemCount(item);
            obj.SetActive(true);
        }
    }
	public void OnClickGo2Market () {
		panel.SetActive(true);
		UpdateCount();
		MarketClient.instance.SetMoney();
	}

    private void UpdateCount()
    {
		MarketSlot[] items = content.GetComponentsInChildren<MarketSlot>();
		foreach(MarketSlot item in items)
		{
			item.maxCount = InventoryManager.Instance.ItemCount(item.item);
		}
    }

    public void OnClickGetOut () {
		panel.SetActive(false);
	}

    public GameObject buyWindow;
    public void BuyClicked()
    {
		if(ItemCollecter.instance.selected != -1)
		{
			buyWindow.SetActive(true);
            ItemCollecter.instance.selected = -1;
		}
    }
    public GameObject sellWindow;
	public void SellClicked()
	{
		if (ItemCollecter.instance.selected != -1)
		{
			ItemAsset item = ItemCollecter.instance.items[ItemCollecter.instance.selected];
			int count = InventoryManager.Instance.ItemCount(item);

            if (count > 0)
			{
				MarketSlot[] slots = sellWindow.GetComponentsInChildren<MarketSlot>();
				foreach(MarketSlot slot in slots)
				{
					slot.item= item;
					slot.maxCount = count;
				}
				sellWindow.SetActive(true);
            }
        }
	}
}
