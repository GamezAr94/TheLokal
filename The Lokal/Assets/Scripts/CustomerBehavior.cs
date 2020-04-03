using UnityEngine;
public enum Order { Capuccino = 1, Americano, Late, IceLate};
public enum CurrentState { Comming = 0, Ordering, FindingSpot, Waiting, Eating, Going, Line , MovingTheLine = 7 };
public interface CustomerBehavior
{
    Vector3 GetNextStop();
    int GetCurrentState();

    void DestroyItself();


}
