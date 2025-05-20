using UnityEngine;
using System.Collections;

public class ZoomOnClick : MonoBehaviour
{
    private Camera mainCamera;
    private bool isZoomed = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float zoomHeight = 2f; // Altezza di zoom
    private float zoomSpeed = 2f; // Velocità dello zoom

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = mainCamera.transform.position;
        originalRotation = mainCamera.transform.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Tasto destro del mouse
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // Se il gameobject è quello cliccato
                {
                    StartCoroutine(ZoomObject(hit.transform.position));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || (isZoomed && Input.GetMouseButtonDown(0))) // ESC o Tasto sinistro del mouse
        {
            StartCoroutine(DezoomObject());
        }
    }

    IEnumerator ZoomObject(Vector3 targetPosition)
    {
        if (!isZoomed)
        {
            float extraDistance = 2f;
            Vector3 targetPos = targetPosition + Vector3.up * (zoomHeight + extraDistance);
            Quaternion targetRot = Quaternion.Euler(90f, 270f, 0f);

            float elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPos, elapsedTime);
                mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetRot, elapsedTime);
                elapsedTime += Time.deltaTime * zoomSpeed;
                yield return null;
            }

            mainCamera.transform.position = targetPos;
            mainCamera.transform.rotation = targetRot;
            isZoomed = true;
        }
    }

    IEnumerator DezoomObject()
    {
        if (isZoomed)
        {
            float elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalPosition, elapsedTime);
                mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, originalRotation, elapsedTime);
                elapsedTime += Time.deltaTime * zoomSpeed;
                yield return null;
            }

            mainCamera.transform.position = originalPosition;
            mainCamera.transform.rotation = originalRotation;
            isZoomed = false;
        }
    }
}