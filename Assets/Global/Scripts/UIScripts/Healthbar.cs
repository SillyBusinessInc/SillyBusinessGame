using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Content;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    // private RectTransform UnderlayHalfHealth;
    // private RectTransform OverlayHalfHealth;
    // private float originalWidth;
    // private Player player;
    // private RectTransform UnderlayWholeHealth;
    // private RectTransform OverlayWholeHealth;
    // private bool isHalf;
    private readonly List<GameObject> healthObjects = new();
    
    [SerializeField] private GameObject healthPrefab;

    void Awake()
    {
        // // getting the half hearts
        // UnderlayHalfHealth = transform.GetChild(0) as RectTransform;
        // OverlayHalfHealth = transform.GetChild(1) as RectTransform;

        // // getting the whole hearts, which is a child object of the halfheart
        // UnderlayWholeHealth = UnderlayHalfHealth.GetChild(0) as RectTransform;
        // OverlayWholeHealth = OverlayHalfHealth.GetChild(0) as RectTransform;

        // originalWidth = UnderlayHalfHealth.sizeDelta.x;
        GlobalReference.SubscribeTo(Events.HEALTH_CHANGED, UpdateCurrentHealthByPlayer);
    }

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++) {
            healthObjects.Add(transform.GetChild(i).gameObject);
        }
        Player player = GlobalReference.GetReference<PlayerReference>().Player;

        UpdateMaxHealth(player.playerStatistic.MaxHealth.GetValue());
        UpdateCurrentHealth(player.playerStatistic.Health);
        Debug.Log(healthObjects.Count);


        player.playerStatistic.MaxHealth.Subscribe(() => UpdateMaxHealth(player.playerStatistic.MaxHealth.GetValue()));
    }

    private void UpdateCurrentHealthByPlayer() {
        Player player = GlobalReference.GetReference<PlayerReference>().Player;

        UpdateCurrentHealth(player.playerStatistic.Health);
    }

    // private void Reset() {
    //     healtObjects.ForEach(x => Destroy(x));
    //     healtObjects
    // }

    // public void UpdateHealthBar()
    // {
    //     UpdateMaxHealth();
    //     UpdateCurrentHealth();
    // }

    public void UpdateMaxHealth(float maxHealth) {
        // populate new health bar
        int newSize = (int)Mathf.Ceil(maxHealth / 2);
        int currentSize = healthObjects.Count;
        bool isHalf = maxHealth % 2 != 0;
        Debug.Log($"Updating max health: {maxHealth}, newsize: {newSize}, currentsize: {currentSize}, half: {isHalf}");

        SetMode(healthObjects.LastOrDefault(), "full", true);

        // add items if increase
        if (currentSize < newSize) {
            for (int i = currentSize; i < newSize; i++) {
                GameObject newObj = Instantiate(healthPrefab, Vector3.zero, Quaternion.identity);
                newObj.transform.SetParent(transform);
                RectTransform source = newObj.GetComponent<RectTransform>();
                source.anchorMin = new(i, 0);
                source.anchorMax = new(i+1, 1);
                source.offsetMin = new(0, 0);
                source.offsetMax = new(0, 0);
                SetMode(newObj, "full", true);
                healthObjects.Add(newObj);
            }
        }
        // remove items if decreased
        else {
            int toRemove = currentSize - newSize;
            for (int i = 0; i < toRemove; i++) {
                GameObject oldObj = healthObjects.LastOrDefault();
                Destroy(oldObj);
                healthObjects.Remove(oldObj);
            }
        }

        // set last heart to half if applicable
        if (isHalf) {
            SetMode(healthObjects.LastOrDefault(), "half", true);
        }
    }

    public void UpdateCurrentHealth(float currentHealth) {
        // populate new health bar
        int newSize = (int)Mathf.Ceil(currentHealth / 2);
        int currentSize = healthObjects.Where((x) => x.transform.GetChild(1).gameObject.activeSelf).ToList().Count;
        bool isHalf = currentHealth % 2 != 0;
        Debug.Log($"Updating current health: {currentHealth}, newsize: {newSize}, currentsize: {currentSize}, half: {isHalf}");

        if (currentSize > 0) SetMode(healthObjects[currentSize-1], "full", false);

        // add items if increase
        if (currentSize < newSize) {
            for (int i = currentSize; i < newSize; i++) {
                GameObject newObj = healthObjects[i];
                SetMode(newObj, "full", false);
            }
        }
        // remove items if decreased
        else if (currentSize > newSize) {
            for (int i = currentSize; i > newSize; i--) {
                GameObject oldObj = healthObjects[i-1];
                SetMode(oldObj, "empty", false);
            }
        }

        // set last heart to half if applicable
        if (isHalf) {
            // SetMode(healthObjects.Where((x) => x.transform.GetChild(1).gameObject.activeSelf).LastOrDefault(), "half", false);
            SetMode(healthObjects[newSize-1], "half", false);
        }
    }

    private void SetMode(GameObject healthObj, string setTo, bool setBase) {
        GameObject halfObj = healthObj.transform.GetChild(setBase ? 0 : 1).gameObject;
        GameObject fullObj = halfObj.transform.GetChild(0).gameObject;

        switch (setTo) {
            case "empty":
                halfObj.SetActive(false);
                fullObj.SetActive(false);
                break;
            case "full":
                halfObj.SetActive(true);
                fullObj.SetActive(true);
                break;
            case "half":
                halfObj.SetActive(true);
                fullObj.SetActive(false);
                break;
            default:
                break;
        }
    }
    
    // public void UpdateCurrentHealth()
    // {
    //     float currentHealth = player.playerStatistic.Health;
    //     isHalf = currentHealth % 2 != 0;
    //     // if a half heart needs to be displayed, display the half heart image as the last image
    //     if (isHalf) {
    //         ChangeHealthWidth(OverlayWholeHealth, currentHealth - 1);
    //         ChangeHealthWidth(OverlayHalfHealth, currentHealth + 1);
    //     } else {
    //         ChangeHealthWidth(OverlayWholeHealth, currentHealth);
    //         ChangeHealthWidth(OverlayHalfHealth, 0);
    //     }
    // }
    
    // public void UpdateMaxHealth() {
    //     // doing maxHealth / 2 because 1hp is a half heart
    //     float maxHealth = player.playerStatistic.MaxHealth.GetValue();
    //     isHalf = maxHealth % 2 != 0;
    //     if (isHalf) {
    //         ChangeHealthWidth(UnderlayWholeHealth, maxHealth - 1);
    //         ChangeHealthWidth(UnderlayHalfHealth, maxHealth + 1);
    //     } else {
    //         ChangeHealthWidth(UnderlayWholeHealth, maxHealth);
    //         ChangeHealthWidth(UnderlayHalfHealth, 0);
    //     }
    // }

    // private void ChangeHealthWidth(RectTransform healthbar, float newHealth) {
    //     healthbar.sizeDelta = new Vector2(
    //         originalWidth * newHealth / 2, 
    //         healthbar.sizeDelta.y
    //     );
    // }
}