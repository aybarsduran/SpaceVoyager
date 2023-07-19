using UnityEngine;

public class ShipSelection : MonoBehaviour
{
    public GameObject[] shipHolders; // Gemi objelerini tutan dizi
    [SerializeField]private int currentShipIndex = 0; // Mevcut gemi indeksi

    private void Start()
    {
        if (!PlayerPrefs.HasKey("selectedShip"))
        {
            PlayerPrefs.SetInt("selectedShip", 0);
            ActivateShip(Load());
        }
        else
        {
            ActivateShip(Load());
        }
    }

    public void RightButtonPressed()
    {
        // Mevcut gemiyi devre dýþý býrakýn
        DeactivateCurrentShip();

        // Bir sonraki gemiye geçin
        currentShipIndex++;
        if (currentShipIndex >= shipHolders.Length)
        {
            currentShipIndex = 0;
        }

        // Yeni gemiyi etkinleþtirin
        ActivateShip(currentShipIndex);
    }
    public void LeftButtonPressed()
    {
        DeactivateCurrentShip();
        if (currentShipIndex == 0)
        {
            currentShipIndex = 5;
        }
        currentShipIndex--;
       
        ActivateShip(currentShipIndex);
    }

    private void ActivateShip(int index)
    {
        shipHolders[index].SetActive(true);
    }

    private void DeactivateCurrentShip()
    {
        shipHolders[currentShipIndex].SetActive(false);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("selectedShip", currentShipIndex); //0 ise ship1, 1 ise ship2, 2ise ship3, 3 ise ship4, 4ise ship5
    }
    private int Load()
    {
        return PlayerPrefs.GetInt("selectedShip");
    }
}
