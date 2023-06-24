using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class CraftManual : MonoBehaviour
{
    private bool isActivated = false;  // CraftManual UI 활성 상태
    private bool isPreviewActivated = false; // 미리 보기 활성화 상태

    [SerializeField]
    private GameObject go_BaseUI; // 기본 베이스 UI

    [SerializeField]
    private ItemAsset craft_fire;  // 불 탭에 있는 슬롯들.

    [SerializeField]
    private Tilemap tilemap; // 타일맵   

    private GameObject go_Preview; // 미리 보기 프리팹을 담을 변수
    private GameObject go_Prefab; // 실제 생성될 프리팹을 담을 변수 

    [SerializeField]
    private Transform tf_Player;  // 플레이어 위치

    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    private Vector3 prepos;
    public void SlotClick()
    {
        craft_fire = InventoryManager.Instance.GetSelectedItem(false);
        go_Preview = Instantiate(craft_fire.go_PreviewPrefab);
        go_Prefab = craft_fire.go_prefab;
        go_Preview.SetActive(false);
        isPreviewActivated = true;
        go_BaseUI.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
        {
            craft_fire = InventoryManager.Instance.GetSelectedItem(false);
            if(craft_fire != null)
                Window();
        }

        if (isPreviewActivated)
        {
            PreviewPositionUpdate();
            if (Input.GetButtonDown("Fire1")) 
                Build();
        }

        if (go_Preview != null)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3Int cellPos = tilemap.WorldToCell(worldPos);
            //Debug.Log(cellPos);
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cellPos);
            //Debug.Log(cellCenterPos);
            cellCenterPos.z = 0;
            go_Preview.transform.position = cellCenterPos;
            go_Preview.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Cancel();
    }        

    private void PreviewPositionUpdate()
    {        
        // 마우스 위치로부터 레이를 쏘아 충돌 검사
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Raycast");
            // 미리보기 오브젝트 위치 이동
            go_Preview.transform.position = hit.point;
            prepos = go_Preview.transform.position;
            prepos.z = 0;
            go_Prefab.transform.position = prepos;
        }
    }

    private void Build()
    {
        //if (isPreviewActivated && go_Preview.GetComponent<PreviewObject>().isBuildable())
        //{
        //    Debug.Log("Build");
        //    Instantiate(go_Prefab, hitInfo.point, Quaternion.identity);                        

        //    Destroy(go_Preview);
        //    isActivated = false;
        //    isPreviewActivated = false;
        //    go_Preview = null;
        //    go_Prefab = null;
        //}

        //Vector3 prepos = go_Preview.transform.position;
        //prepos.z = 0;
        //go_Prefab.transform.position = prepos;        
        if (go_Preview != null)
        {                       
            //go_Preview.GetComponent<PreviewObject>() != null && go_Preview.GetComponent<PreviewObject>().isBuildable()
            if (isPreviewActivated && go_Preview.GetComponent<PreviewObject>().isBuildable())
            {
                foreach(GameObject pos in TurnManager.instance.builded)
                {
                    if(pos.transform.position == go_Preview.transform.position)
                    {
                        Debug.Log("이미 있음");
                        return;
                    }
                }
                // 저장된 위치 정보를 사용하여 프리팹 생성
                InventoryManager.Instance.GetSelectedItem(true);
                //TurnManager.instance.builded.Add(go_Preview.transform.position);
                GameObject obj = Instantiate(go_Prefab, go_Preview.transform.position, Quaternion.identity);
                TurnManager.instance.builded.Add(obj);
                //Debug.Log($"go_Preview - {go_Preview.transform.position}");
                Destroy(go_Preview);
                isActivated = false;
                isPreviewActivated = false;
                go_Preview = null;
                go_Prefab = null;

            }
        }
    }

    private void Window()
    {
        if (!isActivated)
            OpenWindow();
        else
            CloseWindow();
    }

    private void OpenWindow()
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    private void CloseWindow()
    {
        isActivated = false;
        go_BaseUI.SetActive(false);
    }

    private void Cancel()
    {
        if (isPreviewActivated)
            Destroy(go_Preview);

        isActivated = false;
        isPreviewActivated = false;

        go_Preview = null;
        go_Prefab = null;

        go_BaseUI.SetActive(false);
    }
}