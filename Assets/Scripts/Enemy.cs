using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int VidaEnemy;
    float distancia;
    public Transform player;
    public int speedEnemy;
    public Player p1;

    void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void GetHit()
    {
        VidaEnemy -= 1;
        if (VidaEnemy <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        NPC();
    }

    public void NPC()
    {
        distancia = Vector3.Distance(player.transform.position, transform.position);
        if (distancia <= 10)
        {
            transform.LookAt(player.transform);
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speedEnemy);
        }
    }

    IEnumerator Atacar()
    {
        yield return new WaitForSeconds(3f);
        p1.vida -= 20;
    }

    void OnCollisionEnter(Collision colisao)
    {
        if (colisao.gameObject.tag == "Player")
        {
            StartCoroutine(Atacar());
        }
    }

}
