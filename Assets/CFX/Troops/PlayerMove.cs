using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private List<GridNode> validNodes = new List<GridNode>(); // Nodi validi (gialli)


    public bool isInTurn;
    public bool isSelected;

    private Vector3 offsetY = new Vector3(0, 2, 0);

    private void Awake()
    {
        isSelected = true;
    }

    private void Update()
    {
        if (isSelected)
        {
            CameraRayCastMovement();
        }
    }

    public void CameraRayCastMovement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMaskGD = LayerMask.GetMask("GridNode");

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 1000f, layerMaskGD))
        {
            GridNode clickedNode = hit.transform.GetComponent<GridNode>();
            if (clickedNode != null && validNodes.Contains(clickedNode) && clickedNode.state == GridNode.GridNodeState.FREE)
            {
                GridNode currentNode = OnGridNode().GetComponent<GridNode>();

                currentNode.state = GridNode.GridNodeState.FREE;

                transform.position = clickedNode.transform.position + offsetY;

                currentNode = clickedNode;

                clickedNode.SetPlayer(this);
            }
            else
            {
                Debug.Log("Non puoi posizionarti su questo nodo!");
                return;
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
        validNodes = newValidNodes;
    }

    public List<GridNode> GetValidNodes()
    {
        return validNodes;
    }
}
