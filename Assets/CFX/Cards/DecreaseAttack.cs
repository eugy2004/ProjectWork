using UnityEngine;

public class DecreaseAttack : MonoBehaviour
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

            // Controlla se c'è una truppa sopra il nodo
            Ray ray = new Ray(lastHighlightedNode.transform.position, Vector3.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5f))
            {
                GameObject troopOnNode = hit.collider.gameObject;
                Character troopCharacter = troopOnNode.GetComponent<Character>();

                if (troopCharacter != null && troopOnNode.CompareTag("Troop"))
                {
                    Debug.Log("Capsula trovata sopra il nodo: " + troopOnNode.name);

                    // Aumenta l'attacco invece di distruggere la truppa
                    troopCharacter.attack -= 10;
                    Debug.Log("Attacco aumentato per " + troopOnNode.name + ". Nuovo valore: " + troopCharacter.attack);

                    // Ripristina il colore originale dei nodi adiacenti
                    ResetAdjacentNodeColors(lastHighlightedNode);


                    ResetNodeColor();


                    Destroy(gameObject);
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
            if (renderer != null)
            {
                // Se il nodo è valido per una truppa, mantieni il colore giallo
                if (IsValidNodeForAnyTroop(lastHighlightedNode))
                {
                    renderer.material.color = Color.yellow;
                }
                else
                {
                    renderer.material.color = lastHighlightedNode.GetOriginalColor();
                }
            }
            lastHighlightedNode = null;
        }
    }


    private void ResetAdjacentNodeColors(GridNode node)
    {
        foreach (GridNode linkedNode in node.linkedNodes)
        {
            Renderer renderer = linkedNode.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (IsValidNodeForAnyTroop(linkedNode))
                {
                    renderer.material.color = Color.yellow; // Mantieni giallo
                }
                else
                {
                    renderer.material.color = linkedNode.GetOriginalColor();
                }
            }
        }

        foreach (GridNode linkedDiagonalNode in node.linkedDiagonalNodes)
        {
            Renderer renderer = linkedDiagonalNode.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (IsValidNodeForAnyTroop(linkedDiagonalNode))
                {
                    renderer.material.color = Color.yellow; // Mantieni giallo
                }
                else
                {
                    renderer.material.color = linkedDiagonalNode.GetOriginalColor();
                }
            }
        }

        Debug.Log("I nodi adiacenti sono stati ripristinati, mantenendo quelli validi gialli.");
    }

    private bool IsTroopOnNode(GridNode node)
    {
        Ray ray = new Ray(node.transform.position, Vector3.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.collider.CompareTag("Troop"))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsValidNodeForAnyTroop(GridNode node)
    {
        foreach (PlayerMove troop in FindObjectsOfType<PlayerMove>())
        {
            if (troop.GetValidNodes().Contains(node))
            {
                return true; // Il nodo è valido per almeno una truppa
            }
        }
        return false;
    }
}
