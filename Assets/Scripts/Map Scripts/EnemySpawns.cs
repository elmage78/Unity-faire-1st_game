using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawns : MonoBehaviour
{
    public GameObject MasitaOriginal;
    public GameObject Enemies;
    public TilemapCollider2D Floor;
    public Rigidbody2D FloorRB;
    public Rigidbody2D WallsRB;
    public Rigidbody2D Piedrita;
    int a = 0;

    private float timer;
    private void Start()
    {
    }

    public static bool OnBoard(Vector2 pos, Rigidbody2D Boxes)
    {
        if (Boxes.OverlapPoint(pos))
        {
            return true;
        }
        return false;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (this.transform.childCount < 4 && timer >= 20f)
        {
            timer = 0f;
            CloneMasita(1);
        }
    }
    void CloneMasita(int masitaNum)
    {
        for (int i = 0; i < masitaNum; i++)
        {
            Vector3 SpawnPos = new Vector3(Random.Range(Floor.bounds.min.x, Floor.bounds.max.x), Random.Range(Floor.bounds.min.y, Floor.bounds.max.y), 0);
             while (!OnBoard(new Vector2(SpawnPos.x, SpawnPos.y), FloorRB))
             {
                 while (OnBoard(new Vector2(SpawnPos.x, SpawnPos.y), WallsRB))
                 {
                     SpawnPos.x = Random.Range(Floor.bounds.min.x, Floor.bounds.max.x);
                     SpawnPos.y = Random.Range(Floor.bounds.min.y, Floor.bounds.max.y);
                 }
                while (OnBoard(new Vector2(SpawnPos.x, SpawnPos.y), Piedrita))
                {
                    SpawnPos.x = Random.Range(Floor.bounds.min.x, Floor.bounds.max.x);
                    SpawnPos.y = Random.Range(Floor.bounds.min.y, Floor.bounds.max.y);
                }
                SpawnPos.x = Random.Range(Floor.bounds.min.x, Floor.bounds.max.x);
                 SpawnPos.y = Random.Range(Floor.bounds.min.y, Floor.bounds.max.y);
             }
            GameObject CloneMasita = Instantiate(MasitaOriginal, SpawnPos, MasitaOriginal.transform.rotation);
            CloneMasita.transform.parent = this.transform;
            CloneMasita.name = "Masita Nº" + (a); a++;
            CloneMasita.SetActive(true);
        }
    }
}
