using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBuildingsSpawner : MonoBehaviour
{
    [SerializeField] private SideBuilding[] BuildingPrefabs;
    [SerializeField] private float XCoordinate;
    [SerializeField] private float ZGap;
    [SerializeField] private int ZBuildingCount;
    [SerializeField] private float XDistanceFromCenterToCurbEnd;

    private float nextZ;
    
    void Start()
    {
        PopulateRow(XCoordinate, 0);
        PopulateRow(-XCoordinate, 180);
    }

    // Helpers
    private void PopulateRow(float xCoordinate, float yRotation)
    {
        if (xCoordinate > 0)
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

            GameObject newObject = Instantiate(chosenBuilding.gameObject);
            newObject.transform.position = new Vector3(xCoordinate, 0, nextZ);
            newObject.transform.Rotate(new Vector3(0, yRotation, 0));

            nextZ += nextZIncrement;
            nextZ += ZGap;

            // Resize road
            SideBuilding sideBuildingScript = newObject.GetComponent<SideBuilding>();
            if (sideBuildingScript.Path != null)
            {
                Transform roadTransform = sideBuildingScript.Path.transform;
                float requiredXScaleMultiplier;

                if (xCoordinate > 0)
                {
                    requiredXScaleMultiplier = (xCoordinate - XDistanceFromCenterToCurbEnd) / roadTransform.lossyScale.x;
                }
                else
                {
                    requiredXScaleMultiplier = -(xCoordinate + XDistanceFromCenterToCurbEnd) / roadTransform.lossyScale.x;
                }

                roadTransform.localScale = new Vector3(roadTransform.localScale.x * requiredXScaleMultiplier, roadTransform.localScale.y, roadTransform.localScale.z);
            }
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
