using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class HandRaycast : MonoBehaviour
{
    public Transform handOrigin;
    public Transform cameraRef;

    public float maxDistance = 10f;
    public LayerMask hitLayers;

    LineRenderer line;
    HoverDarken currentHover;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.01f;
        line.endWidth = 0.01f;
        line.useWorldSpace = true;
    }

    void Update()
    {
        if (!handOrigin || !cameraRef)
            return;

        Vector3 origin = handOrigin.position;
        Vector3 direction = cameraRef.forward;

        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        Vector3 endPoint = origin + direction * maxDistance;

        HoverDarken newHover = null;

        if (Physics.Raycast(ray, out hit, maxDistance, hitLayers))
        {
            endPoint = hit.point;
            newHover = hit.collider.GetComponent<HoverDarken>();
        }

        if (currentHover != newHover)
        {
            if (currentHover != null)
                currentHover.SetHover(false);

            if (newHover != null)
                newHover.SetHover(true);

            currentHover = newHover;
        }

        line.SetPosition(0, origin);
        line.SetPosition(1, endPoint);
    }
}
