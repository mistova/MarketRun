using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;


    [SerializeField] Slider moneySlider;

    [SerializeField] GameObject startMenu, gameplayMenu, finishMenu;

    private void Start()
    {
        if (instance == null)
            instance = this;

        StartGame();
    }

    internal void SetSliderValue(float value)
    {
        moneySlider.value = value;
    }

    internal void StartGame()
    {
        startMenu.SetActive(false);
        gameplayMenu.SetActive(true);
    }

    internal void FinishGame()
    {
        gameplayMenu.SetActive(false);
        finishMenu.SetActive(true);
    }
}
