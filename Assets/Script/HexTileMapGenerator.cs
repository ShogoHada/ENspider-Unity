using System.Collections;
using UnityEngine;

public class HexTileMapGenerator : MonoBehaviour
{
    public GameObject hexTilePrefab;
    public Transform holder;

    [SerializeField] int mapWidth = 25;
    [SerializeField] int mapHeight = 12;
    [SerializeField] private GameObject TileGenerator;

    float tileXOffset = 1.8f;
    float tileYOffset = 1.565f;

    void Start ()
    {
        CreateHexTileMap();
    }

    void CreateHexTileMap ()
    {
        float mapXMin = -mapWidth / 2;
        float mapXMax = mapWidth / 2;

        float mapYMin = -mapHeight / 2;
        float mapYMax = mapHeight / 2;
        for(float x = mapXMin; x <= mapXMax; x++)
        {
            for(float y = mapYMin; y <= mapYMax; y++)
            {
                GameObject TempGO = Instantiate(hexTilePrefab);
                Vector3 pos;

                if(y % 2 == 0)
                {
                    pos = new Vector3(x * tileXOffset, y * tileYOffset, 0f);
                }
                else 
                {
                    pos = new Vector3(x * tileXOffset + tileXOffset / 2, y * tileYOffset, 0);
                }
                StartCoroutine(SetTileInfo(TempGO, x, y, pos));
                
            }
        }
    }

    IEnumerator SetTileInfo(GameObject GO, float x, float y, Vector3 pos)
    {
        yield return new WaitForSeconds(0.00001f);
        GO.transform.parent = holder;
        GO.name = x.ToString() + "," + y.ToString();
        GO.transform.position = pos;
        if (x == -2 && y == -2)
        {
            GO.GetComponent<Renderer>().material.color = Color.blue;
            string newTag = "MyTile";
            GO.tag = newTag;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            Destroy(other.gameObject);
        }
    }
}