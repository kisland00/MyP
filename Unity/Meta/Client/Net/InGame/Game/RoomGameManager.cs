using JEEWOO.NET;
using FreeNet;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomGameManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> playerList = new List<GameObject>();
    public GameObject cameraPref;
    public GameObject cameraPosPref;
    public HUD hud;
    private static RoomGameManager _instance = null;
    public GameObject gameEndPannel;
    public TMP_Text gameEndText;
    Transform startPoint;
    public static RoomGameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Start()
    {
        _instance = this;
        startPoint = GameObject.Find("StartPos").GetComponent<Transform>();
        //GameObject.Instantiate(player, startPoint.position, startPoint.rotation);

        CreateLocalPlayer();
        SendReqSpawnedPlayer();
    }


    void CreateLocalPlayer()
    {
        GameObject localPlayer = GameObject.Instantiate(player, startPoint.position, startPoint.rotation);

        string user_id = CProcessPacket.Instance.USER_ID;
        localPlayer.AddComponent<CPlayerInfo>().USER_ID = user_id;
        ShootingInfo info = localPlayer.AddComponent<ShootingInfo>();
        localPlayer.GetComponent<CPlayerInfo>().IS_MINE = true;
        localPlayer.GetComponent<GunController>().hud = this.hud;
        GameObject cameraObj = Instantiate(cameraPref, localPlayer.transform);
        localPlayer.GetComponent<SP_Move>().theCamera = cameraObj;
        ActionController ac = cameraObj.GetComponent<ActionController>();
        hud.theGunController = localPlayer.GetComponent<GunController>();
        hud.theGunController.theCam = cameraObj;
        hud.hpInfo = info;
        // Player List�� ���
        playerList.Add(localPlayer);
    }

    // ������ Local Player ���������� ����
    void SendReqSpawnedPlayer()
    {
        CPacket msg = CPacket.create((short)PROTOCOL.SPAWN_PLAYER_REQ);
        string user_id = CProcessPacket.Instance.USER_ID;
        msg.push(user_id);

        CNetworkManager.Instance.send(msg);
    }

    // �ٸ� ����� Local Player�� Remote Player�� �� ������ ����
    public void CreateFriendPlayer(CPacket msg)
    {
        short count = msg.pop_int16();

        for (short i = 0; i < count; i++)
        {
            string user_id = msg.pop_string();
            bool isCreate = true;
            foreach (var player in playerList)
            {
                // �̹� ���� Player�� �����Ǿ� �����Ѵ�.
                if (player.GetComponent<CPlayerInfo>().USER_ID == user_id)
                    isCreate = false;
            }
            // Player�� �������� ���� ���� ����
            if (isCreate)
            {
                Vector3 pos = startPoint.position;
                Quaternion rot = startPoint.rotation;
                GameObject playerObj = Instantiate(player, pos, rot);
                playerObj.AddComponent<CPlayerInfo>().USER_ID = user_id;
                playerObj.AddComponent<ShootingInfo>();
                playerObj.GetComponent<CPlayerInfo>().IS_MINE = false;

                // Player List�� ���
                playerList.Add(playerObj);
            }
        }
    }

    public void GameEnd(string id)
    {
        gameEndPannel.SetActive(true);
        gameEndText.text = id + "Win!!";
    }
}