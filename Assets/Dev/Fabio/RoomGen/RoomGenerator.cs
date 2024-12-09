using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    private Table table;

    [Header("Materials")]
    public Material MBonus;
    public Material MCombat;
    public Material MEntrance;
    public Material MExit;
    public Material MParkour;
    public Material MShop;
    public Material MMoldOrb;
    public Material MWaveSurvival;

    void Start() {
        table = new();
        Generate();
        table.PrintTableAsTree();
    }

    public void Generate() {
        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }

        table.Generate();
        DrawRoomsDebug();
    }

    public void DrawRoomsDebug() {
        foreach (Row row in table.table) {
            GameObject room = Instantiate(roomPrefab, RowPosition(row), Quaternion.identity);
            room.transform.SetParent(transform, true);
            RoomType type = GlobalReference.GetReference<GameManagerReference>().GetRoom(row.id).roomType;
            switch (type) {
                case RoomType.BONUS:
                    room.GetComponent<MeshRenderer>().material = MBonus;
                    break;
                case RoomType.COMBAT:
                    room.GetComponent<MeshRenderer>().material = MCombat;
                    break;
                case RoomType.ENTRANCE:
                    room.GetComponent<MeshRenderer>().material = MEntrance;
                    break;
                case RoomType.EXIT:
                    room.GetComponent<MeshRenderer>().material = MExit;
                    break;
                case RoomType.PARKOUR:
                    room.GetComponent<MeshRenderer>().material = MParkour;
                    break;
                case RoomType.SHOP:
                    room.GetComponent<MeshRenderer>().material = MShop;
                    break;
                case RoomType.MOLDORB:
                    room.GetComponent<MeshRenderer>().material = MMoldOrb;
                    break;
                case RoomType.WAVESURVIVAL:
                    room.GetComponent<MeshRenderer>().material = MWaveSurvival;
                    break;
                default:
                    break;
            }
            room.name = table.RowReference(row) + $"| type: {type}";
            int i = 0;
            foreach (int branch in row.branches) {
                // Debug.DrawLine(RowPosition(row), RowPosition(table.GetRow(branch)), Color.black, 100, true);
                room.GetComponent<LineRenderer>().positionCount = i + 2;
                room.GetComponent<LineRenderer>().SetPosition(i, RowPosition(row));
                room.GetComponent<LineRenderer>().SetPosition(i+1, RowPosition(table.GetRow(branch)));
                i+=2;
            }
        }
    }

    public void ChangeSeed(string value) {
        if (value == "") value = "-1";
        GlobalReference.DevSettings.Set("seed", int.Parse(value));
        GlobalReference.DevSettings.SaveAll();
    }
    public void ChangeMaxObjectPerDepth(string value) {
        if (value == "") value = "0";
        GlobalReference.DevSettings.Set("maxObjectPerDepth", int.Parse(value));
        GlobalReference.DevSettings.SaveAll();
    }
    public void ChangeMaxBranchCount(string value) {
        if (value == "") value = "0";
        GlobalReference.DevSettings.Set("maxBranchCount", int.Parse(value));
        GlobalReference.DevSettings.SaveAll();
    }
    public void ChangeTargetDepth(string value) {
        if (value == "") value = "0";
        GlobalReference.DevSettings.Set("targetDepth", int.Parse(value));
        GlobalReference.DevSettings.SaveAll();
    }


    public Vector3 RowPosition(Row row) => new(2 * row.depth, 1, -2 * table.GetIndexInDepth(row));

    [ContextMenu("TestRandom")]
    public void TestRandom() {
        Dictionary<string, int> dict = new() {
            {"common", 60},
            {"uncommon", 20},
            {"rare", 10},
            {"epic", 7},
            {"legendary", 3},
        };

        for (int i = 0; i < 10; i++) {
            Debug.Log(RandomDistribution.GetRandom(dict));
        }
    }
}
