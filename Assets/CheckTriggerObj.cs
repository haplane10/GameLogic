using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTriggerObj : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            VendingMachineController.instance.isTouched = true;
        }
    }
}
