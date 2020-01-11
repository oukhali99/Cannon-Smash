using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBuildingsSpawner : MonoBehaviour
{
    [SerializeField] private SideBuilding[] BuildingPrefabs;
    [SerializeField] private float ZGap;
    [SerializeField] private int ZBuildingCount;
    [SerializeField] private float XDistanceFromCurb;
    [SerializeField] private float XDistanceFromCenterToCurbEnd;
    [SerializeField] private float XCenter;

    private float nextZ;
    
    void Start()
    {
        PopulateRow(XDistanceFromCurb + XCenter, 0);
        PopulateRow(-XDistanceFromCurb + XCenter, 180);
    }

    // Helpers
    private void PopulateRow(float xCoordinate, float yRotation)
    {
        if (yRotation == 0)
        {
            xCoordinate += XDistanceFromCenterToCurbEnd;
        }
        else
        {
            xCoordinate -= XDistanceFromCenterToCurbEnd;
        }

        nextZ = 0;
        for (int i = 0; i < ZBuildingCount; i++)
        {
            SideBuilding chosenBuilding = GetRandomBuilding(BuildingPrefabs);
            float realZLength = chosenBuilding.ZLength * chosenBuilding.transform.localScale.x;
            float nextZIncrement = realZLength / 2;

            nextZ += nextZIncrement;

            GameObject newObject = Instantiate(chosenBuilding.gameObject, transform);
            newObject.transform.position = new Vector3(xCoordinate, 0, nextZ);
            newObject.transform.Rotate(new Vector3(0, yRotation, 0));

            nextZ += nextZIncrement;
            nextZ += ZGap;
        }
    }

    private SideBuilding GetRandomBuilding(SideBuilding[] sideBuildings)
    {
        SideBuilding chosenBuilding = sideBuildings[Random.Range(0, sideBuildings.Length)];
        float gamble = Random.value;

        if (gamble < chosenBuilding.Prevalence)
        {
            return chosenBuilding;
        }
        else
        {
            return GetRandomBuilding(sideBuildings);
        }
    }
}
