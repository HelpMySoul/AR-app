using Assets.Scripts.Scene.Interfaces;
using Assets.Scripts.Scene.Objects;
using NUnit.Framework.Constraints;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.SceneObjects
{
    public class Footballer : SceneObject, IFootballer, IFootballerInternalActions
    {
        private Animator footballerAnimator;
        private ParticleSystem footballerParticleSystem;

        private bool isMoving   = false;
        private bool isRotating = false;
        private Vector3 targetPosition;

        [Header("Footballer maneuverability settings")]
        [SerializeField] private float moveSpeed         = 1;
        [SerializeField] private float rotationSpeed     = 5;
        [SerializeField] private float rotationThreshold = 5f;
        [SerializeField] private float tolerance         = 0.05f;

        [Header("Impact force settings")]
        [SerializeField] private float bounceForce = 10f;

        public override void Init()
        {
            base.Init();
            footballerAnimator       = GetComponent<Animator>();
            footballerParticleSystem = GetComponent<ParticleSystem>();
        }  

        public float MoveSpeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }

        public float RotationSpeed
        {
            get => rotationSpeed;
            set => rotationSpeed = value;
        }

        public float RotationThreshold
        {
            get => rotationThreshold;
            set => rotationThreshold = value;
        }

        public float BounceForce
        {
            get => bounceForce;
            set => bounceForce = value;
        }

        public bool IsMoving 
        {
            get => isMoving;
            set => isMoving = value;
        }

        public bool IsRotating
        {
            get => isRotating;
            set => isRotating = value;
        }         

        public void StartMovement(bool value)
        {            
            footballerAnimator.SetBool("Run", value);

            IsMoving = value;

            if (isMoving)
                footballerParticleSystem.Play();
            else
                footballerParticleSystem.Stop();            
        }

        public void StartRotation(bool value)
        {
            isRotating = value;
        }
        public void SetTargetPosition(Vector2 targetPosition)
        {
            if (HasReachedPosition(targetPosition))
                return;
            
            StartMovement(true);
            StartRotation(true);
            this.targetPosition = new Vector3(targetPosition.x, Position.y, targetPosition.y);
        }        

        public bool HasReachedPosition(Vector3 targetPosition)
        {
            Vector2 target2dPos = new Vector2(targetPosition.x, targetPosition.z);
            return HasReachedPosition(target2dPos);
        }

        public bool HasReachedPosition(Vector2 targetPosition)
        {
            Vector2 current2dPos = new Vector2(Position.x, Position.z);
            return Vector2.Distance(current2dPos, targetPosition) <= tolerance;
        }

        public override void FixedActions()
        {
            base.FixedActions();

            if (!IsMoving) return;

            // Для завершения передвижения
            if (HasReachedPosition(targetPosition))
            {
                StartMovement(false);
                StartRotation(false);
                return;
            }            

            Rotate();
            Move();            
        }

        private void Rotate()
        {
            Vector3 direction = (targetPosition - Position).normalized;
            direction.y = 0;

            if (IsRotating)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                float angle = Quaternion.Angle(Rotation, targetRotation);

                if (angle > RotationThreshold)
                {
                    transform.rotation = Quaternion.Slerp(Rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    transform.rotation = targetRotation;
                    isRotating = false;
                }
            }
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(Position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            Vector3 currentDirection = (targetPosition - Position).normalized;

            float currentAngle = Vector3.Angle(transform.forward, currentDirection);
            if (currentAngle > RotationThreshold * 1.5f)
            {
                isRotating = true;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                return;
            }

            ContactPoint contact = collision.contacts[0];

            Vector3 direction = (contact.point - Position).normalized;
            direction.y = 0f;

            rigidbody.AddForce(direction * bounceForce, ForceMode.Impulse);
        }
       
    }
}