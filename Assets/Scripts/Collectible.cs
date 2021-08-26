using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    Collider col;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MakeFall();
        }
    }

    private void MakeFall()
    {
        col.isTrigger = false;
        rb.useGravity = true;
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cart") || collision.gameObject.CompareTag("Collectible"))
        {
            AddToCart(collision.gameObject);
        }
    }

    private void AddToCart(GameObject gO)
    {
        transform.parent = gO.transform;
    }
}
