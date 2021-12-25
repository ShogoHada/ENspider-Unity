using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Database : MonoBehaviour
{
    public Text WordList;
    public string mean;
    [SerializeField] List<string> CreateWord = new List<string>();

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
        SqliteDatabase sqlDB = new SqliteDatabase("ejdict.sqlite3");

        // Select
        string selectQuery = $"select word, mean from items where word = '{am}' order by level desc limit 1";
        DataTable dataTable = sqlDB.ExecuteQuery(selectQuery);

        string word;
        mean = "";
        foreach (DataRow dr in dataTable.Rows)
        {
 
            word = (string)dr["word"];
            mean = (string)dr["mean"];
            //Debug.Log(word.ToString());
            //Debug.Log(mean.ToString());
            WordList.text = $"{word.ToString()}\n{mean.ToString()}";
            CreateWord.Add(WordList.text);
        }
    }
}