using Assets.Scripts.Scene.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.Scene.Objects
{
    public abstract class SceneObject : MonoBehaviour, IObject
    {
        protected Rigidbody _rigidbody;

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        public virtual void Init()
        {

        }

        void Start()
        {
            if (!TryGetComponent(out _rigidbody))
            {
                return;
            }
            Init();
        }

        void Update()
        {

        }
        private void FixedUpdate()
        {
            FixedActions();
        }

        public virtual void FixedActions()
        {
            if (_rigidbody == null)
            {
                return;
            }

            FallPrevent();  // Возврат выпавших со сцены объектов
        }

        public void FallPrevent()
        {          
           
            if (transform.position.y < -3)
            {
                ResetPosition();
            }
        }

        public void ResetPosition()
        {
            _rigidbody.linearVelocity = Vector3.zero;
            transform.position = new Vector3(0, 3, 0);
        }
    }
}