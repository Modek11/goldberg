using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class Camerafollower : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToFollow;
    private CinemachineVirtualCamera cVirtualCamera;
    public static Camerafollower Instance { get; private set; }
    
    
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }

        StartCoroutine(Kurtyna());
    }

    private IEnumerator Kurtyna()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (!objectsToFollow[1].GetComponent<Rigidbody>().IsSleeping())
            {
                ChangeObject();
            }    
        }
        
    }


    private void ChangeObject()
    {
        //cVirtualCamera.Follow = objectsToFollow.First().transform;
        objectsToFollow.RemoveAt(0);
    }

    private void LateUpdate()
    {
        var destination = new Vector3(objectsToFollow.First().transform.position.x,
            objectsToFollow.First().transform.position.y + 10, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, destination, 0.1f);
    }

    public void ChangeObject(Transform transform)
    {
        objectsToFollow.Add(transform.gameObject);
        objectsToFollow.RemoveAt(0);
    }

}
