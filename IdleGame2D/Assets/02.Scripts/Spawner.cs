using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Monsters;
    public int Count;
    public int SpawnTime;

    private void Start()
    {
        StartCoroutine(C_Spawn());
    }

    IEnumerator C_Spawn()
    {
        while (true)
        {

            for (int i = 0;  i < Count;  i++)
            {
                Vector2 pos = Random.insideUnitCircle * Random.Range(5, 10);
                Instantiate(Monsters[0], pos, Quaternion.identity);
            }
            yield return new WaitForSeconds(SpawnTime);
            

        }
        
    }
}
