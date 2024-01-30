using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossLive : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int bulletDamage = 10;

    [SerializeField] private Image healthBar  ;   
    
    [Tooltip("VFX prefab to spawn upon impact")]
    public GameObject ImpactVfx;

    [Tooltip("LifeTime of the VFX before being destroyed")]
    public float ImpactVfxLifetime = 5f;
    
    public float blinkDuration = 1f;
    public Material blinkMaterial; // Assign a material that uses Standard Shader with Emission

    public Material originalMaterial;
    public GameObject blinkingObject;
    private float maxHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }
    public int getHealth()
    {
        return health;
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (healthBar != null)
        {
            healthBar.fillAmount = health / maxHealth;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        
       if (collision.gameObject.CompareTag("bullet"))
       {
           health -= bulletDamage;
            StartCoroutine(BlinkEffect());
            if (health <= 0)
            {
                // impact vfx
                if (ImpactVfx)
                {
                    GameObject impactVfxInstance = Instantiate(ImpactVfx, new Vector3(transform.position.x, 0, transform.position.z) , transform.rotation);
                    if (ImpactVfxLifetime > 0)
                    {
                        Destroy(impactVfxInstance.gameObject, ImpactVfxLifetime);
                    }
                }
                Invoke("LoadNextLevel", 3.1f);
                Destroy(this.blinkingObject);
                
                
            }
                
                
        }
    }
    IEnumerator BlinkEffect()
    {
        // Use the blink material if available, otherwise use the original material
        Material materialToUse = (blinkMaterial != null) ? blinkMaterial : originalMaterial;

        // Set the material color to white
        this.blinkingObject.GetComponent<Renderer>().material = blinkMaterial;

        // Wait for the blink duration
        yield return new WaitForSeconds(blinkDuration);

        // Set the material color back to its original state
        this.blinkingObject.GetComponent<Renderer>().material = originalMaterial;
    }
    
    void LoadNextLevel()
    {
        // Assuming the next level is named "NextLevel"
        UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossLive : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int bulletDamage = 10;

    [SerializeField] private Image healthBar  ;   
    
    // Start is called before the first frame update

    public int getHealth()
    {
        return health;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (healthBar != null)
        {
            healthBar.fillAmount = health / 100f;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            health -= bulletDamage;
            if(health <= 0)
                Invoke("LoadNextLevel", 0f);
                
        }
    }
    void LoadNextLevel()
    {
        // Assuming the next level is named "NextLevel"
        UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
    }
}*/