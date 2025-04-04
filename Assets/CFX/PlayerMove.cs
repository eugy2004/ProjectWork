using UnityEditor.AnimatedValues;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private void FixedUpdate()
    {
        CameraRayCast();
    }

    public void CameraRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 mouseWorldPos;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            mouseWorldPos = hit.point;
        }
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.white);
    }
}
