﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBuilding : MonoBehaviour
{
    public StructureBlueprint windmillStructure;
    public StructureBlueprint solarPanelStructure;

    List<StructureBlueprint> listOfBlueprints = new List<StructureBlueprint>();



    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;

        listOfBlueprints.Add(windmillStructure);
    }

    public void SelectShowInfoWindmill()
    {
        windmillStructure.informationPanel.SetActive(true);
    }
    public void HideInfoWindmill()
    {
        windmillStructure.informationPanel.SetActive(false);

    }

    public void SelectShowInfoSolarpanel()
    {
        solarPanelStructure.informationPanel.SetActive(true);
    }
    public void HideInfoSolarpanel()
    {
        solarPanelStructure.informationPanel.SetActive(false);

    }

    public void SelectWindmillStructure()
    {
        buildManager.SelectStructureToBuild(windmillStructure);
        Debug.Log("Selected windmill");
    }

    public void SelectSolarPanelStructure()
    {
        buildManager.SelectStructureToBuild(solarPanelStructure);
        Debug.Log("Selected solar panel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
