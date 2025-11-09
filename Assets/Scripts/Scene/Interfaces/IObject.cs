using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Scene.Interfaces
{
    public interface IObject
    {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
    }
}