using UnityEngine;

public struct MovementParameters
{
    public Vector3 position;
    public Quaternion? lookRotation;

    public MovementParameters(Vector3 position)
    {
        this.position = position;
        this.lookRotation = null;
    }

    public MovementParameters(Vector3 position, Quaternion lookRotation)
    {
        this.position = position;
        this.lookRotation = lookRotation;
    }
}