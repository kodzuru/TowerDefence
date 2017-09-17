using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NodeUI : MonoBehaviour
{

    public GameObject ui; //ссылка на Canvas : NodeUI -> child

    public Button upgradeButton; //кнопка апгрейда

    [Header("Turrest cost options")]
    public Text upgradeCost;//обновняем текст стоимости апгрейда
    public Text sellCost;//обновняем текст продажи вышки






    Node target;//нода которую мы выбрали

    public void SetTarget(Node _target)
    {
        //получаем объект ноды
        target = _target;
        //получаем позицию центра ноды на которой строится вышка
        transform.position = target.GetBuldPosition();

        //если доступен апгрейд
        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;//стоимость
            upgradeButton.interactable = true;//кнопка активна
        }
        else
        {
            upgradeCost.text = "NO MORE!";//больше нет апгрейдов
            upgradeButton.interactable = false;//кнопка не активна
        }

        sellCost.text = "$" + target.turretBlueprint.sellCost; //обновляем текст на кнопке продажи

        ui.SetActive(true);//show UI
    }

    public void Hide()
    {
        ui.SetActive(false);//hide UI
    }

    public void Upgrade()
    {
        target.UpgradeTurret();//вызываем функцию апгрейда вышки
        BuildManager.instance.DeselectNode();//выключаем меню после апгрейда
    }

    public void Sell()
    {
        target.SellTurre();//продаём вышку
        BuildManager.instance.DeselectNode();//выключаем меню после апгрейда
        Hide();//скрываем меню
    }

}
