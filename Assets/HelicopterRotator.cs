using UnityEngine;

public class HelicopterRotator : MonoBehaviour
{
    private Transform bigSmiglo;
    private Transform smallSmiglo;
    [SerializeField] private float rotationSpeed;

    private void Start()
    {
        bigSmiglo = transform.GetChild(1);
        smallSmiglo = transform.GetChild(2);
    }

    private void Update()
    {
        bigSmiglo.transform.Rotate(0f,rotationSpeed,0f);
        smallSmiglo.transform.Rotate(rotationSpeed,0f,0f);
    }
}
