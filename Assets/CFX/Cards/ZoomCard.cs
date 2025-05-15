using UnityEngine;

public class ZoomOnClick : MonoBehaviour
{
    private Camera mainCamera;
    private bool isZoomed = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float zoomHeight = 2f; // Altezza di zoom

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
                    ZoomObject(hit.transform.position);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // Tasto ESC
        {
            DezoomObject();
        }
    }

    void ZoomObject(Vector3 targetPosition)
    {
        if (!isZoomed)
        {
            float extraDistance = 2f; // Puoi regolare questa distanza per ottenere più spazio

            // Posiziona la camera sopra la carta con più distanza
            mainCamera.transform.position = targetPosition + Vector3.up * (zoomHeight + extraDistance);

            // Ruota la camera per una vista perfettamente verticale con Y a 270°
            mainCamera.transform.rotation = Quaternion.Euler(90f, 270f, 0f);

            isZoomed = true;
        }
    }



    void DezoomObject()
    {
        if (isZoomed)
        {
            // Ripristina posizione e rotazione originali
            mainCamera.transform.position = originalPosition;
            mainCamera.transform.rotation = originalRotation;
            isZoomed = false;
        }
    }
}