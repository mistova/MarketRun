using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;


    [SerializeField] Slider moneySlider;

    [SerializeField] GameObject startMenu, gameplayMenu, winMenu, loseMenu;

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

    internal void FinishGame(bool isWinned)
    {
        gameplayMenu.SetActive(false);

        if (isWinned)
            winMenu.SetActive(true);
        else
            loseMenu.SetActive(true);
    }
}
