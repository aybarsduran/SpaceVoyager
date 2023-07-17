using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePanelManager : MonoBehaviour
{
    public ShipController shipController;
    public GameObject pauseMenu;
    public TextMeshProUGUI recordPauseMenuText;
    public GameObject main;
    public GameObject pauseButton;
    public GameObject mainScore;
    private void Start()
    {
        pauseMenu.SetActive(false); 
    }
    public void PauseButtonClicked()
    {

        recordPauseMenuText.text = "record " + GameManager.Instance.GetMaxScore().ToString();
        main.SetActive(false);
        pauseButton.SetActive(false);
        mainScore.SetActive(false);
        Time.timeScale = 0f; 
        pauseMenu.SetActive(true);
        foreach (Transform planet in shipController.planets)
        {
            planet.gameObject.SetActive(false);
        }

    }
    public void PauseBackButtonClicked()
    {
        main.SetActive(true);
        pauseButton.SetActive(true);
        mainScore.SetActive(true);
        foreach (Transform planet in shipController.planets)
        {
            planet.gameObject.SetActive(true);
        }
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void MenuButtonClicked()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void RestartButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}
