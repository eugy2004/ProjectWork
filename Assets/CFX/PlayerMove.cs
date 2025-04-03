using UnityEditor.AnimatedValues;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mouse = Input.mousePosition;

            mouse = Camera.main.ScreenToWorldPoint(mouse);

            ThrowRaycast(mouse);
        }
    }

    private void ThrowRaycast(Vector3 mousePosition)
    {
        Transform ctr = Camera.main.transform;

        Debug.DrawRay(mousePosition, (ctr.position - mousePosition)*1000, Color.white, 2);
        /*if (Physics.Raycast(mousePosition, ctr.forward, out RaycastHit hit, 100))
        {
            GameObject obj = hit.transform.gameObject;
            transform.position = obj.transform.position + Vector3.up;
        }*/
    }
}
