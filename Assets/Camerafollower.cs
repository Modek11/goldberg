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
    private float fovAtStart = 40;
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

        fovAtStart = GetComponent<Camera>().fieldOfView;
        StartCoroutine(Kurtyna());
    }

    private IEnumerator Kurtyna()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            yield return new WaitForEndOfFrame();
            var rb = objectsToFollow[1].GetComponent<Rigidbody>();
            if (!rb.IsSleeping() && rb.isKinematic == false)
            {
                ChangeObject();
            }

            if (rb == null)
            {
                ChangeObject();
            }
        }
        
    }


    public void ChangeObject()
    {
        //cVirtualCamera.Follow = objectsToFollow.First().transform;
        objectsToFollow.RemoveAt(0);
    }

    private void LateUpdate()
    {
        Vector3 destination;
        if (objectsToFollow[0].name == "Can_drink(Clone)")
        {
            destination = new Vector3(objectsToFollow.First().transform.position.x,
                objectsToFollow.First().transform.position.y + 3.5f, transform.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(10,0,0), 0.1f);
        }
        else
        {
            destination = new Vector3(objectsToFollow.First().transform.position.x,
                objectsToFollow.First().transform.position.y + 12, transform.position.z);
        }
        
        transform.position = Vector3.Slerp(transform.position, destination, 0.15f);
    }

    public void ChangeObject(Transform transform)
    {
        objectsToFollow.Add(transform.gameObject);
        objectsToFollow.RemoveAt(0);
    }

    public void AddObject(Transform transform)
    {
        objectsToFollow.Insert(1,transform.gameObject);
        objectsToFollow.RemoveAt(0);
    }
    
    public void RemoveObject(Transform transform)
    {
        objectsToFollow.Remove(transform.gameObject);
    }

    public void BringCloser(float number)
    {
        StartCoroutine(BringCloser2(number));
    }

    private IEnumerator BringCloser2(float number)
    {
        var camera = GetComponent<Camera>();
        
        while(Math.Abs(camera.fieldOfView - number) > 0.01f)
        {
            camera.fieldOfView = Mathf.SmoothStep(camera.fieldOfView, number, 0.1f);
            yield return new WaitForFixedUpdate();
        }
    }

    public void GetBackWider()
    {
        StartCoroutine(GetBackWider2());
    }

    private IEnumerator GetBackWider2()
    {
        var camera = GetComponent<Camera>();
        
        while(Math.Abs(camera.fieldOfView - fovAtStart) > 0.01f)
        {
            camera.fieldOfView = Mathf.SmoothStep(camera.fieldOfView, fovAtStart, 0.2f);
            yield return new WaitForFixedUpdate();
        }
    }
    
}
