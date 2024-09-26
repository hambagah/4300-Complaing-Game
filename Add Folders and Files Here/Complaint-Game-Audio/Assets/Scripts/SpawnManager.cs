
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] Product[] products;

    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawners");
        for (int i = 0; i < spawnPoints.Length; i++) {
            for (int o = -1; o < 2; o+=2) {
                for (int p = -1; p < 1; p++) {
                    if (Random.Range(0, 2) == 0) {
                        Vector3 spawnPos = spawnPoints[i].transform.position;
                        spawnPos.y += (float)72 * o;
                        spawnPos.x += p * 120;
                        Product product = Instantiate(products[Random.Range(0, products.Length)], new Vector3(spawnPos.x, spawnPos.y, spawnPos.z), Quaternion.identity);
                        product.transform.SetParent(GameObject.Find("Products").transform, true);
                    }
                }
            }
        }
    }
}
