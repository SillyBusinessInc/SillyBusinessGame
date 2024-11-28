using System.Linq;
using UnityEngine;

public class TreasureChestInteractable : Interactable
{
    [Tooltip("The loot table to use for this chest [Debug, read only]")]
    [SerializeField] private LootTable _lootTable;
    private LootTable lootTable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {

        base.Start();

        GameManagerReference gameManager = GlobalReference.GetReference<GameManagerReference>();

        RoomType roomType = gameManager.Get(gameManager.activeRoom.id).roomType;

        Debug.Log($"Room Type: {roomType}");
    }

    [ContextMenu("GetLoot")]
    public override void OnInteract()
    {
        if (lootTable == null)
        {
            lootTable = _lootTable;
        }

        var loot = lootTable.GetRandomReward();

        Debug.Log($"Loot: {loot}");

        if (loot != null)
        {
            // loop through the list of actions and invoke them
            foreach (var action in loot.Entry) action.InvokeAction();
        }
    }
}
