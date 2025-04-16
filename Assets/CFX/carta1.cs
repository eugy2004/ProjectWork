using UnityEngine;

public class Carta1 : MonoBehaviour
{
    private bool isRaycastActive = false; // Flag per tenere traccia dello stato del Raycast
    private Ray ray;
    private RaycastHit hit;

    public Material originalMaterial; // Materiale originale del nodo
    public Material highlightedMaterial; // Materiale per evidenziare il nodo
    private GridNode lastHighlightedNode; // Nodo evidenziato precedentemente

    void Update()
    {
        // Controlla se il tasto sinistro del mouse è stato premuto
        if (Input.GetMouseButtonDown(0)) // Tasto sinistro del mouse
        {
            if (isRaycastActive)
            {
                // Disattiva il Raycast
                isRaycastActive = false;
                Debug.Log("Raycast disattivato");

                // Resetta l'ultimo nodo evidenziato se presente
                if (lastHighlightedNode != null)
                {
                    Renderer nodeRenderer = lastHighlightedNode.GetComponent<Renderer>();
                    if (nodeRenderer != null)
                    {
                        nodeRenderer.material = originalMaterial; // Ripristina il materiale originale
                    }
                    lastHighlightedNode = null;
                }
            }
            else
            {
                // Esegui un Raycast quando la carta viene cliccata
                Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(clickRay, out RaycastHit clickHit))
                {
                    if (clickHit.collider != null && clickHit.collider.gameObject == gameObject)
                    {
                        // Attiva il Raycast continuo
                        isRaycastActive = true;
                        Debug.Log("Raycast attivato sulla carta: " + gameObject.name);
                    }
                }
            }
        }

        // Se il Raycast è attivo, continua a calcolarlo
        if (isRaycastActive)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000f))
            {
                // Ottieni il nodo attualmente sotto il Raycast
                GridNode currentNode = hit.transform.GetComponent<GridNode>();
                if (currentNode != null)
                {
                    // Se il nodo è diverso dall'ultimo evidenziato
                    if (currentNode != lastHighlightedNode)
                    {
                        // Resetta l'ultimo nodo evidenziato
                        if (lastHighlightedNode != null)
                        {
                            Renderer lastRenderer = lastHighlightedNode.GetComponent<Renderer>();
                            if (lastRenderer != null)
                            {
                                lastRenderer.material = originalMaterial; // Ripristina il materiale originale
                            }
                        }

                        // Evidenzia il nuovo nodo
                        Renderer currentRenderer = currentNode.GetComponent<Renderer>();
                        if (currentRenderer != null)
                        {
                            currentRenderer.material = highlightedMaterial; // Imposta il materiale evidenziato
                        }

                        lastHighlightedNode = currentNode; // Aggiorna il nodo evidenziato
                    }
                }
            }
            else
            {
                // Se il Raycast non colpisce nulla, resettare l'ultimo nodo evidenziato
                if (lastHighlightedNode != null)
                {
                    Renderer nodeRenderer = lastHighlightedNode.GetComponent<Renderer>();
                    if (nodeRenderer != null)
                    {
                        nodeRenderer.material = originalMaterial; // Ripristina il materiale originale
                    }
                    lastHighlightedNode = null;
                }
            }
        }
    }
}