using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemLauncher : MonoBehaviour
{
    public ScriptableObject[] items;
    public GameObject itemPrefab;
    public GameObject canvas;
    public float leftCorner, rightCorner, top;
    public float spawnInterval, minSpawnForce, maxSpawnForce;
    public float spriteSize, itemLifeTime, itemSpawnMultiplier = 0.25f;
    public TextMeshProUGUI itemLaunchMultiplierText;

    #region OnEnable Kommentare
    //Wird aufgerufen, wenn das GameObject aktiviert wird.
    //Startet die Coroutine zum Erscheinen der Items.
    #endregion
    void OnEnable()
    { 
        StartNewSpawnCoroutine();
    }

    #region OnDisable Kommentare
    //Wird aufgerufen, wenn das GameObject deaktiviert wird.
    //Stoppt alle laufenden Coroutines.
    #endregion
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    #region StartNewSpawnCoroutine Kommentare
    //Stoppt die laufende Coroutine und startet eine neue damit es keine Überlappungen gibt.
    #endregion
    private void StartNewSpawnCoroutine()
    {
        StopCoroutine("SpawnItem");
        StartCoroutine("SpawnItem");
    }

    #region UpdateItemMultiplier Kommentare
    //Aktualisiert den Multiplier für den SpawnInterval der Items und startet die Spawn-Coroutine.
    #endregion
    private void UpdateItemMultiplier(float multiplier)
    {
        itemSpawnMultiplier += multiplier;
        itemLaunchMultiplierText.text = "x" + itemSpawnMultiplier.ToString();
        StartNewSpawnCoroutine();
    }

    #region IncreaseItemMultiplier Kommentare
    //Erhöht den Multiplier für den SpawnInterval der Items.
    #endregion
    public void IncreaseItemMultiplier()
    {
        UpdateItemMultiplier(0.25f);
    }

    #region DecreaseItemMultiplier Kommentare
    //Verringert den Multiplier für den SpawnInterval der Items.
    #endregion
    public void DecreaseItemMultiplier()
    {
        UpdateItemMultiplier(-0.25f);
    }

    #region LaunchItem Kommentare
    //Spawnt ein neues Item und schießt es von oberen Bildschirm rand nach unten.
    #endregion
    private void LaunchItem()
    {
        GameObject instantiatedItem = Instantiate(itemPrefab, canvas.transform);
        ItemLauncherItem launcherItem = instantiatedItem.GetComponent<ItemLauncherItem>();
        launcherItem.spriteSize = spriteSize;
        launcherItem.item = (Item)items[Random.Range(0, items.Length)];
        instantiatedItem.transform.position = new Vector3(Random.Range(leftCorner, rightCorner), top, 0);
        instantiatedItem.GetComponent<Rigidbody>().AddForce(new Vector2(0, -Random.Range(minSpawnForce, maxSpawnForce)));
        StartCoroutine(DestroyItself(instantiatedItem));
    }

    #region SpawnItem Kommentare
    //Coroutine für das Spawnen der Items im SpawnInterval.
    #endregion
    public IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(spawnInterval / itemSpawnMultiplier);
        while (true)
        {
            LaunchItem();
            yield return new WaitForSeconds(spawnInterval / itemSpawnMultiplier);
        }
    }

    #region DestroyItself Kommentare
    //Coroutine, die das Item in der Szene nach einer bestimmten Zeit zerstört.
    #endregion
    public IEnumerator DestroyItself(GameObject gameObject)
    {
        yield return new WaitForSeconds(itemLifeTime);
        Destroy(gameObject);
    }
}
