using System.Globalization;
using UnityEditor.AnimatedValues;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private void FixedUpdate()
    {
        CameraRayCast();
        OnGridNode();
    }

    public void CameraRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Transform trHitObj;

        if (Physics.Raycast(ray, out hit, 1000f)  && Input.GetMouseButtonDown(0))
        {
            trHitObj = hit.transform;
            transform.position = trHitObj.position + Vector3.up;
        }
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.white);
    }

    public GameObject OnGridNode()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.green);

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            return hit.collider.gameObject;
        }

        return null;
    }
}
