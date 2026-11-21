using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.03f;   // Much snappier
    [SerializeField] private float minY = 0f;

    [Header("Dynamic Zoom")]
    [SerializeField] private float baseZoom = 5f;        // Match your Main Camera's starting Size
    [SerializeField] private float maxZoom = 12f;        // Furthest it will zoom out
    [SerializeField] private float zoomPerHeight = 0.3f; // How much to zoom out per unit of height
    [SerializeField] private float zoomSmoothTime = 0.25f;
    [SerializeField] private Rigidbody2D targetRb;
    [SerializeField] private float zoomPerSpeed = 0.3f;

    [SerializeField] private float lookAheadFactor = 0.08f; // How far ahead to shift based on X speed
    [SerializeField] private float maxLookAhead = 2.5f;     // Cap so the stickman stays on screen

    private float yVelocity = 0f;
    private float currentLookAhead = 0f;
    private float lookAheadVelocity = 0f;

    private Camera cam;
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    private float zoomVelocity = 0f;
    private float startY;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam != null) baseZoom = cam.orthographicSize;

        if (target == null) return;
        offset = transform.position - target.position;
        startY = target.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Lock X directly to the target + smooth look-ahead
        float speedX = targetRb != null ? targetRb.linearVelocity.x : 0f;
        float desiredLookAhead = Mathf.Clamp(speedX * lookAheadFactor, 0f, maxLookAhead);
        currentLookAhead = Mathf.SmoothDamp(currentLookAhead, desiredLookAhead, ref lookAheadVelocity, 0.2f);

        float targetX = target.position.x + offset.x + currentLookAhead;

        // 2. Keep Y smooth (still clamped above minY)
        float desiredY = Mathf.Max(target.position.y + offset.y, minY);
        float targetY = Mathf.SmoothDamp(transform.position.y, desiredY, ref yVelocity, smoothTime);

        transform.position = new Vector3(targetX, targetY, transform.position.z);

        // 3. Dynamic zoom (keep your existing zoom code here)
        if (cam != null)
        {
            float heightAboveStart = Mathf.Max(0f, target.position.y - startY);
            float speed = targetRb != null ? targetRb.linearVelocity.magnitude : 0f;
            float targetZoom = Mathf.Clamp(
                baseZoom + (heightAboveStart * zoomPerHeight) + (speed * zoomPerSpeed),
                baseZoom,
                maxZoom
            );
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref zoomVelocity, zoomSmoothTime);
        }
    }

    //void LateUpdate()
    //{
    //    if (target == null) return;

    //    // 1. Snappy position follow
    //    Vector3 desiredPosition = target.position + offset;
    //    desiredPosition.y = Mathf.Max(desiredPosition.y, minY);
    //    desiredPosition.z = transform.position.z;

    //    transform.position = Vector3.SmoothDamp(
    //        transform.position,
    //        desiredPosition,
    //        ref velocity,
    //        smoothTime
    //    );

    //    // 2. Zoom out as the stickman flies higher
    //    if (cam != null)
    //    {
    //        float heightAboveStart = Mathf.Max(0f, target.position.y - startY);
    //        float speed = targetRb != null ? targetRb.linearVelocity.magnitude : 0f;

    //        float targetZoom = Mathf.Clamp(
    //            baseZoom + (heightAboveStart * zoomPerHeight) + (speed * zoomPerSpeed),
    //            baseZoom,
    //            maxZoom
    //        );

    //        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref zoomVelocity, zoomSmoothTime);
    //    }
    //}
}