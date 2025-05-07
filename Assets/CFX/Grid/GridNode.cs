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
    public List<GridNode> linkedNodes = new List<GridNode>();
    public List<GridNode> linkedDiagonalNodes = new List<GridNode>();

    private Renderer gridnodeRenderer;
    public Material originalMaterial;
    private Color originalColor;
    private GameManager gameManager;
    public PlayerMove playerMove;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gridnodeRenderer = GetComponent<Renderer>();
        originalColor = gridnodeRenderer.material.color;
        state = GridNodeState.FREE; // Imposta lo stato iniziale
    }

    private void Update()
    {
        OnPlayerOn();
    }

    


    public void OnPlayerOn()
    {
        if (state == GridNodeState.PLAYERON)
        {
            return; // Evita di cambiare nuovamente lo stato se il nodo è già PLAYERON
        }

        if (playerMove != null)
        {
            if (gameObject == playerMove.OnGridNode() && !Troop.AnyDeploying)
            {
                state = GridNodeState.PLAYERON;
                AggiornaColoriNodi();
                Debug.Log(name + " impostato a PLAYERON!");
            }
            else
            {
                state = GridNodeState.FREE;
            }
        }
    }

    public void SetPlayer(PlayerMove newPlayer)
    {
        playerMove = newPlayer;
    }

    // Metodo per gestire il piazzamento di una truppa e mantenere lo stato PLAYERON
    public void OnTroopDeployed()
    {
        state = GridNodeState.PLAYERON;
        Debug.Log(name + " - Nodo ora è PLAYERON (truppa schierata)");
        AggiornaColoriNodi();
    }

    private void AggiornaColoriNodi()
    {
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
                nuoviNodiValidi.Add(linkedNode);
            }
        }

        // Colora i nodi diagonali di giallo
        foreach (GridNode linkedDiagonalNode in linkedDiagonalNodes)
        {
            Renderer renderer = linkedDiagonalNode.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.yellow; // Colora di giallo
                nuoviNodiValidi.Add(linkedDiagonalNode);
            }
        }

        // Aggiorna i nodi validi nel player
        playerMove.UpdateValidNodes(nuoviNodiValidi);
    }

    public Color GetOriginalColor()
    {
        return originalColor;
    }

}