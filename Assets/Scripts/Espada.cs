using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    public bool estaAtacando1;
    public bool tirarVida;
    public Animator anim;
    
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

   
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(estaAtacando());
        }
    }

    IEnumerator estaAtacando()
    {
        if (!estaAtacando1)
        {
            estaAtacando1 = true;
            anim.SetBool("espadada", true);
            yield return new WaitForSeconds(2.5f);
            estaAtacando1 = false;
            anim.SetBool("espadada", false);
        }
    }
    void OnTriggerEnter(Collider colisao)
    {
        if (estaAtacando1 && colisao.gameObject.CompareTag("Enemy"))
        {
            Enemy inimigo = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
            if (estaAtacando1 && inimigo != null)
            {
                inimigo.GetHit();
                estaAtacando1 = false;
            }
        }
    }

}
