using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class damagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    public float duration;

    public void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        Destroy(gameObject, duration);
    }
    public void SetUp(int damage)
    {
        textMesh.SetText(damage.ToString());
    }

}