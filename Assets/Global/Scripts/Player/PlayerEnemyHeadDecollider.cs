using Unity.Cinemachine;
using UnityEngine;

public class PlayerEnemyHeadDecollider : MonoBehaviour
{
    [Header("No Player Standing On Enemy Head Slide Settings")]
    [Tooltip("Radius of the sphere to check for enemy collision")]
    [Range(0f, 5f)]
    public float radius = 1f;
    [Tooltip("Force multiplier for sliding off the enemy")]
    [Range(0f, 100f)]
    public float slideForceMultiplier = 10f;
    [Tooltip("The maximum amount of upwards force to apply")]
    [Range(0f, 1f)]
    public float maxUpwardsForce = 0.2f;
    [Tooltip("The minimum amount of force to add to where the camera is looking. Only if calculated force is lower than this")]
    [Range(0f, 1f)]
    public float minForwardsForce = 0.2f;

    private PlayerObject playerObj;
    private Rigidbody playerRigidbody;
    private Collider playerCollider;
    private Transform cameraTransform;
    private int enemyLayerMask;

    void Start()
    {
        // Cache components
        playerObj = GlobalReference.GetReference<PlayerReference>().PlayerObj;
        playerRigidbody = playerObj.GetComponent<Rigidbody>();
        playerCollider = playerObj.GetComponent<Collider>();
        cameraTransform = GlobalReference.GetReference<PlayerReference>().PlayerCamera.GetComponent<CinemachineBrain>().transform;
        if (!playerRigidbody || !playerCollider)
        {
            Debug.LogError("PlayerEnemyHeadDecollider requires both player rb and collider");
        }
        enemyLayerMask = LayerMask.GetMask("Enemies");
    }

    void FixedUpdate()
    {
        HandlePenetration();
    }

    void HandlePenetration()
    {
        // check if there is overlap between player and enemy
        Collider[] overlappingColliders = Physics.OverlapSphere(playerObj.transform.position, radius, enemyLayerMask);

        if (overlappingColliders.Length <= 0) return;

        foreach (Collider otherCollider in overlappingColliders)
        {
            Vector3 slideDirection = (playerObj.transform.position - otherCollider.transform.position).normalized;

            Vector3 slideForce = slideDirection * slideForceMultiplier;
            if (slideForce.x < minForwardsForce && slideForce.z < minForwardsForce)
            {
                Vector3 tempForce = cameraTransform.forward * minForwardsForce;
                slideForce.x = Mathf.Max(slideForce.x, tempForce.x);
                slideForce.z = Mathf.Max(slideForce.z, tempForce.z);
            }
            slideForce.y = Mathf.Max(slideDirection.y, maxUpwardsForce);

            // Apply the force
            playerRigidbody.AddForce(slideForce, ForceMode.Impulse);
        }
    }

    void OnDrawGizmos()
    {
        if (playerObj == null)
            return;

        // Set the color of the gizmos to distinguish it in the scene
        Gizmos.color = Color.cyan;

        // Draw a wire sphere at the player's position with the specified radius
        Gizmos.DrawWireSphere(playerObj.transform.position, radius);
    }
}