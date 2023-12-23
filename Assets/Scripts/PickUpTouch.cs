using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTouch : MonoBehaviour
{
    [SerializeField] PickUpSystem pickUpSystem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Main"))
            pickUpSystem.ColliderTouchStart();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Main"))
            pickUpSystem.ColliderTouchStop();
    }
}
