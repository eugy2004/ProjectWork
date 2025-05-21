using UnityEngine;
using UnityEngine.UI;

public class PanelCode : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button readyButton;
    public GameObject panel;
    public GameObject secondPanel;
    public GameObject mazzo;
    public int maxTroops = 5;
    public int troopCountPlayer1 = 0;  // Contatore truppe per il primo giocatore
    public int troopCountPlayer2 = 0;  // Contatore truppe per il secondo giocatore
    private int currentPlayer = 1;      // Indica il turno

    void Start()
    {
        button1.onClick.AddListener(() => PlaceTroop());
        button2.onClick.AddListener(() => PlaceTroop());
        button3.onClick.AddListener(() => PlaceTroop());
        readyButton.onClick.AddListener(() => Ready());

        mazzo.SetActive(false);
        secondPanel.SetActive(false);
    }

    void PlaceTroop()
    {
        if (currentPlayer == 1 && troopCountPlayer1 < maxTroops)
        {
            troopCountPlayer1++;
            Debug.Log($"Giocatore 1: truppa posizionata. Totale truppe: {troopCountPlayer1}");
        }
        else if (currentPlayer == 2 && troopCountPlayer2 < maxTroops)
        {
            troopCountPlayer2++;
            Debug.Log($"Giocatore 2: truppa posizionata. Totale truppe: {troopCountPlayer2}");
        }
        else
        {
            Debug.Log($"Giocatore {currentPlayer}: numero massimo di truppe raggiunto!");
        }
    }

    void Ready()
    {
        if ((currentPlayer == 1 && troopCountPlayer1 == maxTroops) ||
            (currentPlayer == 2 && troopCountPlayer2 == maxTroops))
        {
            if (currentPlayer == 1)
            {
                Debug.Log("Primo giocatore pronto! Passando al secondo giocatore...");
                currentPlayer = 2;  // Cambio turno
            }
            else
            {
                panel.SetActive(false);
                secondPanel.SetActive(true);
                mazzo.SetActive(true);
                Debug.Log("Secondo giocatore pronto! Attivando mazzo e interfaccia di gioco.");
            }
        }
        else
        {
            Debug.Log($"Giocatore {currentPlayer}: posiziona tutte le truppe prima di procedere.");
        }
    }
}