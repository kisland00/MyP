using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowNick : MonoBehaviour
{
    void Update()
    {
        if(MarketClient.instance.id != null)
        {
            this.GetComponent<TMP_Text>().text = MarketClient.instance.id;
        }
    }
}
