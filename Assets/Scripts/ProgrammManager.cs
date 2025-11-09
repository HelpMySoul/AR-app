using Assets.Scripts;
using Assets.Scripts.Scene;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using static Assets.Scripts.ProjectEnums;

public class ProgrammManager : MonoBehaviour
{
    // Для отслеживания нажатия на плоскость
    private XRRayInteractorManager rayInteractorManager;
    [SerializeField] private XRRayInteractor rayInteractor;

    // Для общих действий с объектами
    private ObjectsManager objectsManager;
    [SerializeField] private GameObject sceneObjects;

    public static UIManager uIManager;
    [SerializeField] private GameObject uIManagerObjects;

    // Текущая стратегия нажатия
    public static Strategies Strategy { get; set; }

    ~ProgrammManager() 
    {
        rayInteractorManager = null;
        rayInteractor        = null;
        objectsManager       = null;
        sceneObjects         = null;
    }

    private void Awake()
    {
        objectsManager = new ObjectsManager();
        Strategy       = Strategies.None;
        uIManager      = uIManagerObjects.GetComponent<UIManager>();
    }

    void Start()
    {
        SetObjects();        
        rayInteractorManager = new XRRayInteractorManager(rayInteractor);
    }

    void Update()
    {
        // Обработка нажатий
        switch (Strategy)
        {
            case Strategies.None:
                break;

            case Strategies.Movement:
                Vector2? targetVector2;
                rayInteractorManager.CheckObjectsHit(objectsManager, out targetVector2);
                if (targetVector2 == null)              
                    break;
                
                objectsManager.GetFootballer().SetTargetPosition((Vector2)targetVector2);
                break;

            case Strategies.GatesEdit:
                Vector3? targetVector3;
                rayInteractorManager.CheckObjectsHit(objectsManager, out targetVector3);
                if (targetVector3 == null)
                    break;

                objectsManager.GetGates().SetPosition((Vector3)targetVector3);
                objectsManager.GetGates().SetRotation();
                break;
        }                 
    }

    private void FixedUpdate()
    {

    }

    void SetObjects()
    {
        List<GameObject> childObjects = new List<GameObject>();

        for (int i = 0; i < sceneObjects.transform.childCount; i++)
        {
            childObjects.Add(sceneObjects.transform.GetChild(i).gameObject);
        }

        objectsManager.SetObjects(childObjects);
    }

    public void ResetObjects()
    {
        objectsManager.ResetAll();
    }
}
