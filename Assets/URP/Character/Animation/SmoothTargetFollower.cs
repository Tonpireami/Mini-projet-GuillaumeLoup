using UnityEngine;

public class SmoothTargetFollower : MonoBehaviour
{
    public Transform target;
    public float positionLerpSpeed = 15f;
    public float rotationLerpSpeed = 15f;

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            Time.deltaTime * positionLerpSpeed
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            target.rotation,
            Time.deltaTime * rotationLerpSpeed
        );
    }
}
