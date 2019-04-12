using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelController : MonoBehaviour {

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
