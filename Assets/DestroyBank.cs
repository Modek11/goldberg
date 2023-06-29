using System;
using System.Collections;
using UnityEngine;

public class DestroyBank : MonoBehaviour
{
    [SerializeField] private Transform airConditioner;
    [SerializeField] private Transform lerpDestination;
    [SerializeField] private Transform cableRigTransform;
    [SerializeField] private Transform cableBone;
    [SerializeField] private Transform boneDestination;
    [Space]
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
        var x = transform.position.x;
        var y = transform.position.y;
        var z = transform.position.z;
        var signFire = Instantiate(firePrefab,transform.position, transform.rotation,transform.parent);
        signFire.transform.localScale = Vector3.one * 2f;
        signFire.transform.position = new Vector3(x+.9f, y, z);
        
        yield return new WaitForSeconds(1f);
        
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
            cableBone.position = Vector3.Lerp(cableBone.position, boneDestination.position, time);
        }
    }
}
