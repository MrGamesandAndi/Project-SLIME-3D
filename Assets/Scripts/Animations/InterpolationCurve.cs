using UnityEngine;

[CreateAssetMenu(fileName = "InterpolationCurve", menuName = "Extras/InterpolationCurve")]
public class InterpolationCurve : ScriptableObject
{
    #region Variables
    public AnimationCurve curve;
    #endregion

    #region Custom Methods
    public float Evaluate(float time)
    {
        return curve.Evaluate(time);
    }

    public float Interpolate(float from, float to, float time)
    {
        return from + (to - from) * Evaluate(time);
    }

    public Vector2 Interpolate(Vector2 from, Vector2 to, float time)
    {
        return from + (to - from) * Evaluate(time);
    }

    public Vector3 Interpolate(Vector3 from, Vector3 to, float time)
    {
        return from + (to - from) * Evaluate(time);
    }

    public Color Interpolate(Color from, Color to, float time)
    {
        return from + (to - from) * Evaluate(time);
    }
    #endregion
}
