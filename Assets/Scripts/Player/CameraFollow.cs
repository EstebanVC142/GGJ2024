using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform cameraPosition;
    [SerializeField]
    private Vector3 cameraOffset;
    [SerializeField]
    private float followTime = 1f;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, cameraPosition.position + cameraOffset, followTime * Time.deltaTime);
        //transform.rotation = Quaternion.Lerp(transform.rotation, cameraPosition.rotation, followTime * Time.deltaTime);
    }
}
