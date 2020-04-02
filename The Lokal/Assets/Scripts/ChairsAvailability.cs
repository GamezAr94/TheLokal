using UnityEngine;

public class ChairsAvailability : MonoBehaviour
{
    [SerializeField]
    private bool isAvailable = true;
    [SerializeField]
    private Vector3 thisPosition;

    public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
    public Vector3 ThisPosition { get => thisPosition; set => thisPosition = value; }

    public void Awake()
    {
        ActualPositionChair();
    }
    public void ActualPositionChair()
    {
        ThisPosition = this.transform.position;
    }


}
