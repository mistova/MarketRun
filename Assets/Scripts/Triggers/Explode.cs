using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] float force = 10;

    Rigidbody[] rb;

    void Start()
    {
        rb = GetComponentsInChildren<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Push(other.gameObject.transform.position);
        }
    }

    private void Push(Vector3 position)
    {
        foreach(Rigidbody r in rb)
        {
            r.AddForce((r.transform.position - position).normalized * force, ForceMode.Impulse);
        }
    }
}
