using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target; // Takip edilecek karakterin transformu
    [SerializeField] float smoothSpeed = 0.125f; // Kamera hareketinin yumuþaklýðý
    [SerializeField] Vector3 offset = new Vector3(0f, 0f, -5f); // Kamera ile karakter arasýndaki baþlangýç mesafesi

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}
