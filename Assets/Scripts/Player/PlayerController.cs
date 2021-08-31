using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int startMoney = 100, maxMoney = 200;
    float currentMoney;

    AnimationController anim;

    void Start()
    {
        currentMoney = startMoney;
        anim = GetComponent<AnimationController>();

        UIController.instance.SetSliderValue(currentMoney / maxMoney);
    }

    internal void AddMoneyAmount(int amount)
    {
        currentMoney += amount;
        UIController.instance.SetSliderValue(currentMoney / maxMoney);
    }

    internal bool RemoveMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            UIController.instance.SetSliderValue(currentMoney / maxMoney);

            return true;
        }
        return false;
    }

    internal bool GetHit(int amount)
    {
        if (anim != null)
            anim.GetHit();

        if (RemoveMoney(amount))
            return true;
        return false;
    }
}
