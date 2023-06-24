using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    static public void ClickClose(GameObject obj)
    {
        obj.SetActive(false);
        ItemCollecter.instance.selected = -1;
    }
}
