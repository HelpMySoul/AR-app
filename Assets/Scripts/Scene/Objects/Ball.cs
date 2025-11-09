using Assets.Scripts.Scene;
using Assets.Scripts.Scene.Interfaces;
using Assets.Scripts.Scene.Objects;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.SceneObjects
{
    public class Ball : SceneObject
    {
        public override void FixedActions()
        {
            base.FixedActions();
        }

        private void OnTriggerExit(Collider other)
        {
            other.gameObject.transform.parent.TryGetComponent(out FootballGoals goals); // Вернет не null, если попали в ворота

            if (goals != null)
            {
                ResetPosition();
                SessionInfo.Goal();
            }
        }
    }
}