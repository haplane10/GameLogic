using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{

    public float lineWidth;
    Ray ray;
    RaycastHit hit;
    LineRenderer lineRenderer;

    private float xRotate, yRotate, xRotateMove, yRotateMove;
    public float rotateSpeed = 500.0f;
    GameObject HitObj;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        ShootRay();
    }


    void ShootRay()
    {
        ray = Camera.main.ViewportPointToRay(transform.position);

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        ray.direction = transform.forward;

        if(Physics.Raycast(ray, out hit))
        {
            HitObj = hit.transform.gameObject;
            //Debug.Log(HitObj);
            if(Input.GetMouseButtonUp(0))
            {
                VendingMachineController.instance.CheckItems(HitObj);
            }
            lineRenderer.SetPosition(1, hit.point);
        }
    }


    void Rotate()
    {
        if (Input.GetMouseButton(1)) // 클릭한 경우
        {
            xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed; ;
            yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed; ;

            yRotate = yRotate + yRotateMove;
            xRotate = xRotate + xRotateMove;

            xRotate = Mathf.Clamp(xRotate, -90, 90); // 위, 아래 고정

            Quaternion quat = Quaternion.Euler(new Vector3(xRotate, yRotate, 0));
            //transform.rotation = Quaternion.Slerp(transform.rotation, quat, Time.deltaTime);
            transform.rotation = quat;
        }
    }



    


}
