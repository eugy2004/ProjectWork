using UnityEngine;
using UnityEngine.UI;

public class PanelCode : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button readyButton;
    public GameObject panel;
    public GameObject mazzo; // Il mazzo da attivare
    public int maxTroops = 5;
    private int troopCount = 0;

    void Start()
    {
        button1.onClick.AddListener(() => PlaceTroop());
        button2.onClick.AddListener(() => PlaceTroop());
        button3.onClick.AddListener(() => PlaceTroop());
        readyButton.onClick.AddListener(() => Ready());

        // Assicurati che il mazzo sia disattivato all'inizio
        mazzo.SetActive(false);
    }

    void PlaceTroop()
    {
        if (troopCount < maxTroops)
        {
            troopCount++;
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
            mazzo.SetActive(true); // Attiva il mazzo quando si preme il bottone "Ready"
            Debug.Log("Passaggio alla schermata di combattimento e mazzo attivato.");
        }
        else
        {
            Debug.Log("Posiziona tutte le truppe prima di procedere.");
        }
    }
}