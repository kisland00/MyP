using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickStuffImg : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
    , IPointerDownHandler
{
    private Image image;
    public bool isChecked = false;
    public GameObject parentObject;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = new Color(0.8f, 0.8f, 0.8f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isChecked == true)
        {
            image.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            image.color = Color.white;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isChecked = !isChecked;

        if (isChecked == true)
        {
            image.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            image.color = Color.white;
        }
    }
}
