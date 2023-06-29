using System.Collections;
using Unity.VisualScripting;
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
        StartCoroutine(PressButton());
        yield return new WaitForSeconds(.2f);
        
        Camerafollower.Instance.BringCloser(10);
        
        var cokeCanShooted = Instantiate(cokeCan,startingPlace.position,Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        
        
        cokeCanShooted.GetComponent<Rigidbody>().isKinematic = false;
        cokeCanShooted.GetComponent<Rigidbody>().AddForce(new Vector3(-force,0,0),ForceMode.Impulse);
        yield return new WaitForSeconds(.7f);
        Camerafollower.Instance.GetBackWider();
        
        yield return null;
    }

    private IEnumerator PressButton()
    {
        var tran = transform;
        //var tran = transform.GetChild(3).transform;
        var destination = new Vector3(0.659f, 1.775f, -0.288f);
        while(tran.localPosition != destination)
        {
            yield return new WaitForFixedUpdate();
            tran.localPosition = Vector3.Lerp(tran.localPosition, destination, 0.05f);
        }
    }
    
}
