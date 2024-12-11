using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class damagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    public float duration;

    public float HeightTransfrom;
    public void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        Destroy(gameObject, duration);
    }

    public void Update()
    {
        //make the color fade away over time
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a - (Time.deltaTime / duration));
    }
    public void SetUp(int damage)
    {
        textMesh.SetText(damage.ToString());
    }

    public static void CreatePopUp(Vector3 position, GameObject damagePopUpPrefab, int damage)
    {
        GameObject damagePopUpTransform = Instantiate(damagePopUpPrefab, new Vector3(position.x, position.y, position.z) , Quaternion.identity);
        damagePopUp damagePopUp = damagePopUpTransform.GetComponent<damagePopUp>();
        damagePopUp.SetUp(damage);
    }
}