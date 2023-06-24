using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
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
    public void LogInClick()
    {
        //result.text = id.text + " " + pass.text;
        CheckIdPass();
    }

    private void CheckIdPass()
    {
        string select = $"SELECT * FROM Account WHERE usid='{id.text}'";
        MarketClient.SendCMD(select, 'O');
        MarketClient.OnRecv += MarketClient_OnRecv;

    }
    private void MarketClient_OnRecv(List<string> arg)
    {
        if (arg[0] == "null")
        {
            result.text = "Not correct username";
        }
        else
        {
            string[] st = arg[0].Split(" ");
            if (st[0] == id.text && st[1] == pass.text)
            {
                result.text = "Login Succes";
                MarketClient.instance.id = id.text;
            }
            else
            {
                result.text = "Not correct password";
            }
        }
        MarketClient.OnRecv -= MarketClient_OnRecv;
    }

    public void CloseButtonClick()
    {
        Destroy(this.gameObject);
    }
}
