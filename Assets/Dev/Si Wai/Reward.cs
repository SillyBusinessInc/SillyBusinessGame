using UnityEngine;

public abstract class Reward : MonoBehaviour
{
    public string Title;
    public float Weight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) {
        PlayerObject playerObj = collider.gameObject.GetComponent<PlayerObject>();
        if (playerObj) {
            ActivateReward(playerObj.player);
            this.gameObject.SetActive(false);
        }
    }

    public virtual void ActivateReward(Player player) {
    }
}
