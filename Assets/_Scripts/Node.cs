using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Color hoverColor; // цвет по наведению на ноду
    public Color notEnoughMoneyColor;//цвет при нехватке денег на строительство вышки
    public Vector3 positionOffset; //позиция вышки по отношению к ноде

    Renderer rend; //ссылка на рендер объекта ноды
    Color startColor; //первоначальный цве ноды
   

    [HideInInspector]
    public GameObject turret; //ссылка на объект вышки

    [HideInInspector]
    public TurretBlueprint turretBlueprint;//ссылка на параметры вышки

    [HideInInspector]
    public bool isUpgraded = false; //есть ли уже апгрейд у вышки


    BuildManager buildManager;// ссылка на класс строителя вышек


    void Start()
    {
        //setup references
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;

    }

    void OnMouseDown()
    {

        //если иконка с вышкой находится над нодой и мы на неё кликаем, то вышка не строится, а кликается кнопка
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        //если вышка уже существует, её можно продать или апгрейдить
        if (turret != null)
        {
            //мы больше не можем ничего построить в этой ноде
            Debug.Log("Can't buid there! - TODO: Display on screen.");
            //отправляем ноду с вышкой в BuildManager
            buildManager.SelectNode(this);

            return;
        }

        //если не выбрано ни одной выки
        if (!buildManager.CanBuild)
            return;

        /*
        //стром вышку 
        //получаем объект вышки которую выбрали из BuildManager
        GameObject turretToBuild = buildManager.GetTorretToBuild();
        
        //строим вышку
        turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation) as GameObject;
        */
        //выбрали ноду на которой строим вышку и отправляем ссылку на неё в BuildManager
        //buildManager.BuildTurretOn(this);
        //передаём в функцию какую вышку мы хотим пострить
        BuildTorret(buildManager.GetTurretToBuild());

    }

    void BuildTorret(TurretBlueprint blueprint)
    {
        //если не хватает бабла на постройку вышки
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to buld that!");
            return;
        }
        //снимаем бабло и строим вышку
        PlayerStats.Money -= blueprint.cost;

        //строим вышку
        GameObject _turret = Instantiate(blueprint.prefab, GetBuldPosition(), Quaternion.identity) as GameObject;
        turret = _turret; //отправляем выбранную вышку в ноду

        //ссылка на объект вышки которую строим на ноде
        turretBlueprint = blueprint;

        //эффект строительства на ноде
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuldPosition(), Quaternion.identity) as GameObject;
        Destroy(effect, 2f);//удаляем эффект частиц

        Debug.Log("Turret Build!");
    }

    public void UpgradeTurret()
    {
        //если не хватает бабла на upgrade вышки
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to UPGRADE that!");
            return;
        }
        //снимаем бабло и строим вышку
        PlayerStats.Money -= turretBlueprint.upgradeCost;

        //разрушаем вышку которая уже существует и строим на её месте новую
        Destroy(turret);

        //строим вышку
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuldPosition(), Quaternion.identity) as GameObject;
        turret = _turret; //отправляем выбранную вышку в ноду

        //эффект строительства на ноде
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuldPosition(), Quaternion.identity) as GameObject;
        Destroy(effect, 2f);//удаляем эффект частиц

        //вышка апгрейджена
        isUpgraded = true;



        Debug.Log("Turret Build!");
    }

    public void SellTurre()
    {
        //добавляем бабло за продажу в нашу копилку
        PlayerStats.Money += turretBlueprint.sellCost;
        //уничтожаем вышку
        Destroy(turret);
        turretBlueprint = null;
        //эффект удаления вышки на ноде
        GameObject effect = Instantiate(buildManager.sellEffect, GetBuldPosition(), Quaternion.identity) as GameObject;
        Destroy(effect, 2f);//удаляем эффект частиц

        isUpgraded = false;//вышка не пагрейджена она продона

    }


    void OnMouseEnter()
        //вызывается всякий раз когда мышка пересекает коллайдер
    {
        //если иконка с вышкой находится над нодой и мы на неё кликаем, то вышка не строится, а кликается кнопка
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        

        //если не выбрано ни одной выки
        if (!buildManager.CanBuild)
            return;

        //если хватает денего на вышку
        if (buildManager.HasMoney)
        {
            //красим ноду
            rend.material.color = hoverColor;
        }
        else
        {
            //возвращаем первоначальный цвет
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    //вызывается всякий раз когда мышка покидает коллайдер
    {
        rend.material.color = startColor; //покравить объект в цвет
    }

    public Vector3 GetBuldPosition()
    {
        return transform.position + positionOffset;
    }
}
