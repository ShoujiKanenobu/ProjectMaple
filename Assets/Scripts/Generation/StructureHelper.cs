using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generation
{
    public class StructureHelper : MonoBehaviour
    {
        public BulidingTypes[] buildingTypes;
        public GameObject[] naturePrefabs;
        public bool randomNaturePlacement = false;
        [Range(0, 1)]
        public float randomNaturePlacementThreshold = 0.3f;
        public Dictionary<Vector3Int, GameObject> structuresDictionary = new Dictionary<Vector3Int, GameObject>();
        public Dictionary<Vector3Int, GameObject> natureDictionary = new Dictionary<Vector3Int, GameObject>();

        public void PlaceStructuresAroundRoad(List<Vector3Int> roadPositions)
        {
            Dictionary<Vector3Int, Direction> freeEstateSpots = FindFreeSpacesAroundRoad(roadPositions);
            List<Vector3Int> blockedPositions = new List<Vector3Int>();
            foreach(var freespot in freeEstateSpots)
            {
                if (blockedPositions.Contains(freespot.Key))
                    continue;

                var rotation = Quaternion.identity;
                switch (freespot.Value)
                {
                    case Direction.Up:
                        rotation = Quaternion.Euler(0, 180, 0);
                        break;
                    case Direction.Left:
                        rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case Direction.Right:
                        rotation = Quaternion.Euler(0, -90, 0);
                        break;
                    default:
                        break;
                }
                for (int i = 0; i < buildingTypes.Length; i++)
                {
                    if(buildingTypes[i].quantity == -1)
                    {
                        if(randomNaturePlacement)
                        {
                            var random = UnityEngine.Random.value;
                            if (random < randomNaturePlacementThreshold)
                            {
                                var nature = SpawnPrefab(naturePrefabs[UnityEngine.Random.Range(0, naturePrefabs.Length)], 
                                    freespot.Key, rotation);
                                natureDictionary.Add(freespot.Key, nature);
                                break;
                            }
                        }
                        var building = SpawnPrefab(buildingTypes[i].GetPrefab(), freespot.Key, rotation);
                        structuresDictionary.Add(freespot.Key, building);
                        break;
                    }
                    if(buildingTypes[i].IsBuildingAvailable())
                    {
                        if(buildingTypes[i].sizeRequired > 1)
                        {
                            var halfSize = Mathf.FloorToInt(buildingTypes[i].sizeRequired / 2.0f);
                            List<Vector3Int> tempPositionsBlocked = new List<Vector3Int>();
                            if(VerifyIfBuildingFits(halfSize, freeEstateSpots, freespot, blockedPositions, ref tempPositionsBlocked))
                            {
                                blockedPositions.AddRange(tempPositionsBlocked);
                                var building = SpawnPrefab(buildingTypes[i].GetPrefab(), freespot.Key, rotation);
                                structuresDictionary.Add(freespot.Key, building);
                                foreach(var pos in tempPositionsBlocked)
                                {
                                    structuresDictionary.Add(pos, building);
                                }
                                break;
                            }
                        }
                        else
                        {
                            var building = SpawnPrefab(buildingTypes[i].GetPrefab(), freespot.Key, rotation);
                            structuresDictionary.Add(freespot.Key, building);
                        }
                        break;
                    }
                }
            }
        }

        private bool VerifyIfBuildingFits(int halfSize, 
            Dictionary<Vector3Int, Direction> freeEstateSpots, 
            KeyValuePair<Vector3Int, Direction> freespot, 
            List<Vector3Int> blockedPositions,
            ref List<Vector3Int> tempPositionsBlocked)
        {
            Vector3Int direction = Vector3Int.zero;
            if (freespot.Value == Direction.Down || freespot.Value == Direction.Up)
            {
                direction = Vector3Int.right;
            }
            else
                direction = new Vector3Int(0, 0, 1);
            for (int i = 1; i <= halfSize; i++)
            {
                var pos1 = freespot.Key + direction * i;
                var pos2 = freespot.Key - direction * i;
                if (!freeEstateSpots.ContainsKey(pos1) || !freeEstateSpots.ContainsKey(pos2) || 
                    blockedPositions.Contains(pos1) || blockedPositions.Contains(pos2))
                    return false;

                tempPositionsBlocked.Add(pos1);
                tempPositionsBlocked.Add(pos2);
            }
            return true;
        }

        private GameObject SpawnPrefab(GameObject prefab, Vector3Int position, Quaternion rotation)
        {
            var newStrucutre = Instantiate(prefab, position, rotation, transform);
            return newStrucutre;
        }

        private Dictionary<Vector3Int, Direction> FindFreeSpacesAroundRoad(List<Vector3Int> roadPositions)
        {
            Dictionary<Vector3Int, Direction> freeSpots = new Dictionary<Vector3Int, Direction>();
            foreach (var position in roadPositions)
            {
                var neighborDirections = PlacementHelper.FindNeighbor(position, roadPositions);
                foreach(Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    if(neighborDirections.Contains(direction) == false)
                    {
                        var newPosition = position + PlacementHelper.GetOffsetFromDirection(direction);
                        if(freeSpots.ContainsKey(newPosition))
                        {
                            continue;
                        }
                        freeSpots.Add(newPosition, PlacementHelper.GetReverseDirection(direction));
                    }
                }
            }
            return freeSpots;
        }
    }
}


