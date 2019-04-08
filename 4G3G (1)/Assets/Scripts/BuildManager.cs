﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [Header("Panels and UI")]
    public GameObject cityHall;
    public GameObject uiAllQuests;
    public GameObject buildPanel;

    public GameObject infoPanel;
    public GameObject destroyPanelSelection;

    public bool destroyActivate = false;

    private StructureBlueprint structureToBuild;
    private NodeTouch selectedNode;

    public NodeUI nodeUI;

    public bool haveCityHall = false;

    private Player player;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than a one buildmanager in the scene");
            return;
        }
        instance = this;

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void BuildStructureOn(NodeTouch node)
    {
        if (!HasEnoughMoney())
        {
            Debug.Log("Not enough money!");
            return;
        }

        player.DecreaseMoney(structureToBuild.cost);
        player.IncreaseHappiness(structureToBuild.amountOfHappiness);
        player.IncreasePollution(structureToBuild.amountOfPollution);
        player.IncreaseEnergy(structureToBuild.amountOfEnergy);

        GameObject structure = (GameObject)Instantiate(structureToBuild.prefab, node.GetBuildPosition(), structureToBuild.prefab.transform.rotation);

        node.buildingThere = structure;
        structureToBuild.nodeAsociate = node;

        Debug.Log("CONSTRUIDO");
        //Hide the node
        node.gameObject.GetComponent<MeshRenderer>().enabled = false;

        structureToBuild = null;

    }

    bool HasEnoughMoney()
    {
        return structureToBuild.cost < player.GetTotalMoney();
    }
    void BuildCityHall(NodeTouch node)
    {
        GameObject structure = Instantiate(cityHall, node.GetBuildPosition(), cityHall.transform.rotation);

        node.buildingThere = structure;

        Debug.Log("City hall did it");
        node.gameObject.GetComponent<MeshRenderer>().enabled = false;

        player.IncreaseMoney(35000);

        structureToBuild = null;
    }
    public void HideUIQuest()
    {
        uiAllQuests.SetActive(false);
    }
    public void SelectNode(NodeTouch node)
    {
        if (node.tag == "CityPlace" && haveCityHall == false)
        {
            BuildCityHall(node);
            DeselectNode();
            haveCityHall = true;
            return;
        }
        if (haveCityHall)
        {
                if (selectedNode == node)
                {
                    //node.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    DeselectNode();
                    return;
                }
                if(node.isUnlock == false)
                {
                     DeselectNode();
                     return;
                }
                if (structureToBuild == null && node.buildingThere == null)
                {
                    buildPanel.SetActive(true);
                    HideInfoPanel();
                    selectedNode = node;
                    return;
                }
                if (structureToBuild != null && node.buildingThere == null)
                {
                    BuildStructureOn(node);
                    //buildPanel.SetActive(false);
                    DeselectNode();
                    structureToBuild = null;
                    return;
                }

                if(node.buildingThere != null && node.buildingThere.tag != "House" && node.buildingThere.tag != "CityHall")
                {
                    selectedNode = node;
                    infoPanel.SetActive(true);
                    HideConstructionPanel();
                    return;
                }

               //if (node.buildingThere.tag == "Factory")
               //{
               //    DeselectNode();
               //    selectedNode = node;
               //    structureToBuild = null;
               //    nodeUI.SetTargetReplaceFactory(node);
               //    return;
               //}
                if (node.buildingThere.tag == "CityHall")
                {
                    Debug.Log("Menu city hall");
                    DeselectNode();
                    uiAllQuests.SetActive(true);
                    return;
                }
               //else //House
               //{
               //    DeselectNode();
               //    Debug.Log("Is not a factory");
               //    selectedNode = node;
               //    structureToBuild = null;
               //    //nodeUI.SetTargetHouses(node);
               //}
            }
            
           
       
       
    }
    public void DestroyButton()
    {
        destroyActivate = true;
        destroyPanelSelection.SetActive(true);
        HideInfoPanel();


    }

    public void DestroyThisBuilding()
    {
        if(selectedNode != null)
        {
            Destroy(selectedNode.buildingThere);
            selectedNode.gameObject.GetComponent<MeshRenderer>().enabled = true;
            HideInfoPanel();
            NotDestroyThisBuilding();
        }
        
    }

    public void NotDestroyThisBuilding()
    {
        destroyPanelSelection.SetActive(false);
        destroyActivate = false;
    }

    public StructureBlueprint GetStructureToBuild()
    {
        return structureToBuild;
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.HideReplaceFactory();
        nodeUI.HideHouses();
        HideInfoPanel();
        HideConstructionPanel();
    }

    public void HideInfoPanel()
    {
        infoPanel.SetActive(false);

    }

    public void HideConstructionPanel()
    {
        buildPanel.SetActive(false);
    }

    public bool ConstructionPanelActivated()
    {
        return buildPanel.activeSelf;
    }


    public void SelectStructureToBuild (StructureBlueprint structure)
    {
        structureToBuild = structure;
        
        DeselectNode();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
