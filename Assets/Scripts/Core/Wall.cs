using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private bool randomHealth;
    [SerializeField] private float healthLoseSpeed;
    [SerializeField] private TextMeshPro healthText;
    [SerializeField] private Renderer wallRenderer;
    [SerializeField] private ParticleSystem destroyParticle;
    [SerializeField] private Rigidbody wallRB;


    private bool isPushed;
    void Start()
    {
        if (randomHealth)
        {
            health = Random.Range(1, 100);
        }
        healthText.text = health.ToString();
        wallRenderer.material.color = Color.HSVToRGB(0.3f- health/360, 1, 1);
    }

    IEnumerator LoseHealth()
    {
        while (isPushed && health > 0)
        {
            health -= healthLoseSpeed * Time.deltaTime;
            healthText.text = ((int) health).ToString();
            wallRenderer.material.color = Color.HSVToRGB(0.3f- health/360, 1, 1);
            
            yield return null;
        }

        wallRB.velocity = Vector3.zero;
        
        if (health <= 0)
        {
            WallDestroy(); 
        }
    }

    void WallDestroy()
    {
        Destroy(destroyParticle.gameObject, 1.5f);
        destroyParticle.transform.SetParent(null);
        destroyParticle.Play();
        transform.position = Vector3.zero;
        Destroy(gameObject, 0.5f);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPushed = true;
            StartCoroutine(LoseHealth());
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPushed = false;
            StopCoroutine(LoseHealth());
        }
    }
}
