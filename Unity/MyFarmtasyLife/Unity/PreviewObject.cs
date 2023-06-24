using IsoTools.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PreviewObject : MonoBehaviour
{
    private List<Collider> colliderList = new List<Collider>(); // 충돌한 오브젝트들 저장할 리스트

    [SerializeField]
    private int layerGround; // 지형 레이어 (무시하게 할 것)
    private const int IGNORE_RAYCAST_LAYER = 2;  // ignore_raycast (무시하게 할 것)

    [SerializeField]
    private Tilemap Tilemap;

    [SerializeField]
    private Material green;
    [SerializeField]
    private Material red;

    bool canbuild = true;

    void Update()
    {
        do_check();
        ChangeColor();
    }
  

    private void do_check()
    {
        // 마우스 위치를 2D 월드 좌표로 변환
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 충돌 검사를 수행할 레이어 마스크
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("PreView"));

        // 2D 충돌 검사 수행
        RaycastHit2D hit2d = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, layerMask);

        // 충돌한 오브젝트가 있다면
        if (hit2d.collider != null)
        {
            //Debug.Log($"Hit object: {hit2d.collider.gameObject.name}");

            // 충돌한 오브젝트
            if (hit2d.transform.gameObject == gameObject)
            //if (hit2d.transform.gameObject != Tilemap.CompareTag("Tilemap"))
            {
                canbuild = false;
                //Debug.Log($"PreviewObject - Can build: {canbuild}");
                   
            } 
            else
            {
                canbuild = true;
                //Debug.Log($"PreviewObject - Can build: {canbuild}");
                
            }
        }
        else if (hit2d.collider == null) 
        {
            canbuild = false;
            //Debug.Log($"Can build: {canbuild}");
        }
    }    
    private void ChangeColor()
    {
        if (!canbuild)
        {
            SetColor(red);
        }            
        else
        {
            canbuild = true;
            SetColor(green);
        }
            
    }

    private void SetColor(Material mat)
    {
        foreach (Transform tf_Child in this.transform)
        {
            Material[] newMaterials = new Material[tf_Child.GetComponent<Renderer>().materials.Length];

            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = mat;
            }

            tf_Child.GetComponent<Renderer>().materials = newMaterials;
        }
    }

    public bool isBuildable()
    {
        if(canbuild)
            return true;
        else
        return false;
    }   
    
}
