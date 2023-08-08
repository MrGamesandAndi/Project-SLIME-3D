using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringArm : MonoBehaviour
{
    #region Variables
    public float targetLength = 3.0f;
    public float speedDamp = 0.0f;
    public Transform collisionSocket;
    public float collisionRadius = 0.25f;
    public LayerMask collisionMask = 0;
    public Camera _camera;
    public float cameraViewportExtentsMultiplier = 1.0f;

    private Vector3 _socketVelocity;
    #endregion

    #region Basic Methods
    private void LateUpdate()
    {
        if (_camera != null)
        {
            collisionRadius = GetCollisionRadiusForCamera(_camera);
            _camera.transform.localPosition = -Vector3.forward * _camera.nearClipPlane;
        }

        UpdateLength();
    }

    private void OnDrawGizmos()
    {
        if (collisionSocket != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, collisionSocket.transform.position);
            DrawGizmoSphere(collisionSocket.transform.position, collisionRadius);
        }
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cam"></param>
    /// <returns></returns>
    private float GetCollisionRadiusForCamera(Camera cam)
    {
        float halfFOV = (cam.fieldOfView / 2.0f) * Mathf.Deg2Rad;
        float nearClipPlaneHalfHeight = Mathf.Tan(halfFOV) * cam.nearClipPlane * cameraViewportExtentsMultiplier;
        float nearClipPlaneHalfWidth = nearClipPlaneHalfHeight * cam.aspect;
        float collisionRadius = new Vector2(nearClipPlaneHalfWidth, nearClipPlaneHalfHeight).magnitude;

        return collisionRadius;
    }

    private float GetDesiredTargetLength()
    {
        Ray ray = new Ray(transform.position, -transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, Mathf.Max(0.001f, collisionRadius), out hit, targetLength, collisionMask))
        {
            return hit.distance;
        }
        else
        {
            return targetLength;
        }
    }

    private void UpdateLength()
    {
        float targetLength = GetDesiredTargetLength();
        Vector3 newSocketLocalPosition = -Vector3.forward * targetLength;

        collisionSocket.localPosition = Vector3.SmoothDamp(
            collisionSocket.localPosition, newSocketLocalPosition, ref _socketVelocity, speedDamp);
    }

    

    private void DrawGizmoSphere(Vector3 pos, float radius)
    {
        Quaternion rot = Quaternion.Euler(-90.0f, 0.0f, 0.0f);

        int alphaSteps = 8;
        int betaSteps = 16;

        float deltaAlpha = Mathf.PI / alphaSteps;
        float deltaBeta = 2.0f * Mathf.PI / betaSteps;

        for (int a = 0; a < alphaSteps; a++)
        {
            for (int b = 0; b < betaSteps; b++)
            {
                float alpha = a * deltaAlpha;
                float beta = b * deltaBeta;

                Vector3 p1 = pos + rot * GetSphericalPoint(alpha, beta, radius);
                Vector3 p2 = pos + rot * GetSphericalPoint(alpha + deltaAlpha, beta, radius);
                Vector3 p3 = pos + rot * GetSphericalPoint(alpha + deltaAlpha, beta - deltaBeta, radius);

                Gizmos.DrawLine(p1, p2);
                Gizmos.DrawLine(p2, p3);
            }
        }
    }

    private Vector3 GetSphericalPoint(float alpha, float beta, float radius)
    {
        Vector3 point;
        point.x = radius * Mathf.Sin(alpha) * Mathf.Cos(beta);
        point.y = radius * Mathf.Sin(alpha) * Mathf.Sin(beta);
        point.z = radius * Mathf.Cos(alpha);

        return point;
    }
    #endregion
}
