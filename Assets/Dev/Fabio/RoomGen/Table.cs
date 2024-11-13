using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using SRandom = System.Random;

public class Table
{
    // Settings
    public int maxObjectPerDepth = 3;
    public int maxBranchCount = 3;
    public int targetDepth = 7;
    public int seed = -1;
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

        table = new() {new(0, 0)};

        Populate(GetFirstUnpopulatedRow());
    }
    private void Populate(Row row) {
        // base cases
        if (row.depth == targetDepth) return;

        // population
        int branchesToApply = random.Next(1, maxBranchCount+1);
        List<Row> existingBranches = GetRowsAtDepth(row.depth+1);
        int roomForNewBranches = maxObjectPerDepth - existingBranches.Count();
        int roomForOldBranches = existingBranches.Count();

        int newBranchesToApply = random.Next(0, Math.Min(roomForNewBranches+1, branchesToApply+1));
        int oldBranchesToApply = existingBranches.Count() != 0 ? Math.Min(roomForOldBranches, branchesToApply - newBranchesToApply) : 0;

        // Debug.Log($"row: {row.id} | total: {branchesToApply} | new: {newBranchesToApply} | old: {oldBranchesToApply} | room: {roomForNewBranches}");

        for (int i = 0; i < newBranchesToApply; i++ ) {
            row.branches.Add(GetIdFromNewRow(row.depth+1));
        }
        for (int i = 0; i < oldBranchesToApply; i++ ) {
            row.branches.Add(GetIdFromOldRow(existingBranches));
        }

        // recursion
        Populate(GetFirstUnpopulatedRow());
    }

    private int GetIdFromNewRow(int depth) {
        Row newRow = new(GetNextId(), depth);
        table.Add(newRow);
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
    public List<int> branches;

    public Row(int id_, int depth_) {
        id = id_;
        depth = depth_;
        branches = new List<int>();
    }
}
