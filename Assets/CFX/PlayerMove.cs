using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private List<GridNode> validNodes = new List<GridNode>(); // Nodi validi (colorati di giallo)

    public bool isInTurn;                                     // per sapere se è il turno del suo giocatore
    public bool isSelected;                                   // per non far muovere tutti i personaggi


    private void Awake()
    {
        isSelected = false;
    }

    private void Update()
    {
        if (isSelected)
        {
            CameraRayCast(); // Gestisce il movimento verso un nodo valido
        }
        OnGridNode();    // Ottiene il nodo attuale
    }

    public void CameraRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Controlla il click e verifica che l'oggetto cliccato sia un nodo
        if (Physics.Raycast(ray, out hit, 1000f) && Input.GetMouseButtonDown(0))
        {
            GridNode clickedNode = hit.transform.GetComponent<GridNode>();
            if (clickedNode != null && validNodes.Contains(clickedNode)) // Controlla se è un nodo valido
            {
                // Muovi il player sopra il nodo cliccato
                transform.position = clickedNode.transform.position + Vector3.up;
            }
            else
            {
                Debug.Log("Non puoi muoverti su questo nodo!");
            }
        }
    }

    public GameObject OnGridNode()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    public void UpdateValidNodes(List<GridNode> newValidNodes)
    {
        // Aggiorna la lista dei nodi validi
        validNodes = newValidNodes;
    }

    public List<GridNode> GetValidNodes()
    {
        // Restituisce la lista dei nodi validi
        return validNodes;
    }
}