using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AlphabetGenerator : MonoBehaviour
{
    [SerializeField] GameObject alphabetPrefab = default;
    [SerializeField] Sprite[] alphabetSprites = default;
    [SerializeField] float socialDistance = 1.2f;

    public float span = 10f;
    public float time = 0f;
    public int alphabetID;

    private void Start()
    {
        Spawns(50);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > span)
        {
            int Count = GameObject.FindGameObjectsWithTag("Alphabet").Length +
                GameObject.FindGameObjectsWithTag("MyAlp").Length;
            if (Count < 65)
            {
                Spawns(5);
                time = 0f;
            }
        }
    }

    
    void Spawns(int count)
    {
        //var list = new List<Vector2>();
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = Random.insideUnitCircle * 9f;
            if (GManager.instance.list.Where(_ => Vector2.Distance(pos, _) < socialDistance).Any())
            {
                i--;
                continue;
            }

            Probability();
            GameObject alphabet = Instantiate(alphabetPrefab, pos, Quaternion.identity);
            alphabet.GetComponent<SpriteRenderer>().sprite = alphabetSprites[alphabetID];
            GManager.instance.list.Add(pos);

        }
    }

    public enum SampleRank
    {
        a = 0, b = 1, c = 2, d = 3, e = 4, f = 5, g = 6, h = 7, i = 8, j = 9, k = 10, l = 11, m = 12, n = 13,
        o = 14, p = 15, q = 16, r = 17, s = 18, t = 19, u = 20, v = 21, w = 22, x = 23, y = 24, z = 25
    }

    void Probability()
    {
        var tmp = new Dictionary<SampleRank, int>();
        tmp.Add(SampleRank.a, 6);
        tmp.Add(SampleRank.b, 3);
        tmp.Add(SampleRank.c, 3);
        tmp.Add(SampleRank.d, 4);
        tmp.Add(SampleRank.e, 8);
        tmp.Add(SampleRank.f, 2);
        tmp.Add(SampleRank.g, 3);
        tmp.Add(SampleRank.h, 3);
        tmp.Add(SampleRank.i, 6);
        tmp.Add(SampleRank.j, 6);
        tmp.Add(SampleRank.k, 2);
        tmp.Add(SampleRank.l, 2);
        tmp.Add(SampleRank.m, 4);
        tmp.Add(SampleRank.n, 2);
        tmp.Add(SampleRank.o, 7);
        tmp.Add(SampleRank.p, 6);
        tmp.Add(SampleRank.q, 2);
        tmp.Add(SampleRank.r, 6);
        tmp.Add(SampleRank.s, 6);
        tmp.Add(SampleRank.t, 7);
        tmp.Add(SampleRank.u, 3);
        tmp.Add(SampleRank.v, 2);
        tmp.Add(SampleRank.w, 2);
        tmp.Add(SampleRank.x, 2);
        tmp.Add(SampleRank.y, 2);
        tmp.Add(SampleRank.z, 1);

        var res = ProbabilityRatio<SampleRank>.GetResult(tmp);
        alphabetID = (int)res;
    }
}
