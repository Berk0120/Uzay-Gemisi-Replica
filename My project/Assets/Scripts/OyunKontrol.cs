using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class OyunKontrol : MonoBehaviour
{

    [SerializeField]
    GameObject uzayGemisiPrefab;

   
    
    
    GameObject uzayGemisi;
    List<GameObject> asteroidList = new List<GameObject>(); 

    [SerializeField]
    List<GameObject> asteroidPrefabs = new List<GameObject>();

    [SerializeField]
    int carpan = 5;

    [SerializeField]
    int zorluk = 1;


    UIKontrol uýkontrol;
    
    // Start is called before the first frame update
    void Start()
    {
        uýkontrol = GetComponent<UIKontrol>();
    }



     public void OyunuBaslat()
    {
        uýkontrol.OyunBasladi();
        uzayGemisi = Instantiate(uzayGemisiPrefab);
        uzayGemisi.transform.position = new Vector3(0, EkranHesaplayicisi.Alt + 1.5f);
        asteroidUret(5);
    }


    
  
    void asteroidUret(int adet)
    {
        Vector3 position = new Vector3();
        for (int i = 0; i < adet; i++)
        {
            position.z = -Camera.main.transform.position.z;
            position = Camera.main.ScreenToWorldPoint(position);
            position.x = Random.Range(EkranHesaplayicisi.Sol, EkranHesaplayicisi.Sag);
            position.y = EkranHesaplayicisi.Ust - 1;

            GameObject asteroid = Instantiate(asteroidPrefabs[Random.Range(0, 3)], position,Quaternion.identity);
            asteroidList.Add(asteroid);
        }



    }

    public void AsteroidYokOldu(GameObject asteroid)
    {
        uýkontrol.AsteroidYokOldu(asteroid);
        asteroidList.Remove(asteroid);
       if(asteroidList.Count <= zorluk)
        {
            zorluk++;
            asteroidUret(zorluk * carpan);
        }
    }

    public void OyunuBitir()
    {
        foreach (GameObject asteroid in asteroidList)
        {
            asteroid.GetComponent<Asteroid>().AsteroidYokEt();
        }
        asteroidList.Clear();
        zorluk = 1;
        uýkontrol.OyunBitti();
    }



}
