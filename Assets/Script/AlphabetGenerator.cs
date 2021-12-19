using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AlphabetGenerator : MonoBehaviour
{
    [SerializeField] GameObject alphabetPrefab = default;
    [SerializeField] Sprite[] alphabetSprites = default;
    [SerializeField] int count = 50;
    [SerializeField] float socialDistance = 1.5f;

    private void Start()
    {
        Spawns();
        
    }

    public void Spawns()
    {
        var list = new List<Vector2>();
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = Random.insideUnitCircle * 9f;
            if (list.Where(_ => Vector2.Distance(pos, _) < socialDistance).Any())
            {

                i--;
                continue;
            }
            GameObject alphabet = Instantiate(alphabetPrefab, pos, Quaternion.identity);
            int alphabetID = Random.Range(0, alphabetSprites.Length);
            alphabet.GetComponent<SpriteRenderer>().sprite = alphabetSprites[alphabetID];
            //alphabet.GetComponent<Alphabet>().id = alphabetID;
            list.Add(pos);
        }
    }

    void Update()
    {
        
    }
}
