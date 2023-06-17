using System;
using System.Collections;
using UnityEngine;

public class DestroyBank : MonoBehaviour
{
    [SerializeField] private Transform airConditioner;
    [SerializeField] private Transform lerpDestination;
    [SerializeField] private Transform cableRigTransform;
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float time;

    private bool destroyTriggered = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.body.CompareTag("Burgir"))
        {
            if (destroyTriggered) return;
            destroyTriggered = true;
            
            //Trigger wybuch banku
            Debug.Log("Burgir zaatakował");
            //Camerafollower.Instance.ChangeObject(collision.transform);
            StartCoroutine(KaBoom());

        }
    }

    private IEnumerator KaBoom()
    {
        var fire = Instantiate(firePrefab,cableRigTransform.GetChild(0).position, cableRigTransform.GetChild(0).rotation).transform;
        
        Camerafollower.Instance.AddObject(fire);
        
        for (int i = 1; i < 3; i++)
        {
            Transform destination = cableRigTransform.GetChild(i);
            while (Vector3.Distance(fire.position,destination.position) > 1f)
            {
                yield return new WaitForEndOfFrame();
                fire.position = Vector3.Lerp(fire.position, destination.position, 0.01f);
            } 
        }
        Camerafollower.Instance.RemoveObject(fire);
        Destroy(fire.gameObject);

        while (airConditioner.position != lerpDestination.position)
        {
            yield return new WaitForEndOfFrame();
            airConditioner.position = Vector3.Lerp(airConditioner.position, lerpDestination.position, time);
        }
    }
}
