using System.Collections.Generic;
using UnityEngine;

public class Table
{
    public List<int> idColumn;
    public List<int> depthColumn;
    public List<List<int>> branchesColumn;
    
    public struct Row { 
        public int id; 
        public int depth; 
        public List<int> branches; 
    }


}
