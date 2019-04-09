using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject qbit;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - qbit.transform.position;
    }

    void LateUpdate()
    {
        transform.position = qbit.transform.position + offset;
    }
}