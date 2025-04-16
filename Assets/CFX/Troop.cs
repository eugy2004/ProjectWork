using System.Runtime.CompilerServices;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Troop : MonoBehaviour
{
    private GameManager gameManager;

    public bool isDeploying = true;

    public Renderer renderer;

    public Collider collider;

    void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();
        renderer = gameObject.GetComponent<Renderer>();
        collider = gameObject.GetComponent<Collider>();
    }

    private void Update()
    {
        CameraRayCast();
    }

    public void CameraRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("GridNode");
        float offset = 2f;

        if (Physics.Raycast(ray, out hit, 1000f, layerMask) && isDeploying)
        {
            Debug.Log("Sto disegnando il raycast");
            renderer.enabled = true;
            transform.position = new Vector3 (hit.point.x, hit.point.y + offset, hit.point.z);
        }
        else
        {
            renderer.enabled = false;
        }
    }
}
