using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Register : MonoBehaviour
{
    TMP_Text result;
    TMP_InputField pass;
    TMP_InputField id;
    void Start()
    {
        result = this.transform.Find("Result").GetComponent<TMP_Text>();
        pass = this.transform.Find("password").GetComponent<TMP_InputField>();
        id = this.transform.Find("id").GetComponent<TMP_InputField>();
    }
    public void RegClick()
    {
        //result.text = id.text + " " + pass.text;
        CheckDuplication();
    }

    private void CheckDuplication()
    {
        string select = $"SELECT usid FROM Account WHERE usid='{id.text}'";
        MarketClient.SendCMD(select, 'O');
        MarketClient.OnRecv += MarketClient_OnRecv;
    }

    private void MarketClient_OnRecv(List<string> arg)
    {
        if (arg[0] == "null")
        {
            string istring = @"INSERT INTO Account(usid, pwd)" + "\r\n" +
                               $"VALUES('{id.text}', '{pass.text}')";
            MarketClient.SendCMD(istring, 'I');
            result.text = "Registered";
        }
        else
        {
            result.text = "Id is using";
        }
        MarketClient.OnRecv -= MarketClient_OnRecv;
    }

    public void CloseButtonClick()
    {
        Destroy(this.gameObject);
    }
}
