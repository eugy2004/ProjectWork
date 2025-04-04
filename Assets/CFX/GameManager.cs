using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerMove playerMove;
    private GridManager gridManager;
    private Transform player;

    public Transform closestNode;

    void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
    }

    void Update()
    {
        Transform closestNode = GetClosestNode(player.position);
    }

    public Transform GetClosestNode(Vector3 playerPosition)
    {
        if (gridManager == null || gridManager.gridNodesList == null || gridManager.gridNodesList.Count == 0)
        {
            Debug.LogWarning("GetClosestNode: gridNodesList non è inizializzata o è vuota!");
            return null;
        }

        Transform closestNode = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject nodeObject in gridManager.gridNodesList)
        {
            if (nodeObject == null) continue;

            float distance = Vector3.Distance(playerPosition, nodeObject.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestNode = nodeObject.transform;
            }
        }

        return closestNode;
    }



}
