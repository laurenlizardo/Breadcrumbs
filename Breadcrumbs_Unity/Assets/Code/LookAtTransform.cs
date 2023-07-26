using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LookAtTransform : MonoBehaviour
{
    public Transform TransformToLookAt;
    public GameObject[] GameObjectsToRotate;

    private void Start()
    {
        foreach (GameObject go in GameObjectsToRotate)
        {
            go.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    
    void Update()
    {
        transform.LookAt(TransformToLookAt);
    }
}
