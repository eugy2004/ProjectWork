using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private List<GridNode> validNodes = new List<GridNode>(); // Nodi validi (gialli)


    public bool isInTurn;
    public bool isSelected;
    public bool isMoving;
    public bool isAttacking;

    private Vector3 offsetY = new Vector3(0, 2, 0);

    public int idTroop;

    private Vector3 rangeWarrior = new Vector3(1.1f, 1.1f, 1.1f);
    private Vector3 rangeWizard = new Vector3(5.1f, 5.1f, 5.1f);
    private Vector3 rangeArcher = new Vector3(7.1f, 7.1f, 7.1f);

    private void Awake()
    {
        isSelected = true;
        isMoving = true;
        isAttacking = true;
    }

    private void Update()
    {
        if (isSelected)
        {
            if (isMoving)
            {
                CameraRayCastMovement();
            }
            else if (isAttacking) 
            {
                CameraRayCastAttack();
            }
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

    public void CameraRayCastAttack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        RaycastHit targetHit;
        int layerMaskGridnode = LayerMask.GetMask("GridNode");
        int layerMaskCharacter = LayerMask.GetMask("Character");
        GameObject target = new GameObject();

        switch (idTroop)
        {
            case 0:
                WarriorAttackUpdate();
                break;
            case 1:
                ArcherAttackUpdate();
                break;
            case 2:
                WizardAttackUpdate();
                break;
        }

        void WarriorAttackUpdate() 
        {
            if (Physics.Raycast(ray, out hit, 1000f, layerMaskCharacter) && Input.GetMouseButtonDown(0))
            {

                target = hit.transform.gameObject;

                targetHit = hit;

                if (GetDistanceRayCast(targetHit, gameObject).x < rangeWarrior.x && GetDistanceRayCast(targetHit, gameObject).z < rangeWarrior.z)
                {

                }
            }
        }

        void ArcherAttackUpdate()
        {
            if (Physics.Raycast(ray, out hit, 1000f, layerMaskCharacter) && Input.GetMouseButtonDown(0))
            {

                target = hit.transform.gameObject;

                targetHit = hit;

                if (GetDistanceRayCast(targetHit, gameObject).x < rangeArcher.x && GetDistanceRayCast(targetHit, gameObject).z < rangeArcher.z)
                {

                }
            }
        }

        void WizardAttackUpdate()
        {
            if (Physics.Raycast(ray, out hit, 1000f, layerMaskGridnode) && Input.GetMouseButtonDown(0))
            {

                target = hit.transform.gameObject;

                targetHit = hit;

                if (GetDistanceRayCast(targetHit, gameObject).x < rangeWizard.x && GetDistanceRayCast(targetHit, gameObject).z < rangeWizard.z)
                {

                }
            }
        }
    }

    public Vector3 GetDistanceRayCast(RaycastHit hit, GameObject troop)
    {
        Vector3 distance = new Vector3();
        distance = hit.transform.position - troop.transform.position;
        return distance;
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
