using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EndGame(other.gameObject);
        }
    }

    private void EndGame(GameObject gameObject)
    {
        gameObject.GetComponent<Movement>().Finished();

        UIController.instance.FinishGame(true);
    }
}
