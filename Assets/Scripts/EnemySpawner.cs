using UnityEngine;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemySpawner : MonoBehaviour {
    public GameObject[] enemyType;
    public int maxEnemies;
    private GroundGenerator gg;
    private GameObject player;
    private List<GameObject> enemies;
    private System.Random random;

	void Start () {
        gg = GameObject.FindGameObjectWithTag("GroundGenerator").GetComponent<GroundGenerator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        random = new System.Random();
	}
	
	void Update () {
        if (!HasSpawned() && gg.HasBeenGenerated(gg.groundWidth * 3) 
            && enemies.Count() < maxEnemies) {
            SpawnEnemies(random.Next(0, 4));
        }
    }

    void SpawnEnemies(int amount) {
        for (int i = 0; i < amount; i++) {
            int offsetX = random.Next(0, (int)gg.groundWidth);
            int enemyIndex = random.Next(0, enemyType.Length);

            GameObject newEnemy = (GameObject)Instantiate(enemyType[enemyIndex], 
                new Vector3(player.transform.position.x + gg.groundWidth + offsetX, 0), 
                Quaternion.identity);

            enemies.Add(newEnemy);
        }
    }

    bool HasSpawned() {
        float playerX = player.transform.position.x;
        float offsetY = 15;
        Collider2D[] colliders = Physics2D.OverlapAreaAll(new Vector2(playerX, offsetY), 
            new Vector2(playerX + gg.groundWidth * 2, -offsetY));

        foreach (Collider2D col in colliders) {
            if (col.tag == "Enemy") {
                return true;
            }
        }
        return false;
    }

    public void DestroyEnemy(GameObject enemy) {
        Destroy(enemy);
        enemies.Remove(enemy);
    }

    public void DestroyAllEnemies() {
        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }
        enemies.Clear();
    }
}

#if UNITY_EDITOR
// Botões para debug (inspector)
[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        EnemySpawner spawner = (EnemySpawner)target;
        if (GUILayout.Button("Destroy all enemies")) {
            spawner.DestroyAllEnemies();
        }
    }
}
#endif
