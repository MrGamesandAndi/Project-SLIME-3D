using UnityEngine;

public abstract class Controller : ScriptableObject
{
    #region Variables
    public CharacterController3D characterController { get; set; }
    #endregion

    #region Custom Methods
    public abstract void Init();
    public abstract void OnCharacterUpdate();
    public abstract void OnCharacterFixedUpdate();
    #endregion
}
