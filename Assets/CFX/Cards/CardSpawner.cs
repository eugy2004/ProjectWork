using UnityEngine;
using System.Collections.Generic;

public class RandomCardSpawner : MonoBehaviour
{
    public GameObject Carta1;
    public GameObject Carta2;
    public GameObject Carta3;
    public GameObject Carta4;
    public GameObject Carta5;

    private GameObject[] carte;
    public Vector3 firstSlotPosition; // Inserita manualmente nell'Inspector
    private List<Vector3> slotPositions = new List<Vector3>(); // Posizioni degli slot
    private Dictionary<int, GameObject> carteInSlot = new Dictionary<int, GameObject>(); // Mappa slot -> carta
    private int maxCarte = 5; // Numero massimo di slot disponibili
    private float offsetZ = 2.5f; // Distanza tra le carte

    void Start()
    {
        carte = new GameObject[] { Carta1, Carta2, Carta3, Carta4, Carta5 };

        // Genera le posizioni degli slot basandosi sulla posizione iniziale
        for (int i = 0; i < maxCarte; i++)
        {
            slotPositions.Add(new Vector3(firstSlotPosition.x, firstSlotPosition.y, firstSlotPosition.z + (i * offsetZ)));
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Tasto sinistro del mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    InstanziaCartaCasuale();
                }
            }
        }
    }

    void InstanziaCartaCasuale()
    {
        int randomIndex = Random.Range(0, carte.Length); // Genera un indice casuale

        // Trova il primo slot libero
        int slotLibero = TrovaPrimoSlotLibero();

        if (slotLibero != -1) // Se c'è almeno un posto libero
        {
            GameObject nuovaCarta = Instantiate(carte[randomIndex], slotPositions[slotLibero], Quaternion.Euler(0, 90, 0));
            carteInSlot[slotLibero] = nuovaCarta;
        }
        else // Se tutti gli slot sono pieni, fai scalare le carte
        {
            ScalaCarte();

            // Instanzia la nuova carta nell'ultimo slot
            GameObject nuovaCarta = Instantiate(carte[randomIndex], slotPositions[maxCarte - 1], Quaternion.Euler(0, 90, 0));
            carteInSlot[maxCarte - 1] = nuovaCarta;
        }
    }

    int TrovaPrimoSlotLibero()
    {
        for (int i = 0; i < maxCarte; i++)
        {
            if (!carteInSlot.ContainsKey(i) || carteInSlot[i] == null)
            {
                return i;
            }
        }
        return -1; // Nessuno slot libero
    }

    void ScalaCarte()
    {
        // Elimina la carta più vecchia
        Destroy(carteInSlot[0]);

        // Scala tutte le carte
        for (int i = 0; i < maxCarte - 1; i++)
        {
            carteInSlot[i] = carteInSlot[i + 1];
            carteInSlot[i].transform.position = slotPositions[i]; // Aggiorna la posizione
        }

        // Libera l'ultimo slot
        carteInSlot.Remove(maxCarte - 1);
    }
}