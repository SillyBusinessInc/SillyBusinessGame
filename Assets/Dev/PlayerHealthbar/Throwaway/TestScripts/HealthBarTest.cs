using UnityEngine;

public class HealthBarTest : MonoBehaviour
{
    public Player player;

    [ContextMenu("1HP damage")]
    void DoDamage1()
    {
        DoDamage(1);
    }
    [ContextMenu("10HP damage")]
    void DoDamage10()
    {
        DoDamage(10);
    }
    [
        ContextMenu("100HP damage (instakill)")]
    void DoDamage100()
    {
        DoDamage(100);
    }

    private void DoDamage(float damage)
    {
        player.OnHit(damage);
    }

}
