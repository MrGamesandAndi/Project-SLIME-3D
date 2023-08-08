using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    #region Variables
    [Tooltip("How fast the camera rotates around the pivot. Value <= 0 is interpreted as instant rotation.")]
    public float rotationSpeed = 0.0f;
    public float positionSmoothDamp = 0.0f;
    public Transform rig;
    public Transform pivot; 
    public Camera _camera;

    private Vector3 cameraVelocity;
    #endregion

    #region Custom Methods
    /// <summary>
    /// Positions the camera.
    /// </summary>
    /// <param name="position">Target of the camera.</param>
    public void SetPosition(Vector3 position)
    {
        rig.position = Vector3.SmoothDamp(rig.position, position, ref cameraVelocity, positionSmoothDamp);
    }

    /// <summary>
    /// Rotates the camera towards target.
    /// </summary>
    /// <param name="controlRotation">Rotation of the controller</param>
    public void SetControlRotation(Vector2 controlRotation)
    {
        Quaternion rigTargetLocalRotation = Quaternion.Euler(0.0f, controlRotation.y, 0.0f);
        Quaternion pivotTargetLocalRotation = Quaternion.Euler(controlRotation.x, 0.0f, 0.0f);

        if (rotationSpeed > 0.0f)
        {
            rig.localRotation = Quaternion.Slerp(rig.localRotation, rigTargetLocalRotation, rotationSpeed * Time.deltaTime);
            pivot.localRotation = Quaternion.Slerp(pivot.localRotation, pivotTargetLocalRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            rig.localRotation = rigTargetLocalRotation;
            pivot.localRotation = pivotTargetLocalRotation;
        }
    }
    #endregion
}
