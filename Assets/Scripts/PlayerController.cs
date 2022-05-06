using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private bool fuelIsOver;
    [SerializeField]
    private float floatForce;
    [SerializeField]
    private float fuelConsumption = 0.5f;
    private float maxAltitude = 9;
    private float startForce = 2;
    private Rigidbody playerRB;
    
    public bool gameOver = false;
    public ParticleSystem playerParticle;
    public Slider fuelIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRB.AddForce(Vector3.up * startForce, ForceMode.Impulse);
        fuelIndicator.value = 0.5f;
        fuelIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTheFuel();

        if (Input.GetKey(KeyCode.Space) && transform.position.y < maxAltitude && !fuelIsOver)
        {
            playerRB.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
            playerParticle.gameObject.SetActive(true);
            fuelIndicator.value -= fuelConsumption * Time.deltaTime;
        }
        else
        {
            playerParticle.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            gameOver = true;
        }

        if (other.gameObject.CompareTag("Fuel"))
        {
            Destroy(other.gameObject);
            fuelIndicator.value += 0.25f;
        }
    }

    void CheckTheFuel()
    {
        if (fuelIndicator.value == 0)
        {
            fuelIsOver = true;
        }
    }


}
