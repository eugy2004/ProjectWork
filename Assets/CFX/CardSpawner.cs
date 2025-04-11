using UnityEngine;

public class RandomCardSpawner : MonoBehaviour
{
    public GameObject Carta1;
    public GameObject Carta2;
    public GameObject Carta3;

    private GameObject[] carte;
    public Vector3 spawnPosition; // Posizione di spawn iniziale

    void Start()
    {
        // Inizializza l'array con i prefabs delle carte
        carte = new GameObject[] { Carta1, Carta2, Carta3 };
    }

    void Update()
    {
        // Controlla se il tasto sinistro del mouse è stato premuto
        if (Input.GetMouseButtonDown(0)) // Tasto sinistro del mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Controlla se il click è avvenuto sull'oggetto del mazzo
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    // Instanzia una carta casuale
                    InstanziaCartaCasuale();
                }
            }
        }
    }

    void InstanziaCartaCasuale()
    {
        // Genera un indice casuale
        int randomIndex = Random.Range(0, 3);

        // Istanzia la carta casuale
        Instantiate(carte[randomIndex], spawnPosition, Quaternion.identity);

        // Incrementa la coordinata X della posizione di spawn
        spawnPosition.z += 5;
    }
}