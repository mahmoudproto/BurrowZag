using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleRandomizer : MonoBehaviour
{
    [SerializeField] Collectible[] collectablesInArea;
    public void Randomize()
    {
        if (collectablesInArea == null || collectablesInArea.Length == 0)
            return;
        int selected = Random.Range(0, collectablesInArea.Length);
        for(int i=0;i<collectablesInArea.Length;i++)
        {
            //if selected == i setActive=true, else false
            collectablesInArea[i].gameObject.SetActive(selected == i ? true : false);

            collectablesInArea[i].GetComponent<SpriteRenderer>().enabled = true;

        }
        GameManager.instance.Fire_NewEnergyPackGenerated(collectablesInArea[selected]);
    }
}
