using UnityEngine;

public class ThiefController : MonoBehaviour
{
    [SerializeField] float speed = 1;

    [SerializeField] int stealAmount = 25;

    bool isTriggered;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 displacement = Vector3.right * speed;

        transform.position += displacement * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
            Steal(other.gameObject);
        }
    }

    private void Steal(GameObject gameObject)
    {
        gameObject.GetComponent<PlayerController>().GetHit(stealAmount);
        anim.SetTrigger("Steal");
        speed = 0;
    }
}
