using UnityEngine;

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
