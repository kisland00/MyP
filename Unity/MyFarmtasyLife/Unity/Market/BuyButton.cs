using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public string itemMarketId;
    public void BuyClicked()
    {
        string count = this.transform.Find("Count").GetComponent<TMP_Text>().text;
        string price = this.transform.Find("Price").GetComponent<TMP_Text>().text;
        if(itemMarketId != null)
        {
            Debug.Log(count + price);
        }
    }
}
