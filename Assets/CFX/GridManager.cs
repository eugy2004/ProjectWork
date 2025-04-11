using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public GridNode gridNodeTemplate;

    public int righe = 7;
    public int colonne = 15;

    public GridNode[,] gridMatrix;

    public List<GameObject> gridNodesList = new List<GameObject>();

    // Inizializzo la griglia
    public void InitGrid()
    {
        // Inizializzazione matrice
        gridMatrix = new GridNode[righe, colonne];

        // Creo la griglia
        for (int i = 0; i < righe; i++)
        {
            for (int j = 0; j < colonne; j++)
            {
                GridNode tNode = Instantiate(gridNodeTemplate);

                // Inserisco il nodo in matrice
                gridMatrix[i, j] = tNode;
                tNode.coordinate = new Vector2(i, j);

                // Parent del gridNode spawnato = transform del GridManager
                tNode.transform.parent = transform;
                tNode.transform.position = new Vector3(i, 0, j);
                tNode.name = "GridNode_" + i + "_" + j;

                // Associa il GridManager al nodo
                tNode.gridManager = this;
                gridNodesList.Add(tNode.gameObject);
            }
        }

        for (int i = 0; i < righe; i++)
        {
            for (int j = 0; j < colonne; j++)
            {
                
                if (i > 0) 
                    gridMatrix[i, j].linkedNodes.Add(gridMatrix[i - 1, j]);

                if (i < righe - 1) 
                    gridMatrix[i, j].linkedNodes.Add(gridMatrix[i + 1, j]);

                if (j > 0) 
                    gridMatrix[i, j].linkedNodes.Add(gridMatrix[i, j - 1]);

                if (j < colonne - 1) 
                    gridMatrix[i, j].linkedNodes.Add(gridMatrix[i, j + 1]);

                if (i > 0 && j > 0)
                    gridMatrix[i, j].linkedDiagonalNodes.Add(gridMatrix[i - 1, j - 1]);

                if (i > 0 && j < colonne - 1)
                    gridMatrix[i, j].linkedDiagonalNodes.Add(gridMatrix[i - 1, j + 1]);

                if (i < righe - 1 && j > 0)
                    gridMatrix[i, j].linkedDiagonalNodes.Add(gridMatrix[i + 1, j - 1]);

                if (i < righe - 1 && j < colonne - 1)
                    gridMatrix[i, j].linkedDiagonalNodes.Add(gridMatrix[i + 1, j + 1]);
            }
        }
    }
    [ContextMenu("Popola nodi diagonali da scena")]
    public void PopolaNodiDiagonali()
    {
        gridMatrix = new GridNode[righe, colonne];

        // Trova tutti i GridNode nella scena (puoi filtrare meglio se serve)
        GridNode[] allNodes = GetComponentsInChildren<GridNode>();

        // Riempie la matrice in base alla posizione salvata nei nodi
        foreach (GridNode node in allNodes)
        {
            int i = (int)node.coordinate.x;
            int j = (int)node.coordinate.y;

            if (i >= 0 && i < righe && j >= 0 && j < colonne)
            {
                gridMatrix[i, j] = node;
            }
            else
            {
                Debug.LogWarning($"Nodo {node.name} ha coordinate fuori dai limiti: ({i},{j})");
            }
        }

        // Ora che la matrice è popolata, si possono assegnare i diagonali
        for (int i = 0; i < righe; i++)
        {
            for (int j = 0; j < colonne; j++)
            {
                GridNode node = gridMatrix[i, j];

                if (node == null)
                {
                    Debug.LogWarning($"Nessun nodo in gridMatrix[{i},{j}]");
                    continue;
                }

                node.linkedDiagonalNodes.Clear();

                if (i > 0 && j > 0 && gridMatrix[i - 1, j - 1] != null)
                    node.linkedDiagonalNodes.Add(gridMatrix[i - 1, j - 1]);

                if (i > 0 && j < colonne - 1 && gridMatrix[i - 1, j + 1] != null)
                    node.linkedDiagonalNodes.Add(gridMatrix[i - 1, j + 1]);

                if (i < righe - 1 && j > 0 && gridMatrix[i + 1, j - 1] != null)
                    node.linkedDiagonalNodes.Add(gridMatrix[i + 1, j - 1]);

                if (i < righe - 1 && j < colonne - 1 && gridMatrix[i + 1, j + 1] != null)
                    node.linkedDiagonalNodes.Add(gridMatrix[i + 1, j + 1]);
            }
        }

        Debug.Log("Popolamento nodi diagonali completato.");
    }

}