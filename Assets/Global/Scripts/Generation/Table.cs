using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using SRandom = System.Random;

public class Table
{
    // Settings
    public int maxObjectPerDepth = 3;
    public int minBranchCount = 2;
    public int maxBranchCount = 3;
    public int targetDepth = 7;
    public int seed = -1;
    public int shopDepth = 0;
    private SRandom random;

    // Content
    public List<Row> table;

    // Constructor
    public Table() => Generate();

    // Data
    public Row GetRow(int id) => table.Where(x => x.id == id).FirstOrDefault();
    public Row GetFirstUnpopulatedRow() => table.Where(x => x.branches.Count == 0).FirstOrDefault(); 
    public int GetNextId() => table?.Count ?? 0;
    public int GetIndexInDepth(Row row) => GetRowsAtDepth(row.depth).OrderBy(x => x.id).ToList().FindIndex(x => x.id == row.id);
    public List<int> GetDepthColumn() => table.Select(x => x.depth).ToList();
    public List<Row> GetRowsAtDepth(int depth) => table.Where(x => x.depth == depth).ToList();

    // Reference
    public string RowReference(Row row) => $"Row: {row.id}, {row.depth}, [{string.Join(';', row.branches)}]";

    // Generation Logic
    public void Generate() {
        if (seed < 0) random = new(Guid.NewGuid().GetHashCode());
        else random = new(seed);

        shopDepth = random.Next(2, targetDepth-1);

        GlobalReference.GetReference<GameManagerReference>().Reset();

        table = new();
        GetIdFromNewRow(0, RoomType.ENTRANCE);

        Populate(GetFirstUnpopulatedRow());
    }
    
    private void Populate(Row row) {
        // base cases
        if (row.depth == targetDepth) return;

        // init
        RoomType roomType = RoomType.OTHER;

        // get total branches to apply
        int branchesToApply = random.Next(minBranchCount, maxBranchCount+1);
        // get list of all existing branches at target depth
        List<Row> existingBranches = GetRowsAtDepth(row.depth+1);
        // calculate the room for new rooms taking into account the existing amount of branches and the max amount of branches
        int roomForNewBranches = maxObjectPerDepth - existingBranches.Count();
        // calculating the amount of already existing rooms we can branch into
        int roomForOldBranches = existingBranches.Count();

        // deciding the amount of new rooms to create
        int newBranchesToApply = random.Next(0, Math.Min(roomForNewBranches, branchesToApply) + 1);
        // deciding the amount of existing rooms to branch into
        int oldBranchesToApply = existingBranches.Count() != 0 ? Math.Min(roomForOldBranches, branchesToApply - newBranchesToApply) : 0;
        // destribute leftover branches
        int leftoverBranchesToApply = branchesToApply - newBranchesToApply - oldBranchesToApply;
        if (leftoverBranchesToApply > 0) newBranchesToApply = Mathf.Min(newBranchesToApply + leftoverBranchesToApply, roomForNewBranches);

        if (row.depth+1 == targetDepth ) {
            roomType = RoomType.EXIT;
            if (existingBranches.Count() == 0) {
                newBranchesToApply = 1;
                oldBranchesToApply = 0;
            }
            else {
                newBranchesToApply = 0;
                oldBranchesToApply = 1;
            }
        }

        if (row.depth+1 == shopDepth ) {
            roomType = RoomType.SHOP;
            if (existingBranches.Count() == 0) {
                newBranchesToApply = 1;
                oldBranchesToApply = 0;
            }
            else {
                newBranchesToApply = 0;
                oldBranchesToApply = 1;
            }
        }

        // Debug.Log($"row: {row.id} | total: {branchesToApply} | new: {newBranchesToApply} | old: {oldBranchesToApply} | room: {roomForNewBranches}");

        for (int i = 0; i < newBranchesToApply; i++ ) {
            row.branches.Add(GetIdFromNewRow(row.depth+1, roomType));
        }
        for (int i = 0; i < oldBranchesToApply; i++ ) {
            row.branches.Add(GetIdFromOldRow(existingBranches));
        }

        // recursion
        Populate(GetFirstUnpopulatedRow());
    }

    private int GetIdFromNewRow(int depth, RoomType roomType = RoomType.OTHER) {
        Row newRow = new(GetNextId(), depth);
        table.Add(newRow);
        GlobalReference.GetReference<GameManagerReference>().Add(newRow.id, roomType);
        return newRow.id;
    }
    private int GetIdFromOldRow(List<Row> existingBranches) {
        Row newRow = existingBranches[random.Next(0, existingBranches.Count)];
        existingBranches.Remove(newRow);
        return newRow.id;
    }
}

public struct Row {
    public int id;
    public int depth;
    public int column;
    public List<int> branches;

    public Row(int id_, int depth_, int column_ = -1) {
        id = id_;
        depth = depth_;
        column = column_;
        branches = new List<int>();
    }
}
