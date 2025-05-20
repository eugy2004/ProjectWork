
using UnityEngine;
using UnityEngine.UI;

public class PanelCode : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button readyButton;
    public GameObject panel;        // Primo pannello da disattivare
    public GameObject secondPanel;  // Secondo pannello da attivare
    public GameObject mazzo;        // Mazzo da attivare
    public int maxTroops = 5;
    private int troopCount = 0;

    void Start()
    {
        button1.onClick.AddListener(() => PlaceTroop());
        button2.onClick.AddListener(() => PlaceTroop());
        button3.onClick.AddListener(() => PlaceTroop());
        readyButton.onClick.AddListener(() => Ready());

        // Disattiva il mazzo e il secondo pannello all'inizio
        mazzo.SetActive(false);
        secondPanel.SetActive(false);
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
            panel.SetActive(false);        // Nasconde il primo pannello
            secondPanel.SetActive(true);   // Mostra il secondo pannello
            mazzo.SetActive(true);         // Attiva il mazzo
            Debug.Log("Pronto! Primo pannello nascosto, secondo pannello e mazzo attivati.");
        }
        else
        {
            Debug.Log("Posiziona tutte le truppe prima di procedere.");
        }
    }
}
