using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemLauncher : MonoBehaviour
{
    public ScriptableObject[] items;
    public GameObject itemPrefab;
    public GameObject canvas;
    public float leftCorner, rightCorner, top;
    public int minSpawnForce, maxSpawnForce;
    public float spriteSize, itemLifeTime, itemSpawnMultiplier = 0;
    public TextMeshProUGUI itemLaunchMultiplierText;

    #region OnEnable Kommentare
    //Wird aufgerufen, wenn das GameObject aktiviert wird.
    //Startet die Coroutine zum Spawnen der Items.
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
    //Stoppt die laufende Coroutine und startet eine neue damit nur eine Coroutine läuft.
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
        itemSpawnMultiplier = multiplier;
        itemLaunchMultiplierText.text = itemSpawnMultiplier.ToString() + "x";
        StartNewSpawnCoroutine();
    }

    #region IncreaseItemMultiplier Kommentare
    //Verändert den Multiplier für den SpawnInterval der Items nach Slider Value.
    #endregion
    public void IncreaseDecreaseItemMultplier(Slider slider)
    {
        UpdateItemMultiplier(slider.value);
    }

    #region LaunchItem Kommentare
    //Spawnt ein neues Item und schießt es von dem oberen Bildschirm rand nach unten.
    //Das Szenen Item wird nach einer bestimmten Zeit zerstört.
    //Das Szenen Item bekommt ein zufälliges Item zugewiesen.
    //Das Szenen Item bekommt eine zufällige geschwindigkeit zugewiesen.
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
    //Die Spawnrate wird durch den Multiplier beeinflusst.
    #endregion
    public IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            if (itemSpawnMultiplier > 0)
            {
                LaunchItem();
            }
            yield return new WaitForSeconds(1 / itemSpawnMultiplier);
        }
    }

    #region DestroyItself Kommentare
    //Coroutine, die das Item in der Szene nach dem float itemLifeTime zerstört.
    #endregion
    public IEnumerator DestroyItself(GameObject gameObject)
    {
        yield return new WaitForSeconds(itemLifeTime);
        Destroy(gameObject);
    }
}
