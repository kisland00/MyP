using JEEWOO.NET;
using FreeNet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMovement : MonoBehaviour
{
    CProcessPacket processPacket;

    private Animator animator;
    private CharacterController controller;
    private new Transform transform;


    CPlayerInfo playerInfo;

    // ���� ũ�� �̻��� ��ȭ�� ���� ��Ŷ�� �����ϱ� ���ؼ�(������ �������� ��Ŷ �����Ϸ���)
    Vector3 prePosition = Vector3.zero; // ���� �������� ��ġ����
    Vector3 preRotator = Vector3.zero;  // ���� �������� ȸ������

    /*������ ��/������ Transform����*/
    Vector3 nowMoveDir = Vector3.zero;   // ���� controller.SimpleMove�� �̵��ϴ� ���Ͱ� 
    Vector3 nowPosition = Vector3.zero;  // ���� �������� ��ġ����
    Vector3 nowRotator = Vector3.zero;  // ���� �������� ȸ������
    void Start()
    {
        processPacket = CProcessPacket.Instance;
        controller = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();

        playerInfo = GetComponent<CPlayerInfo>();

        CProcessPacket.Instance.OnDispatchTransform += OnRecvTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInfo.IS_MINE)
        {
            SendReqTransformPlayer();
        }
    }

    public void SendReqTransformPlayer()
    {
        nowPosition = transform.position;
        nowRotator = transform.eulerAngles;

        // �̵�ũ�⳪ ȸ�������� �������� �̸��� ���� ��Ŷ �������� �ʴ´�.
        if ((Vector3.Distance(nowPosition, prePosition) < 0.05f) &&
            (Mathf.Abs(nowRotator.y - preRotator.y) < 5.0f))
            return;

        CPacket msg = CPacket.create((short)PROTOCOL.TRANSFORM_PLAYER_REQ);

        msg.push(playerInfo.USER_ID);
        // controller.SimpleMove�� �̵��ؾ� �ϴ� ��
        msg.push(nowMoveDir.x);
        msg.push(nowMoveDir.y);
        msg.push(nowMoveDir.z);
        // transform.position
        msg.push(nowPosition.x);
        msg.push(nowPosition.y);
        msg.push(nowPosition.z);
        // transform.eulerAngles
        msg.push(nowRotator.x);
        msg.push(nowRotator.y);
        msg.push(nowRotator.z);
        // animator ����
        msg.push(animator.GetInteger("aniStep"));

        CNetworkManager.Instance.send(msg);

        prePosition = nowPosition;
        preRotator = nowRotator;
    }

    void OnRecvTransform(string uid, Vector3 moveDir, Vector3 pos, Vector3 euler, int aniState)
    {
        // ���� �����̴� �÷��̾ �ƴϰ�
        // USER_ID�� ��ġ�ϴ� �÷��̾ �� Transform�� �����ض�
        if (!playerInfo.IS_MINE && uid == playerInfo.USER_ID)
        {
            controller.enabled = false;
            //if (controller.enabled)
            //    controller.SimpleMove(moveDir);
            transform.position = pos;
            transform.eulerAngles = euler;
            controller.enabled = true;
            animator.SetInteger("aniStep", aniState);
            //animator.SetFloat("Forward", forward);
            //animator.SetFloat("Strafe", strafe);
        }
    }

    private void OnDestroy()
    {
        CProcessPacket.Instance.OnDispatchTransform -= OnRecvTransform;
    }
}
