using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerBoomEnd : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    [Space]
    [SerializeField] private float power;
    private float radius = 10000;
    
    private bool destroyTriggered = false;
    
    private void Awake()
    {
        Camerafollower.Instance.ChangeObject(transform);
    }
        
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.body.CompareTag("Puszka"))
        {
            if (destroyTriggered) return;
            destroyTriggered = true;
            
            Debug.Log("Puszka Boom");
            StartCoroutine(Boom());

        }
    }

    private IEnumerator Boom()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
        
        var firee = Instantiate(fire,transform.position,transform.rotation);
        
        Camerafollower.Instance.ChangeObject();

        yield return null;
    }
}
