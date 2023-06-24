using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    static public void ToFarm()
    {
        if(InventoryManager.Instance != null)
            InventoryManager.Instance.UI.SetActive(true);
        
        if (GameManager.instance!= null)
        {
            if (GameManager.instance.IsGameOver)
            {
                string upd = $"UPDATE Account SET umoney = umoney - 200 WHERE usid = '{MarketClient.instance.id}'";
                MarketClient.SendCMD(upd, 'I');
                MarketClient.instance.SetMoney();
            }
            else
            {
                string upd = $"UPDATE Account SET umoney = umoney + {GameManager.instance.totScore} WHERE usid = '{MarketClient.instance.id}'";
                MarketClient.SendCMD(upd, 'I');
                MarketClient.instance.SetMoney();
            }
        }
        if (MarketClient.instance.id != null && MarketClient.instance.id != "")
        {
            SceneManager.LoadScene("Farm");
            SceneManager.LoadScene("MarketL", LoadSceneMode.Additive);
        }
        if (TurnManager.instance != null)
        {
            foreach (GameObject obj in TurnManager.instance.builded)
            {
                DontDestroyOnLoad(obj);
                obj.SetActive(true);
            }
        }
    }
    static public void ToFight()
    {
        InventoryManager.Instance.UI.SetActive(false);
        if (TurnManager.instance != null)
        {
            foreach (GameObject obj in TurnManager.instance.builded)
            {
                DontDestroyOnLoad(obj);
                obj.SetActive(false);
            }
        }
        SceneManager.LoadScene("Fight");
    }
}
