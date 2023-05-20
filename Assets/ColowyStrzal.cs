using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColowyStrzal : MonoBehaviour
{
    [SerializeField] private GameObject cokeCan;
    [SerializeField] private Transform startingPlace;
    [SerializeField] private float force;
    private bool destroyTriggered = false;

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.body.CompareTag("Burgir"))
        {
            if (destroyTriggered) return;
            destroyTriggered = true;
            
            Debug.Log("Burgir zaatakował");
            StartCoroutine(Shoot());

        }
    }

    private IEnumerator Shoot()
    {
        var cokeCanShooted = Instantiate(cokeCan,startingPlace.position,Quaternion.identity);
        cokeCanShooted.GetComponent<Rigidbody>().AddForce(new Vector3(-force,0,0),ForceMode.Impulse);
        yield return null;
    }
}
