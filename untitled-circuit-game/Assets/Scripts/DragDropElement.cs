using UnityEngine;

public class DragDropElement : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private bool isGrabbingObject;
    private GameObject grabbedGameObject;
    private void Update()
    {
        if (isGrabbingObject)
        {
            // TODO: Draw shilouette at the nearest grid point
        }
        if (Input.GetMouseButtonDown(0) && !isGrabbingObject) // 0 is left click
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 point = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
            Vector3 cameraDirection = mainCamera.transform.TransformDirection(Vector3.forward);

            RaycastHit hit;

            if (Physics.Raycast(
                point,
                cameraDirection,
                out hit, mainCamera.farClipPlane,
                LayerMask.GetMask("DragableCircuitElements")
            ))
            {
                grabbedGameObject = hit.collider.gameObject;
                isGrabbingObject = true;
            }
        }
        else if (Input.GetMouseButtonUp(0) && isGrabbingObject)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 point = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
            Vector3 cameraDirection = mainCamera.transform.TransformDirection(Vector3.forward);

            RaycastHit hit;

            if (Physics.Raycast(
                point,
                cameraDirection,
                out hit, mainCamera.farClipPlane,
                LayerMask.GetMask("GridCell")
            ))
            {
                grabbedGameObject.transform.position = hit.collider.gameObject.transform.position + new Vector3(0, 1, 0);
                isGrabbingObject = false;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 point = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.farClipPlane));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(mainCamera.transform.position, point);
    }
}