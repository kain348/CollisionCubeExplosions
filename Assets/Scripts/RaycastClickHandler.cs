using UnityEngine;

internal class RaycastClickHandler : MonoBehaviour
{
    [SerializeField] private LayerMask clickableLayer;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickableLayer))
            {
                ClickableObject clickable = hit.collider.GetComponent<ClickableObject>();

                if (clickable != null)
                {
                    clickable.HandleClick();
                }
            }
        }
    }
}