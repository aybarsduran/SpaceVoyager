using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PanelController : MonoBehaviour
{
    public TextMeshProUGUI maxScoreText;
    public GameObject menuPanel;
    public GameObject optionsPanel;
    public GameObject shipsPanel;
    public ShipSelection shipSelection;
    private void Start()
    {
        menuPanel.SetActive(true);
        maxScoreText.text = "record " + GameManager.Instance.GetMaxScore().ToString();
    }
    public void StartButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }
    public void ShipsButtonClicked()
    {
        menuPanel.SetActive(false);
        shipsPanel.SetActive(true);
    }
    public void ShipsBackButtonClicked()
    {
        shipSelection.Save();
        shipsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
    public void OptionsButtonClicked()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
     
    }
    public void OptionsBackButtonClicked()
    {
        optionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
   

    
}
