using Assets.Scripts.Scene.Objects;
using Assets.Scripts.SceneObjects;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager
{
    private IFootballer      _footballer;
    private IGates           _gates;
    private List<GameObject> _objects;

    ~ObjectsManager()
    {
        _objects.Clear();
        _footballer = null;
        _gates      = null;
    }

    public void SetObjects(List<GameObject> objects)
    {
        _objects    = objects;
        _footballer = null;
        _gates      = null;

        foreach (GameObject obj in objects)
        {
            if (_footballer != null && _gates != null)
                break;

            if (_footballer == null &&
                HasComponent(obj, out _footballer))                        
                    continue;
            

            if (_gates == null &&           
                HasComponent(obj, out _gates))
                    continue;            
        }
    }

    private bool HasComponent<T>(GameObject gameobject, out T component)
    {       
        if (gameobject.TryGetComponent(out T foundComponent))
        {
            component = foundComponent;
            return true;
        }

        component = default(T);
        return false;
    }

    public IFootballer GetFootballer()
    {
        return _footballer;
    }

    internal IGates GetGates()
    {
        return _gates;
    }

    public void ResetAll()
    {
        foreach ( GameObject obj in _objects) 
        { 
            obj.GetComponent<SceneObject>().ResetPosition();
        }
    }
}