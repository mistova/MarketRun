using UnityEngine;

public class Collectible : MonoBehaviour
{
    Collider col;
    Rigidbody rb;

    bool isTriggered;

    [SerializeField] GameObject obj;

    [SerializeField] int price = 10;

    private void Start()
    {
        Instantiate(obj, transform.position, obj.transform.rotation).transform.parent = transform;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.gameObject.CompareTag("Player"))
        {
            isTriggered = true;

            if(Collect(other.gameObject))
                MakeFall(other.transform);
        }
    }

    private bool Collect(GameObject gameObject)
    {
        if (gameObject.GetComponent<PlayerController>().RemoveMoney(price))
            return true;
        return false;
    }

    private void LetItGo()
    {
        transform.parent = null;
    }

    private void MakeFall(Transform o)
    {
        col.isTrigger = false;
        rb.useGravity = true;
        GetComponent<MeshRenderer>().enabled = false;

        transform.parent = o;
        float x = transform.position.x - o.position.x;
        float r = Random.Range(-0.5f, 0.5f);
        if (x > 0.3)
            x = 0.3f;
        else if (x < -0.3)
            x = -0.3f;
        transform.position = new Vector3(o.position.x + x, transform.position.y, o.position.z + r);
    }
}
