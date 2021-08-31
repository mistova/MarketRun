using UnityEngine;

public class CashRetriver : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] int money = 10;

    [SerializeField] GameObject particle;

    private void Start()
    {
        Instantiate(obj, transform.position, obj.transform.rotation).transform.parent = transform.GetChild(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject gameObject)
    {
        gameObject.GetComponent<PlayerController>().AddMoneyAmount(money);
        Destroy(Instantiate(particle, transform.position, transform.rotation), 5);
        Destroy(this.gameObject);
    }
}
