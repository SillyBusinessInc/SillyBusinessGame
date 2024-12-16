using UnityEngine;

public class MoldEffectController : MonoBehaviour
{
    public Material TestMold; // Assign your material here
    [Range(0f, 1f)] public float currentIntensity = 0.0f; // Intensity of the mold
    public Vector3 overlayDirection = Vector3.zero; // Direction for the overlay (X, Y, Z)

    void Start()
    {
        if (TestMold != null)
        {
            // Initialize mold intensity and direction
            currentIntensity = TestMold.GetFloat("_Overlay_Density");
            Vector4 currentDirection = TestMold.GetVector("_Overlay_Direction");
            overlayDirection = new Vector3(currentDirection.x, currentDirection.y, currentDirection.z);
        }
    }

    void Update()
    {
        if (TestMold != null)
        {
            // Update the mold intensity
            TestMold.SetFloat("_Overlay_Density", currentIntensity);

            // Update the overlay direction
            TestMold.SetVector("_Overlay_Direction", new Vector4(overlayDirection.x, overlayDirection.y, overlayDirection.z, 0));
        }
    }
}
