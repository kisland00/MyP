using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemAsset[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("ITEM NOT ADDED");
        }
    }
    public void GetSelectedItem()
    {
        ItemAsset receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Received item : " + receivedItem);
        }
        else
        {
            Debug.Log("No item received!");
        }
    }

    public void UseSelectedItem()
    {
        ItemAsset receivedItem = inventoryManager.GetSelectedItem(true);
        if(receivedItem != null)
        {
            Debug.Log("Used item : " + receivedItem);
        }
        else
        {
            Debug.Log("No item used!");
        }
    }
}
