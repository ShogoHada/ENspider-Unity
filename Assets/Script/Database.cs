using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Database : MonoBehaviour
{
    public Text WordList;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void word(string am)
    {
        //string am = "cat";
        SqliteDatabase sqlDB = new SqliteDatabase("ejdict.sqlite3");

        // Select
        string selectQuery = $"select word, mean from items where word = '{am}' order by level desc limit 1";
        DataTable dataTable = sqlDB.ExecuteQuery(selectQuery);

        string word;
        string mean;
        foreach (DataRow dr in dataTable.Rows)
        {
            //word = (string)dr["lemma"];
            //Debug.Log("lemma:");
            word = (string)dr["word"];
            mean = (string)dr["mean"];
            Debug.Log(word.ToString());
            Debug.Log(mean.ToString());
            WordList.text = $"{word.ToString()}\n{mean.ToString()}";
        }
    }
}