using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    ItemCollecter ic;
    public void Start()
    {
        ic = GameObject.Find("ItemMan").GetComponent<ItemCollecter>();
    }
    public void OnSelectClick()
    {
        ic.selected = this.GetComponentInParent<MarketSlot>().item.id;
    }
}
