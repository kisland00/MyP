using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using TMPro;

public class MarketClient : MonoBehaviour
{
    [Serializable]
    class IOPacket
    {
        public char IO;
        public string CMD;
    }
    [Serializable]
    class ReaderPacket
    {
        public List<string> READER;
    }

    static Socket socket;
    static IPEndPoint ipep;
    public string id;
    static public MarketClient instance;
    static bool isRunRecv = false;
    public delegate void RecvDeleg(List<string> arg);
    public static event RecvDeleg OnRecv;
    public static Queue<List<string>> lsqu = new Queue<List<string>>();
    static NetworkStream ns;
    static StreamWriter sw;
    static StreamReader sr;
    public int umoney;
    public delegate void MoneyDele(int arg);
    public MoneyDele MoneyRecv;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork,
                                            SocketType.Stream,
                                            ProtocolType.Tcp);
        ipep = new IPEndPoint(IPAddress.Parse("192.168.0.220"),
                                    666);
        Debug.Log("서버 접속 시도...");
        socket.Connect(ipep);
        Debug.Log("서버 접속 연결!!!");

        ns = new NetworkStream(socket);
        sw = new StreamWriter(ns);
        sr = new StreamReader(ns);
        isRunRecv = true;
        Thread threadRecv = new Thread(new ThreadStart(ThreadRecv));
        threadRecv.Start();
    }
    private void Update()
    {
        if(OnRecv != null && lsqu.Count > 0)
        {
            OnRecv(lsqu.Dequeue());
        }
    }
    void ThreadRecv()
    {
        while (isRunRecv)
        {
            string data = null;
            try
            {
                data = sr.ReadLine();
                if (data == null)
                    isRunRecv = false;
                else
                {
                    ReaderPacket read =  JsonUtility.FromJson<ReaderPacket>(data);
                    //if(OnRecv != null)
                    //    OnRecv(read.READER);
                    lsqu.Enqueue(read.READER);
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"Recv Error : {ex.Message}");
                Debug.Log($"Json Data : {data}");
                isRunRecv = false;
            }
        }
    }
    static public void SendCMD(string cmd, char io)
    {
        try
        {
            IOPacket cmdP = new IOPacket();
            cmdP.IO = io;
            cmdP.CMD = cmd;
            string h = JsonUtility.ToJson(cmdP);
            Debug.Log(h);
            sw.WriteLine(h);
            sw.Flush();
        }
        catch (Exception ex)
        {
            Debug.LogError("Send Error : " + ex.Message);
            throw ex;
        }
    }

    public void SetMoney()
    {
        string moneys = $"SELECT umoney FROM Account WHERE usid = '{id}'";
        SendCMD(moneys, 'O');
        OnRecv += MarketClient_OnRecv;
    }

    private void MarketClient_OnRecv(List<string> arg)
    {
        string[] a = arg[0].Split(" ");
        if (a[0] != "null")
        {
            umoney = int.Parse(a[0]);
            InventoryManager.Instance.moneyText.text = umoney.ToString();
            if (MoneyRecv != null)
                MoneyRecv(umoney);
        }
        OnRecv -= MarketClient_OnRecv;
    }
}
