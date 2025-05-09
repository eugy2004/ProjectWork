using System.Runtime.CompilerServices;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class Troop : MonoBehaviour
{
    private GameManager gameManager; private GridNode gridNode;
    public bool isDeploying;

    public static bool AnyDeploying = false;

    public Renderer renderer;

    public Collider collider;

    public Vector3 lastDeployPosition;

    public Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();
        gridNode = Object.FindFirstObjectByType<GridNode>();
        renderer = gameObject.GetComponent<Renderer>();
        collider = gameObject.GetComponent<Collider>();

        AnyDeploying = true;
        isDeploying = true;
    }

    private void Update()
    {
        if (isDeploying)
        {
            CameraRayCastDeploy();
            ConfirmDeploy();
        }
    }

    public void CameraRayCastDeploy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("GridNode");
        float offset = 2f;
        GridNode deployingGridNode;

        renderer.enabled = false;

        if (Physics.Raycast(ray, out hit, 1000f, layerMask) && isDeploying)
        {
            deployingGridNode = hit.collider.GetComponent<GridNode>();

            if (deployingGridNode.state == GridNode.GridNodeState.FREE)
            {
                lastDeployPosition = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + offset, hit.collider.gameObject.transform.position.z);
                transform.position = lastDeployPosition;
                renderer.enabled = true;
            }
            else
            {
                renderer.enabled = false;
            }
        }
    }

    public void ConfirmDeploy()
    {
        if (isDeploying && Input.GetMouseButtonDown(0) && renderer.enabled)
        {
            isDeploying = false;
            AnyDeploying = false;
            renderer.enabled = true;
            transform.position = lastDeployPosition;
        }
        if (isDeploying && (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)))
        {
            Destroy(gameObject);
            AnyDeploying = false;
        }
    }

    public void Dead()
    {
        Object.Destroy(gameObject);
    }
}