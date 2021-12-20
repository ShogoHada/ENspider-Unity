using UnityEngine;
using System.Collections;

public class Database : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // Insert
        SqliteDatabase sqlDB = new SqliteDatabase("wnjpn.db");
        string query = "insert into word values(1, 'Yamada', 19, 'Tokyo')";
        sqlDB.ExecuteNonQuery(query);

        // Select
        string selectQuery = "select * from word";
        DataTable dataTable = sqlDB.ExecuteQuery(selectQuery);

        string name = "";
        foreach (DataRow dr in dataTable.Rows)
        {
            name = (string)dr["lemma"];
            Debug.Log("lemma:");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}