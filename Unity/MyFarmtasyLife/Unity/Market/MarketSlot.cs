using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketSlot : MonoBehaviour
{
    [SerializeField] Image image;

    public string marketID;
    public string SellerName;
    public string price;
    public ItemAsset item;
    public TMP_Text text;
    public TMP_Text NPCtext;
    public int maxCount;
    public bool isNPC = false;
    private void OnEnable()
    {

    }
    private void Update()
    {
        if (isNPC)
        {
            if (item != null)
            {
                image.sprite = item.itemImage;
                if (text != null)
                    text.text = item.description;
                if (NPCtext != null)
                    NPCtext.text = ((int)(item.marketPrice / 2)).ToString();
                if (maxCount > 0)
                {
                    GameObject obj = transform.Find("MaxCount")?.gameObject;
                    if (obj != null)
                        obj.GetComponent<TMP_Text>().text = "MAX : " + maxCount;
                    else
                    {
                        transform.Find("Count").GetComponent<TMP_Text>().text = "99";
                        transform.Find("Price").GetComponent<TMP_Text>().text = item.marketPrice.ToString();
                        transform.Find("Seller").GetComponent<TMP_Text>().text = "NPC";
                    }
                }
                image.color = new Color(1, 1, 1, 1);
            }
        }
        if (!isNPC)
        {
            if (item != null)
            {
                image.sprite = item.itemImage;
                if (text != null)
                    text.text = item.description;
                GameObject obj = transform.Find("MaxCount")?.gameObject;
                if (obj != null)
                    obj.GetComponent<TMP_Text>().text = "";
                if (maxCount > 0)
                {
                    if (obj != null)
                        obj.GetComponent<TMP_Text>().text = "MAX : " + maxCount;
                    else
                    {
                        
                        transform.Find("Count").GetComponent<TMP_Text>().text = "" + maxCount;
                        transform.Find("Price").GetComponent<TMP_Text>().text = price;
                        transform.Find("Seller").GetComponent<TMP_Text>().text = SellerName;
                    }
                }
                image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }
}
