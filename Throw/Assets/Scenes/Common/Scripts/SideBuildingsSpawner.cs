using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBuildingsSpawner : MonoBehaviour
{
    [SerializeField] private SideBuilding[] BuildingPrefabs;
    [SerializeField] private float XCoordinate;
    [SerializeField] private float ZGap;
    [SerializeField] private int ZBuildingCount;
    
    void Start()
    {
        for (int i = 0; i < ZBuildingCount; i++)
        {
            float doorZ = i * ZGap;

            SpawnBuilding(XCoordinate, doorZ, 0);
            SpawnBuilding(-XCoordinate, doorZ, 180);
        }
    }

    // Helpers
    private void SpawnBuilding(float doorX, float doorZ, float rotationY)
    {
        int buildingIndex = Random.Range(0, BuildingPrefabs.Length);

        GameObject newObject = Instantiate(BuildingPrefabs[buildingIndex].gameObject);
        newObject.transform.position = new Vector3(doorX, 0, doorZ);
        newObject.transform.Rotate(new Vector3(0, rotationY, 0));
    }
}
