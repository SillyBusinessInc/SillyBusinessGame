using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    private Table table;

    void Start() {
        table = new();
        Generate();
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
            room.name = table.RowReference(row);
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
        table.seed = int.Parse(value);
    }
    public void ChangeMaxObjectPerDepth(string value) {
        if (value == "") value = "0";
        table.maxObjectPerDepth = int.Parse(value);
    }
    public void ChangeMaxBranchCount(string value) {
        if (value == "") value = "0";
        table.maxBranchCount = int.Parse(value);
    }
    public void ChangeTargetDepth(string value) {
        if (value == "") value = "0";
        table.targetDepth = int.Parse(value);
    }


    public Vector3 RowPosition(Row row) => new(2 * row.depth, 1, -2 * table.GetIndexInDepth(row));
}
