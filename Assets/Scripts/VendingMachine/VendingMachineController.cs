using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VendingMachineController : MonoBehaviour
{
    public static VendingMachineController instance;
    public GameObject vendingMachineLight;
    public TMP_Text displayNum;

    public GameObject snackTrays;
    Transform[] trayChildrens;

    public Transform touchPoint;
    BoxCollider touchPointBC;
    public float moveSpeed;
    public float rotSpeed;
    public float drag;


    int intNumber;
    public bool isTouched = false; 

    private void Awake()
    {
        instance = this;
    }

    List<Transform> trayList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        trayChildrens = snackTrays.GetComponentsInChildren<Transform>();
        touchPointBC = touchPoint.GetComponent<BoxCollider>();

        foreach (var item in trayChildrens)
        {
            if(item.CompareTag("Tray"))
            {
                trayList.Add(item);
            }
        }

    }

 

    public void CheckItems(GameObject item)
    {
        //1. 동전 넣기
        if(item.CompareTag("Coin"))
        {
            vendingMachineLight.SetActive(true);
        }

        //2. 번호 누르기
        if(item.CompareTag("Number"))
        {
            if(displayNum.text == "00")
            {
                displayNum.text = "";
            }

            
                CheckNumber(item, item.name[item.name.Length - 1].ToString());
                //CheckNumber(item, "0");
                //CheckNumber(item, "1");
                //CheckNumber(item, "2");
                //CheckNumber(item, "3");
                //CheckNumber(item, "4");
                //CheckNumber(item, "5");
                //CheckNumber(item, "6");
                //CheckNumber(item, "7");
                //CheckNumber(item, "8");
                //CheckNumber(item, "9");
            
            //CheckNumber(item, "*");
            //CheckNumber(item, "C");
        }
        
        if(item.CompareTag("Hatch"))
        {
            StartCoroutine(HatchAnimation(item));
        }

    }

    void CheckNumber(GameObject item , string number)
    {
        if(item.name.Contains(number))
        {
            displayNum.color = Color.white;
            //입력
            if (number == "*")
            {
                displayNum.color = Color.green;
                intNumber = int.Parse(displayNum.text);
                //3. 해당 번호 아이템 출력
                DispenseItems(intNumber);

                //NumberPlate Max
                if (intNumber > 34)
                {
                    displayNum.color = Color.red;
                }
            }
            //초기화
            else if(number == "C")
            {
                displayNum.color = Color.white;
                displayNum.text = "00";
            }
            else
            {
                if (displayNum.text.Length < 2)
                {
                    displayNum.text += number;
                }
            }
        }
    }

    void DispenseItems(int number)
    {
        int traynum = number - 1;

        for(int i=0; i<trayList.Count; i++)
        {
            if(i == traynum)
            {
                //수정
                GameObject backStick = trayList[i].GetChild(0).gameObject;
                StartCoroutine(PushItems(backStick));
                //StartCoroutine(ItemAnimation(item));
            }
        }

    }

    IEnumerator HatchAnimation(GameObject hatch)
    {
        //50도 까지만 회전
        while(hatch.transform.localEulerAngles.x < 50f)
        {
            hatch.transform.Rotate(Vector3.right, rotSpeed * Time.deltaTime);
            yield return null;
        }

    }


    IEnumerator ItemAnimation(GameObject item)
    {
        while(item.transform.position.z > touchPoint.position.z)
        {
            item.transform.position -= Vector3.forward * moveSpeed * Time.deltaTime;


            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        //Rigidbody rigid = item.AddComponent<Rigidbody>();
        //rigid.drag = drag;

    }

    IEnumerator PushItems(GameObject BackStick)
    {
        while(!isTouched)
        {
            BackStick.transform.position -= Vector3.forward * moveSpeed * Time.deltaTime;

            yield return null;
        }

        isTouched = false;
       
    }



}
