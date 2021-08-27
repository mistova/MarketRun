using UnityEngine;

public class Collectible : MonoBehaviour
{
    Collider col;
    Rigidbody rb;

    Transform parent;

    [SerializeField] GameObject obj;

    private void Start()
    {
        Instantiate(obj, transform.position, obj.transform.rotation).transform.parent = transform;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MakeFall(other.transform);
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LetItGo();
        }
    }*/

    private void LetItGo()
    {
        transform.parent = null;
    }

    private void MakeFall(Transform o)
    {
        col.isTrigger = false;
        rb.useGravity = true;
        GetComponent<MeshRenderer>().enabled = false;
        //Debug.Log(transform.parent);
        transform.parent = o;
        float r = Random.Range(-0.25f, 0.25f);
        transform.position = new Vector3(o.position.x + r, transform.position.y, o.position.z + r);
        //parent = o;
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cart") || collision.gameObject.CompareTag("Collectible"))
        {
            AddToCart(collision.gameObject);
        }
    }*/

    private void AddToCart(GameObject gO)
    {
        transform.parent = parent;
    }
}
