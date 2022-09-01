using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BlockEvent : MonoBehaviour, IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    ThreeMatchGameController threeMatchGC;

    Rigidbody2D rigid;
    Image img;

    public float distance;

    public bool isMoving = true;

    public LinkedBlock h_BlockList;// = new LinkedBlock();
    public LinkedBlock v_BlockList;// = new LinkedBlock();

    // Start is called before the first frame update
    void Start()
    {
        rigid = transform.GetComponent<Rigidbody2D>();
        img = transform.GetComponent<Image>();
        threeMatchGC = GameObject.Find("Controller").GetComponent<ThreeMatchGameController>();
    }


    void Update()
    {
        

        if(isMoving == false)
        {
            if (threeMatchGC.isShootRay)
            {
                RaycastBlock();
            }
        }

    }

    
    void InitializeLinkedBlocks()
    {
        //블럭이 움직일때
        h_BlockList.preBlock = null;
        h_BlockList.postBlock = null;
        v_BlockList.preBlock = null;
        v_BlockList.postBlock = null;
    }

   

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag(PointerEventData eventData)");
        
        img.raycastTarget = false;
        //Debug.Log("클릭 : " + eventData.pointerEnter.transform.position);
        threeMatchGC.PressBlockPos = eventData.pointerEnter.transform.position;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag(PointerEventData eventData)" + rigid.bodyType);
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,0);
    
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop(PointerEventData eventData)" + rigid.bodyType + " | " + GetComponent<Image>().sprite.name);
        //Debug.Log(eventData.pointerEnter.GetComponent<Image>().sprite.name);
    
        threeMatchGC.ReleaseBlockPos = eventData.pointerEnter.transform.position;
        transform.position = threeMatchGC.PressBlockPos;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag(PointerEventData eventData)" + rigid.bodyType);
        
        img.raycastTarget = true;
        transform.position = threeMatchGC.ReleaseBlockPos;
    
        
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter(PointerEventData eventData)" + rigid.bodyType + " | " + GetComponent<Image>().sprite.name);
    }



    void RaycastBlock()
    {
        RaycastHit2D[] hitsRight = Physics2D.RaycastAll(transform.position, Vector2.right, distance);
        RaycastHit2D[] hitsLeft = Physics2D.RaycastAll(transform.position, Vector2.left, distance);
        RaycastHit2D[] hitsUp = Physics2D.RaycastAll(transform.position, Vector2.up, distance);
        RaycastHit2D[] hitsDown = Physics2D.RaycastAll(transform.position, Vector2.down, distance);

        Debug.DrawRay(transform.position, Vector2.right * distance, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * distance, Color.red);
        Debug.DrawRay(transform.position, Vector2.up * distance, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);

        //수평
        for (int i = 0; i < hitsRight.Length; i++)
        {
            if (hitsRight[i].collider.gameObject != transform.gameObject)
            {
                GameObject go = hitsRight[i].collider.gameObject;
                
                if(go.name == transform.name)
                {
                    h_BlockList.postBlock = go;
                }
            }
        }

        for (int i = 0; i < hitsLeft.Length; i++)
        {
            if (hitsLeft[i].collider.gameObject != transform.gameObject)
            {
                //h_BlockList.preBlock = hitsLeft[i].collider.gameObject;
                GameObject go = hitsLeft[i].collider.gameObject;
                if (go.name == transform.name)
                {
                    h_BlockList.preBlock = go;
                }
            }
        }

        //수직
        for (int i = 0; i < hitsUp.Length; i++)
        {
            if (hitsUp[i].collider.gameObject != transform.gameObject)
            {
                //v_BlockList.postBlock = hitsUp[i].collider.gameObject;
                GameObject go = hitsUp[i].collider.gameObject;
                if (go.name == transform.name)
                {
                    v_BlockList.postBlock = go;
                }
            }
        }

        for (int i = 0; i < hitsDown.Length; i++)
        {
            if (hitsDown[i].collider.gameObject != transform.gameObject)
            {
                //v_BlockList.preBlock = hitsDown[i].collider.gameObject;
                GameObject go = hitsDown[i].collider.gameObject;
                if (go.name == transform.name)
                {
                    v_BlockList.preBlock = go;
                }
            }
        }

        if(h_BlockList.preBlock && h_BlockList.postBlock)
        {
            BlockManager.instance.CrushedBlockList.Add(h_BlockList);
        }

        if (v_BlockList.preBlock && v_BlockList.postBlock)
        {
            BlockManager.instance.CrushedBlockList.Add(v_BlockList);
        }

    }

}
