using UnityEngine;

public class StartForce : MonoBehaviour
{
    public Vector3 startForce;
    private bool hasStarted = false;
    private Rigidbody rigibody;

    void Awake()
    {
        rigibody = GetComponent<Rigidbody>();
        rigibody.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasStarted && other.CompareTag("HandGrabInteractable"))
        {
            hasStarted = true;
            rigibody.isKinematic = false;
            rigibody.AddForce(startForce, ForceMode.Impulse);
        }
    }
}