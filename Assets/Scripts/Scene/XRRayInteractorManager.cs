using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace Assets.Scripts.Scene
{
    public class XRRayInteractorManager : ScriptableObject
    {
        private XRRayInteractor _rayInteractor;

        private Vector2 previousHit;

        public XRRayInteractorManager(XRRayInteractor rayInteractor)
        {
            _rayInteractor = rayInteractor;
        }

        ~XRRayInteractorManager()
        {
            _rayInteractor = null;
        }

        /**
         * <summary>
         * Проверка на нажатие плоскости с возвращением координат попадания
         * </summary>
         */
        public void CheckObjectsHit<T>(ObjectsManager objectsManager, out T? target) where T : struct
        {
            if (_rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit) && 
                    hit.transform.gameObject.tag != "Object") // Не учитываем игровые объекты
            {
                switch (typeof(T))
                {
                    case Type t when t == typeof(Vector2):
                        target = (T)(object)new Vector2(hit.point.x, hit.point.z);
                        break;
                    case Type t when t == typeof(Vector3):
                        target = (T)(object)new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        break;
                    default:
                        throw new ArgumentException($"Unsupported vector type: {typeof(T)}");
                }

                Vector2 target2d = new Vector2(hit.point.x, hit.point.z);

                if (previousHit != null && !target2d.Equals(previousHit))
                {
                    previousHit = new Vector2(hit.point.x, hit.point.z);
                }
                else
                {
                    target = null;
                }
            }
            else
            {
                target = null;
            }
        }
    }
}