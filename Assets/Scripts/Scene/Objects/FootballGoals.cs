using Assets.Scripts.Scene.Interfaces;
using Assets.Scripts.Scene.Objects;
using Assets.Scripts.SceneObjects;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class FootballGoals : SceneObject, IGates
    {
        public override void FixedActions()
        {
            base.FixedActions();
        }

        public void SetPosition(Vector3 position)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            transform.position = position;
        }

        public override void Init()
        {
            base.Init();
            gameObject.SetActive(false);
        }

        public void SetRotation()
        {
            Vector3 directionToCenter = Vector3.zero - transform.position;

            if (directionToCenter != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToCenter);

                transform.rotation = lookRotation * Quaternion.Euler(0, 90, 0);
            }
        }
    }
}