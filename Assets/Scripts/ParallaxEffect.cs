using UnityEngine;

/// <summary>
/// ParallaxEffect script creates a layered background movement based on the camera’s
/// movement to simulate depth. Farther layers move slower, creating a parallax illusion.
/// </summary>
public class ParallaxEffect : MonoBehaviour
{
    // Reference to the main camera transform
    Transform mainCamera;

    // The camera's initial starting position
    Vector3 cameraStartPosition;

    // Horizontal distance the camera has moved from its start position
    float distance;

    // Array to hold background GameObjects (children of this object)
    GameObject[] backgrounds;

    // Materials of the backgrounds to manipulate texture offsets
    Material[] materials;

    // Calculated parallax speed for each background based on distance from the camera
    float[] backSpeed;

    // Maximum distance of the farthest background from the camera
    float farthestBack;

    [Header("Parallax Settings")]
    [Tooltip("Controls the strength of the parallax effect. Value between 0 (no parallax) and 1 (full parallax).")]
    [Range(0f, 1f)]
    public float parallaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Get main camera's transform
        mainCamera = Camera.main.transform;

        // Store the initial camera position
        cameraStartPosition = mainCamera.position;

        // Get number of background layers (child objects)
        int backCount = transform.childCount;

        // Initialize arrays
        materials = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        // Populate backgrounds and their materials
        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            materials[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        // Calculate the individual background parallax speeds
        BackSpeedCalculate(backCount);
    }

    /// <summary>
    /// Calculates the speed for each background layer based on its Z distance from the camera.
    /// The farther the background, the slower it moves.
    /// </summary>
    /// <param name="backCount">Total number of background layers</param>
    void BackSpeedCalculate(int backCount)
    {
        // First, find the farthest background from the camera
        for (int i = 0; i < backCount; i++)
        {
            if ((backgrounds[i].transform.position.z - mainCamera.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - mainCamera.position.z;
            }
        }

        // Then, calculate parallax speed relative to the farthest background
        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - mainCamera.position.z) / farthestBack;
        }
    }

    // LateUpdate is called after all Update calls
    private void LateUpdate()
    {
        // Calculate how far the camera has moved on X axis
        distance = mainCamera.position.x - cameraStartPosition.x;

        // Move the parent object to follow the camera’s X position (optional visual adjustment)
        transform.position = new Vector3(mainCamera.position.x, transform.position.y, 0);

        // Apply parallax effect to each background layer using texture offset
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            materials[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}
