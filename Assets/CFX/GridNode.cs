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

    private Renderer gridnodeRenderer;

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

    /*private void OnCollisionEnter(Collision collision)
    {
        // Verifica se il layer dell'oggetto in collisione è "Player"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            state = GridNodeState.PLAYERON;

            // Colora i nodi adiacenti di giallo
            foreach (GridNode linkedNode in linkedNodes)
            {
                Renderer renderer = linkedNode.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.yellow;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Verifica se il layer dell'oggetto che ha smesso di collidere è "Player"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            state = GridNodeState.FREE;

            // Torna al colore verde per i nodi adiacenti
            foreach (GridNode linkedNode in linkedNodes)
            {
                Renderer renderer = linkedNode.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.green;
                }
            }
        }
    }*/

    private void OnMouseEnter()
    {
        /*if (gameObject.transform.position == gameManager.GetClosestNode(player.transform.position).position)
        {
            foreach (GridNode linkedNode in linkedNodes)
            {
                Renderer renderer = linkedNode.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.SetColor("_BaseColor", Color.yellow);
                }
            }
        }*/
    }

    private void OnMouseExit()
    {
        gridnodeRenderer.material.SetColor("_BaseColor", originalColor);
    }

    public void OnPlayerOn()
    {
        if (gameObject == playerMove.OnGridNode())
        {
            foreach (GridNode linkedNode in linkedNodes)
            {
                Renderer renderer = linkedNode.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Debug.Log("Coloro questo gridnode di giallo");
                    renderer.material.color = Color.yellow;
                }
            }
        }
    }
}