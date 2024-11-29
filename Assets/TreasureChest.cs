using UnityEngine;

public class TreasureChestInteractable : Interactable
{
    [Header("Treasure Chest Settings")]
    [SerializeField] private LootTable lootTable;
    [Tooltip("The reward config it uses for this treasure chest [Read Only]")]
    [SerializeField] private RewardConfig rewardConfig;


    private GameManagerReference gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        gameManager = GlobalReference.GetReference<GameManagerReference>();

        RoomType roomType = gameManager.Get(gameManager.activeRoom.id).roomType;
        lootTable = rewardConfig.GetLootTableForRoomType(roomType);

        GlobalReference.SubscribeTo(Events.ROOM_FINISHED, ShowTreasureChest);
    }

    public override void OnInteract()
    {

        if (lootTable == null) return;

        var loot = lootTable.GetRandomReward();

        if (loot != null)
        {
            // loop through the list of actions and invoke them
            foreach (var action in loot.Entry) action.InvokeAction();
        }
    }

    public void ShowTreasureChest()
    {
        // set active 
        gameObject.SetActive(true);
    }
}
