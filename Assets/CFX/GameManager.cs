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
}
