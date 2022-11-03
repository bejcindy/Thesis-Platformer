using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //public float walkSpeed, jumpSpeed, jetSpeed;

    [Header("Jet Max Speed")]
    public float jetSpeed;
    [Space(10)]
    public float gravityMultiplier;
    public Slider gasSlider;
    public float gasAmount, useSpeed, refillSpeed;

    public enum Bounciness
    {
        Bouncy,
        NotBouncy
    };
    public Bounciness bounciness;



    [Header("Jet Mode (Accelerate/Same Speed)")]
    public bool accel;
    [Header("If Accelerate")]
    public float accelRate;

    [HideInInspector]
    public bool respawn;

    public PhysicsMaterial2D bouncy, notBouncy;

    public TextMeshProUGUI characterStat;
    
    Rigidbody2D rb;
    float moveX;
    Vector2 jetDirection;
    bool inAir, grounded;
    GameObject jetParticles;
    ParticleSystem part;
    ParticleSystem.EmissionModule emissionModule;
    Vector2 originalGravity;
    float currentSpeed;

    Transform respawnPos;
    bool bounce, highGrav;
    string b, a, g;

    // Start is called before the first frame update
    void Start()
    {
        if (gravityMultiplier == 0)
        {
            gravityMultiplier = 1;
        }
        originalGravity = Physics2D.gravity;
        //Physics2D.gravity = Physics2D.gravity * gravityMultiplier;
        rb = GetComponent<Rigidbody2D>();
        gasSlider.maxValue = gasAmount;
        gasSlider.value = gasAmount;
        jetParticles = transform.GetChild(0).gameObject;
        part = jetParticles.GetComponent<ParticleSystem>();
        //jetParticles.GetComponent<ParticleSystem>().Stop();
        emissionModule = part.emission;
        emissionModule.rateOverTime = 0;
        respawnPos = GameObject.FindGameObjectWithTag("Respawn").transform;
        respawn = false;
        currentSpeed = 0;
        b = "Bounciness (Z): No";
        a = "Jet Speed (X): Accelerate";
        g = "Gravity (C): Normal";
        if (bounciness == Bounciness.Bouncy)
        {
            GetComponent<Collider2D>().sharedMaterial = bouncy;
        }
        else
        {
            GetComponent<Collider2D>().sharedMaterial = notBouncy;
        }
    }

    void FixedUpdate()
    {
        if (respawn)
        {
            transform.position = respawnPos.position;
            respawn = false;
        }
        if (Input.GetButton("Fire1") && gasSlider.value > 0)
        {
            //如果在喷气
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pos = transform.position;
            jetDirection = (pos - mousePos).normalized;
            if (!accel)
            {
                rb.velocity = jetSpeed * jetDirection;
            }
            else
            {
                if (currentSpeed < jetSpeed)
                {
                    currentSpeed += accelRate;
                }
                else
                {
                    currentSpeed = jetSpeed;
                }
                rb.velocity = currentSpeed * jetDirection;
            }

            inAir = true;
            jetParticles.transform.forward = mousePos - pos;
            Physics2D.gravity = originalGravity;
            emissionModule.rateOverTime = 10;
        }
        else
        {
            //Debug.Log("here");
            //如果没在喷气
            //Jump & WASD Movement
            //moveX = walkSpeed * Input.GetAxis("Horizontal");
            //if (Input.GetKeyDown(KeyCode.Space) && grounded)
            //{
            //    rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            //}
            //rb.velocity = new Vector2(moveX, rb.velocity.y);
            Physics2D.gravity = originalGravity * gravityMultiplier;
            inAir = false;
            currentSpeed = 0;
            emissionModule.rateOverTime = 0;
        }
    }

    void Update()
    {
        //如果正在喷气
        if(inAir && !grounded && gasSlider.value > 0)
        {
            gasSlider.value -= (Time.deltaTime * useSpeed);
            
        }
        //如果落地了
        if(!inAir && grounded)
        {
            if (gasSlider.value < gasSlider.maxValue)
            {
                gasSlider.value += (Time.deltaTime * refillSpeed);
            }
            else
            {
                gasSlider.value = gasSlider.maxValue;
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("Level Select");
        }

        //调节Character数值
        if (Input.GetKeyDown(KeyCode.Z))
        {
            bounce = !bounce;
            if (bounce)
            {
                b = "Bounciness (Z): Bouncy";
                GetComponent<Collider2D>().sharedMaterial = bouncy;
            }
            else
            {
                b = "Bounciness (Z): No";
                GetComponent<Collider2D>().sharedMaterial = notBouncy;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            accel = !accel;
            if (accel)
            {
                a = "Jet Speed (X): Accelerate";
            }
            else
            {
                a = "Jet Speed (X): Same Speed";
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            highGrav = !highGrav;
            if (highGrav)
            {
                g = "Gravity (C): High";
                gravityMultiplier = 2f;
            }
            else
            {
                g = "Gravity (C): Normal";
                gravityMultiplier = 1f;
            }
        }
        
        characterStat.text = b + "\n" + a + "\n" + g;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //不能摸墙recharge
            if(transform.position.y> collision.gameObject.transform.position.y)
            {
                grounded = true;
                inAir = false;
            }

        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            gasSlider.value = gasSlider.maxValue;
            transform.position = respawnPos.position;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

}
