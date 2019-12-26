using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBuildingsSpawner : MonoBehaviour
{
    [SerializeField] private SideBuilding[] BuildingPrefabs;
    [SerializeField] private float XCoordinate;
    [SerializeField] private float ZGap;
    [SerializeField] private int ZBuildingCount;

    private float nextZ;
    
    void Start()
    {
        PopulateRow(XCoordinate, 0);
        PopulateRow(-XCoordinate, 180);
    }

    // Helpers
    private void PopulateRow(float XCoordinate, float YRotation)
    {
        nextZ = 0;
        for (int i = 0; i < ZBuildingCount; i++)
        {
            SideBuilding chosenBuilding = GetRandomBuilding(BuildingPrefabs);
            float realZLength = chosenBuilding.ZLength * chosenBuilding.transform.localScale.x;
            float nextZIncrement = realZLength / 2;

            nextZ += nextZIncrement;

            GameObject newObject = Instantiate(chosenBuilding.gameObject);
            newObject.transform.position = new Vector3(XCoordinate, 0, nextZ);
            newObject.transform.Rotate(new Vector3(0, YRotation, 0));

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
