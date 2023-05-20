using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private bool rotateLeft;
    void Update()
    {
        if (rotateLeft)
        {
            transform.Rotate(new Vector3(0,0,0.5f));
        }
        else
        {
            transform.Rotate(new Vector3(0,0,-0.5f));
        }
    }
}
