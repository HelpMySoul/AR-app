using UnityEngine;

namespace Assets.Scripts.SceneObjects
{    
    public interface IFootballer
    {        
        void SetTargetPosition(Vector2 targetPosition);

        float MoveSpeed         { get; set; }
        float RotationSpeed     { get; set; }
        float RotationThreshold { get; set; }
        float BounceForce       { get; set; }
        bool IsMoving           { get; }
        bool IsRotating         { get; }   
    }

    public interface IFootballerInternalActions
    {
        void StartMovement(bool value);
        void StartRotation(bool value);
        bool HasReachedPosition(Vector3 targetPosition);
        bool HasReachedPosition(Vector2 targetPosition);
    }

}