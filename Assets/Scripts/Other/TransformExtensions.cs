using UnityEngine;

public static class TransformExtensions
{
    public static bool IsInCameraView(this Transform transform)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }
}
