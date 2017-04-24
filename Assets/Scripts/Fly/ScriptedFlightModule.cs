using UnityEngine;

[System.Serializable]
public struct FlightControlModule
{
    //-----------------Inspector Variables-----------------
    [SerializeField]
    public float
        speedMin,
        speedMax,
        drag,
        speed;

    [SerializeField]
    [Range(0f, 1f)]
    public float
        lerpSpeed;

    [SerializeField]
    public Transform
        target;

    public Vector3 GetDirectionVector(Vector3 position)
    {
        return (target.position - position) * speed;
    }

    public Vector3 ClampForce(Vector3 force)
    {
        if (force.magnitude > speedMax)
            force = force.normalized * speedMax;
        else if (force.magnitude < speedMin)
            force = force.normalized * speedMin;
        return force;
    }
}