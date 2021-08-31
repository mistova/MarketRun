using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] int damage;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Hit(other.gameObject);
        }
    }

    private void Hit(GameObject gameObject)
    {
        gameObject.GetComponent<PlayerController>().GetHit(damage);

        if(anim != null)
            anim.SetTrigger("Crush");
    }
}
