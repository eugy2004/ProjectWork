using UnityEngine;
using UnityEngine.UI;

public class PanelCode : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button readyButton;
    public GameObject panel;
    public int maxTroops = 5;
    private int troopCount = 0;

    void Start()
    {
        button1.onClick.AddListener(() => PlaceTroop());
        button2.onClick.AddListener(() => PlaceTroop());
        button3.onClick.AddListener(() => PlaceTroop());
        readyButton.onClick.AddListener(() => Ready());
    }

    void PlaceTroop()
    {
        if (troopCount < maxTroops)
        {
            troopCount++;
            // Codice per posizionare la truppa nel campo
            Debug.Log("Truppa posizionata. Totale truppe: " + troopCount);
        }
        else
        {
            Debug.Log("Numero massimo di truppe raggiunto.");
        }
    }

    void Ready()
    {
        if (troopCount == maxTroops)
        {
            panel.SetActive(false);
            // Codice per passare alla schermata di combattimento
            Debug.Log("Passaggio alla schermata di combattimento.");
        }
        else
        {
            Debug.Log("Posiziona tutte le truppe prima di procedere.");
        }
    }
}
