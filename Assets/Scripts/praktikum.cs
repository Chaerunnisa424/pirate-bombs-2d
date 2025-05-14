using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class praktikum : MonoBehaviour
{
    public int health = 80;
    int damage = 20;
    public bool isAlive = true;
    //public int health = 100;
    //public float speed = 5.5f;
    //public bool isAlive = true;
    //public string playerName = "Sadako";
    //public char grade = 'A';

    // Start is called before the first frame update
    void Start()
    {
        health = health - damage;
        if (health > 50)
        {
            Debug.Log("Player masih kuat");
        }
        else if (health > 0)
        {
            Debug.Log("Player harus berhati hati");
        }
        else
        {
            Debug.Log("Player mati");
            isAlive = false;
        }
        //Debug.Log("Nama Pemain: " + playerName);
        //Debug.Log("Darah: " + health);
        //Debug.Log("Kecepatan: " + speed);
        //Debug.Log("Status Hidup: " + isAlive);
        //Debug.Log("Peringkat: " + grade);

        //Operator Dasar
        //int damage = 20;
        //health = health - damage;
        //Debug.Log("Darah Sekarang" + health);

        //bool isDead = (health <= 0);
        //Debug.Log("Apakah Player Mati?" + isDead);

        //Operator Logika
        //if(isAlive && health > 0)
        //{
        //    Debug.Log("Pemain masih bisa bertarung");
        //} 
        //else
        //{
        //    Debug.Log("Pemain telah mati");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        // Tambahkan logika update di sini jika diperlukan
    }
}