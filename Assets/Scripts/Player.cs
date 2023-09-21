using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed;
    public int speedRotation;
    public Animator anim;
    public Rigidbody rb;
    public float ForcaPulo;
    public bool estaNoAr;
    public bool estaAtacando;
    public float vida;

    //FALLDAMAGE
    float endPosition;
    public bool readyDamage;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        endPosition = transform.position.y;
        Moviment();
        FallDamage();
    }

    void Moviment()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (estaNoAr)
            {
                anim.SetBool("andando", false);
            }
            else
            {
                anim.SetBool("andando", true);
            }
        }
        if (!estaNoAr && Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("andando", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * speedRotation);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * speedRotation);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("andando", false);
            StartCoroutine(pulo());
        }
    }

    IEnumerator pulo()
    {
        if (anim.GetBool("andando"))
        {
            estaNoAr = true;
            anim.SetBool("andando", false);
            anim.SetBool("pulando", true);
            yield return new WaitForSeconds(0.5f);
            rb.AddForce(new Vector3(0, 4, 0) * ForcaPulo, ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);
            anim.SetBool("pulando", false);
            estaNoAr = false;
        }
        else
        {
            estaNoAr = true;
            anim.SetBool("pulando", true);
            yield return new WaitForSeconds(0.5f);
            rb.AddForce(new Vector3(0, 4, 0) * ForcaPulo, ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);
            anim.SetBool("pulando", false);
            estaNoAr = false;
        }
    }

    public void FallDamage()
    {
        if (endPosition >= 10f)
        {
            readyDamage = true;
        }
    }

    public void OnCollisionEnter(Collision colisao)
    {
        if (readyDamage && colisao.gameObject.tag == "chao")
        {
            vida -= 5;
            readyDamage = false;
        }
    }

}
