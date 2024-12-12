using TMPro;
using UnityEngine;

public class damagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float showDamage = 0;
    public float duration;
    public float activeDuration;
    public Color defaultColor;
    public void Awake()
    {
        Debug.Log("Awake daamge popup");
        textMesh = GetComponent<TextMeshPro>();
        defaultColor = textMesh.color;
    }

    public void Update()
    {
        Debug.Log("Update");
        if (duration <= activeDuration)
        {
            ResetColor();
            textMesh.SetText("");
            activeDuration = 0;
            showDamage = 0;
        }
        else
        {
            Debug.Log(activeDuration);
            activeDuration += Time.deltaTime;
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a - (Time.deltaTime / duration));
        }
    }
    public void SetUp(int damage)
    {
        Debug.Log("Setup");
        showDamage += damage;
        textMesh.SetText(showDamage.ToString());
    }

    public void ResetColor()
    {
        textMesh.color = defaultColor;
    }
}