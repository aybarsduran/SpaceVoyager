using UnityEngine;

public class GamePanelManager : MonoBehaviour
{

    public GameObject pauseMenu; 

    public void PauseButtonClicked()
    {
        Time.timeScale = 0f; 
        pauseMenu.SetActive(true); 
    }
    public void PauseBackButtonClicked()
    {
       
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
