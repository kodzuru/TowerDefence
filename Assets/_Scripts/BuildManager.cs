using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    

    #region SINGLETON
    /*-------------------СИНГЛТОН START-------------------*/
    /**
     * Одиночка (Singleton, Синглтон) - порождающий паттерн, 
     * который гарантирует, что для определенного класса будет создан только один объект, 
     * а также предоставит к этому объекту точку доступа.         
     **/
    public static BuildManager instance; //создаём единственный объект

    private void Awake()
    {
        //проверка на существование более одного объекта
        if (instance != null)
        {
            Debug.LogError("More then one BuildManager in scene!");
            return;
        }
        instance = this; //ссылка на объект
    }
    /*-------------------СИНГЛТОН END-------------------*/
    #endregion


    //public GameObject standardTurretPrefab;//ссылка на префаб вышки которую хотим построить
    //public GameObject missleLauncherPrefab;

    public GameObject buildEffect;//префаб частиц когда строишь вышку
    public GameObject sellEffect;//префаб частиц когда продаём вышку

    /*
    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }
    */

    TurretBlueprint turretToBuild; // объект который мы хотим построить
    Node selectedNode;//выделяемая нода

    public NodeUI nodeUI;//ссылка на NodeUI объект

    public bool CanBuild
        //можем ли построить вышку на выбранной ноде
    {
        get { return turretToBuild != null; }
    }
    public bool HasMoney
    //хватает ли бабла на вышку
    {
        get { return PlayerStats.Money >= turretToBuild.cost; }
    }
    /*
    public GameObject GetTorretToBuild()
        //для получения объекта который необходимо\хотим построить
    {
        return turretToBuild;
    }
    
    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }
    */


   public void SelectNode(Node node)
    {
        //если нажимаем на ноду, но она уже выбрана
        if (selectedNode == node)
        {
            DeselectNode();//отключаем выбранную ноду
            return;
        }
        //выбираем ноду
        selectedNode = node;
        Debug.Log(selectedNode.name);
        //не выбираем вышку
        //выбрав ноду с вышкой больше нельзя строить
        turretToBuild = null;

        //передаём ноду в класс nodeUI
        nodeUI.SetTarget(node);


    }

    public void DeselectNode()
    {
        selectedNode = null; 
        nodeUI.Hide();//скрываем NodeUI интерфейс
    }


    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        //выбираем вышку
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
