using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour {

    public GameObject particlePrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 0.1f;
    public Color particleColor = new Color(0.43f, 0.35f, 0.27f, 0.92f);

    private float spawnTimer = 0f;

    void Update () {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f) {
            SpawnParticle();
            spawnTimer = spawnInterval;
        }
    }

    void SpawnParticle () {
        foreach (Transform spawnPoint in spawnPoints) {
            Vector2 spawnPos = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
            GameObject particle = Instantiate(particlePrefab, spawnPos, Quaternion.identity);
            SpriteRenderer spriteRenderer = particle.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) {
                spriteRenderer.color = particleColor;
            }
        }
    }
}
