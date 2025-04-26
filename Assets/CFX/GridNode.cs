using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class GridNode : MonoBehaviour
{
    // Enum per rappresentare lo stato del nodo
    public enum GridNodeState { FREE, PLAYERON }
    public GridNodeState state = GridNodeState.FREE; // Stato iniziale del nodo

    public GridManager gridManager;

    public Vector2 coordinate; // Coordinate del nodo, se necessario

    // Lista dei nodi adiacenti
    public List<GridNode> linkedNodes = new List<GridNode>();
    
    public List<GridNode> linkedDiagonalNodes = new List<GridNode>();

    private Renderer gridnodeRenderer;

    public Material originalMaterial;

    private Color originalColor;

    private GameManager gameManager;

    private PlayerMove playerMove; 

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        gridnodeRenderer = GetComponent<Renderer>();
        originalColor = gridnodeRenderer.material.color;
        state = GridNodeState.FREE; // Imposta lo stato iniziale

    }

    private void Update()
    {
        OnPlayerOn();
    }

    

    private void OnMouseExit()
    {
        gridnodeRenderer.material.SetColor("_BaseColor", originalColor);
    }

    public void OnPlayerOn()
{
    if (gameObject == playerMove.OnGridNode())
    {

        state = GridNodeState.PLAYERON;

        // Ripristina i colori di tutti i nodi precedenti
        foreach (GridNode nodo in playerMove.GetValidNodes())
        {
            Renderer renderer = nodo.GetComponent<Renderer>();
            if (renderer != null)
            {
                    renderer.material = originalMaterial; // Torna al colore originale
            }
        }

        // Lista temporanea per i nuovi nodi validi
        List<GridNode> nuoviNodiValidi = new List<GridNode>();

        // Colora i nodi adiacenti di giallo
        foreach (GridNode linkedNode in linkedNodes)
        {
            Renderer renderer = linkedNode.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.yellow; // Colora di giallo
                nuoviNodiValidi.Add(linkedNode); // Aggiungi alla lista dei validi
            }
        }

        // Colora i nodi diagonali di giallo
        foreach (GridNode linkedDiagonalNode in linkedDiagonalNodes)
        {
            Renderer renderer = linkedDiagonalNode.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.yellow; // Colora di giallo
                nuoviNodiValidi.Add(linkedDiagonalNode); // Aggiungi alla lista dei validi
            }
        }

        // Aggiorna i nodi validi nel player
        playerMove.UpdateValidNodes(nuoviNodiValidi);
    }
    else
    {
            state = GridNodeState.FREE;
    }
}
}