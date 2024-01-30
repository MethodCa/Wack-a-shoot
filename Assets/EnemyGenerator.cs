using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class EnemyGenerator: MonoBehaviour
{
    // Reference to the Cube_Boss GameObject
    public GameObject cubeBoss;

    // Reference to the Capsule_Player GameObject
    public GameObject capsulePlayer;

    // Reference to the cactus prefab
    public GameObject cactusFoePrefab;
    public GameObject cactusFoeMedPrefab;
    public GameObject cactusFoeBigPrefab;

    // Editable angle for throwing the sphere
    [Range(0f, 120f)]
    public float throwAngle = 90f;

    // Custom offset for sphere instantiation
    public Vector3 offsetPos = Vector3.zero;

    // Timer to control the frequency of sphere throwing
    private float timer = 0f;
    public float throwInterval = 1f; // Interval in seconds

    // Variable to control the number of foes on the game
    private int foesCactis = 0;
    public int maxCactus = 500;

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the timer exceeds the throw interval
        if (timer >= throwInterval)
        {
            // Reset the timer
            timer = 0f;

            // if the number of foes is not the max
            if (foesCactis < maxCactus)
            {
                // Throw a sphere from Cube_Boss towards Capsule_Player
                ThrowFoe();
            }
   
        }
    }

    void ThrowFoe()
    { 
        // Ensure both cubeBoss and capsulePlayer references are not null
        if (cubeBoss != null && capsulePlayer != null)
        {
            // Calculate the midpoint between Cube_Boss and Capsule_Player
            //Vector3 midpoint = ((cubeBoss.transform.position + capsulePlayer.transform.position) / 0.9f);

            // Calculate the direction from Cube_Boss to the midpoint
            Vector3 throwDirection = (capsulePlayer.transform.position - cubeBoss.transform.position).normalized;

            // Calculate the initial velocity based on the throw angle
            float throwSpeed = CalculateThrowSpeed();

            int option = (int)Random.Range(1, 10);
            GameObject cactus = null;
            //Debug.Log("Option " + option);
            switch (option)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7: // Instantiate a new sphere at the position of Cube_Boss
                    cactus = Instantiate(cactusFoePrefab, (cubeBoss.transform.position + offsetPos), Quaternion.identity);
                    break;
                case 8:
                case 9:
                    cactus = Instantiate(cactusFoeMedPrefab, (cubeBoss.transform.position + offsetPos), Quaternion.identity);
                    break;
                default:
                    cactus = Instantiate(cactusFoeBigPrefab, (cubeBoss.transform.position + offsetPos), Quaternion.identity);
                    break;
            }
            // Control the number of foes on the scene
            foesCactis++;
            Rigidbody rb = cactus.GetComponent<Rigidbody>();
            // Set the rotation of the sphere to always face the Capsule_Player
            // Apply force to the sphere to throw it towards Capsule_Player
            rb.velocity = CalculateThrowVelocity(throwSpeed/2, throwDirection + new Vector3(0,0.8f,0));
         

        }
        else
        {
            //Debug.LogError("Cube_Boss, Capsule_Player, or spherePrefab reference is null. Please assign the GameObjects in the inspector.");
        }
    }

    float CalculateThrowSpeed()
    {
        // Calculate the initial velocity based on the throw angle
        float g = Physics.gravity.y;
        float distance = Vector3.Distance(cubeBoss.transform.position, capsulePlayer.transform.position);
        // Use the kinematic equation: distance = (v^2 * sin(2 * angle)) / g
        // Solve for initial velocity (v)
        float throwSpeed = Mathf.Sqrt(((distance * 2 )* Mathf.Abs(g)) / Mathf.Sin( Mathf.Deg2Rad * throwAngle));

        return throwSpeed;
    }

    Vector3 CalculateThrowVelocity(float throwSpeed, Vector3 throwDirection)
    {
        // Calculate the initial velocity vector
        Vector3 throwVelocity = throwSpeed * throwDirection;
        //Debug.Log("ThrowVel= "+throwVelocity);
        return throwVelocity;
    }
}