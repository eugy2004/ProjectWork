using UnityEngine;

public class Carta1 : MonoBehaviour
{
    private bool isRaycastActive = false; // Indica se l'evidenziazione è attiva
    private Ray ray;
    private RaycastHit hit;

    private GridNode lastHighlightedNode; // Nodo precedentemente evidenziato
    private PlayerMove playerMove; // Riferimento al player

    [Header("Materiale per evidenziazione")]
    public Material highlightMaterial; // Materiale rosso da assegnare nell'Inspector

    void Start()
    {
        // Ottieni il riferimento al player
        playerMove = GameObject.FindObjectOfType<PlayerMove>();
    }

    void Update()
    {
        // Controlla se l'oggetto (carta) è stato cliccato per attivare/disattivare il Raycast
        if (Input.GetMouseButtonDown(0))
        {
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(clickRay, out RaycastHit clickHit))
            {
                if (clickHit.collider.gameObject == gameObject)
                {
                    isRaycastActive = !isRaycastActive; // Attiva/disattiva la modalità Raycast
                    Debug.Log(isRaycastActive ? "Evidenziazione attivata" : "Evidenziazione disattivata");
                }
            }
        }

        // Se l'evidenziazione è attiva, gestisci il passaggio del cursore sui nodi
        if (isRaycastActive)
        {
            HighlightNodeUnderCursor();

            // Controlla se il mouse è premuto per interagire con il nodo
            if (Input.GetMouseButtonDown(0))
            {
                HandleNodeClick();
            }
        }
    }

    private void HighlightNodeUnderCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GridNode nodeUnderCursor = hit.transform.GetComponent<GridNode>();

            if (nodeUnderCursor != null && nodeUnderCursor != lastHighlightedNode)
            {
                ResetNodeColor(); // Ripristina il colore dell'ultimo nodo evidenziato

                Renderer renderer = nodeUnderCursor.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = highlightMaterial; // Cambia il materiale in rosso
                }

                lastHighlightedNode = nodeUnderCursor; // Aggiorna il nodo evidenziato
            }
        }
        else
        {
            ResetNodeColor(); // Ripristina il colore se nessun nodo è sotto il cursore
        }
    }

    private void HandleNodeClick()
    {
        if (lastHighlightedNode != null)
        {
            Debug.Log("Nodo selezionato: " + lastHighlightedNode.name);

            // Lancia un Raycast dal nodo verso l'alto
            Ray ray = new Ray(lastHighlightedNode.transform.position, Vector3.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5f)) // Limita la distanza a 5 per evitare colpi inutili
            {
                GameObject troopOnNode = hit.collider.gameObject;

                if (troopOnNode.CompareTag("Troop")) // Assicurati che la capsula abbia il tag "Troop"
                {
                    Debug.Log("Capsula trovata sopra il nodo: " + troopOnNode.name);

                    // Distruggi la capsula sopra il nodo selezionato
                    Destroy(troopOnNode);
                    Debug.Log("Capsula eliminata su nodo " + lastHighlightedNode.name);
                }
                else
                {
                    Debug.Log("Nessuna truppa sopra il nodo selezionato.");
                }
            }
        }
    }
    private void ResetNodeColor()
    {
        if (lastHighlightedNode != null)
        {
            Renderer renderer = lastHighlightedNode.GetComponent<Renderer>();
            if (renderer != null && lastHighlightedNode.originalMaterial != null)
            {
                renderer.material = lastHighlightedNode.originalMaterial; // Ripristina il materiale originale
            }
            lastHighlightedNode = null;
        }
    }
}
