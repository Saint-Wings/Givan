using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Level Boundaries")]
    [SerializeField] private Transform boundaryTopRight;
    [SerializeField] private Transform boundaryBottomLeft;
    [SerializeField] private bool autoDetectBoundaries = true;

    [Header("Camera Settings")]
    [SerializeField] private float panSpeed = 5f;
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 8f;
    [SerializeField] private float edgePadding = 0.5f;

    private Camera cam;
    private Vector3 dragOrigin;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (autoDetectBoundaries) AutoDetectBoundaries();
    }

    private void Update()
    {
        HandleZoom();
        HandlePan();
        ClampCameraPosition();
    }

    private void AutoDetectBoundaries()
    {
        // Находим все граничные спрайты
        Transform bl = GameObject.Find("Boundary_BottomLeft")?.transform;
        Transform br = GameObject.Find("Boundary_BottomRight")?.transform;
        Transform tl = GameObject.Find("Boundary_TopLeft")?.transform;
        Transform tr = GameObject.Find("Boundary_TopRight")?.transform;

        if (bl != null && tr != null)
        {
            boundaryBottomLeft = bl;
            boundaryTopRight = tr;
        }
        else if (bl != null && br != null && tl != null && tr != null)
        {
            // Если есть все 4 угла, вычисляем общие границы
            Vector3 min = bl.position;
            Vector3 max = tr.position;

            min.x = Mathf.Min(bl.position.x, br.position.x, tl.position.x, tr.position.x);
            min.y = Mathf.Min(bl.position.y, br.position.y, tl.position.y, tr.position.y);
            max.x = Mathf.Max(bl.position.x, br.position.x, tl.position.x, tr.position.x);
            max.y = Mathf.Max(bl.position.y, br.position.y, tl.position.y, tr.position.y);

            // Создаем временные объекты для границ
            GameObject go = new GameObject("CameraBounds");
            boundaryBottomLeft = new GameObject("Auto_BottomLeft").transform;
            boundaryBottomLeft.position = min;
            boundaryBottomLeft.parent = go.transform;

            boundaryTopRight = new GameObject("Auto_TopRight").transform;
            boundaryTopRight.position = max;
            boundaryTopRight.parent = go.transform;
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            cam.orthographicSize = Mathf.Clamp(
                cam.orthographicSize - scroll * zoomSpeed,
                minZoom,
                maxZoom
            );
        }
    }

    private void HandlePan()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position += difference;
        }
    }

    private void ClampCameraPosition()
    {
        if (boundaryTopRight == null || boundaryBottomLeft == null) return;

        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Вычисляем границы с учетом размера камеры и отступа
        float minX = boundaryBottomLeft.position.x + camWidth;
        float maxX = boundaryTopRight.position.x - camWidth;
        float minY = boundaryBottomLeft.position.y + camHeight;
        float maxY = boundaryTopRight.position.y - camHeight;

        // Добавляем отступ только если камера не слишком большая для уровня
        if (maxX > minX)
        {
            minX += edgePadding;
            maxX -= edgePadding;
        }

        if (maxY > minY)
        {
            minY += edgePadding;
            maxY -= edgePadding;
        }

        // Обеспечиваем, чтобы камера не выходила за границы
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        transform.position = clampedPosition;
    }

    private void OnDrawGizmosSelected()
    {
        if (boundaryTopRight != null && boundaryBottomLeft != null)
        {
            Gizmos.color = new Color(0, 1, 1, 0.3f);
            Vector3 center = (boundaryTopRight.position + boundaryBottomLeft.position) / 2;
            Vector3 size = boundaryTopRight.position - boundaryBottomLeft.position;
            Gizmos.DrawCube(center, size);
        }
    }
}