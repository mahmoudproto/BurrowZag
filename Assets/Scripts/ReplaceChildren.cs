using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ReplaceChildren : MonoBehaviour
{
    public GameObject Prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Replace child")]
    public void ReplaceChildrenwith()
    {
        SpriteRenderer[] children = transform.GetComponentsInChildren<SpriteRenderer>();
        foreach (var child in children)
        {
            GameObject newchild = GameObject.Instantiate(Prefab);
            newchild.transform.parent = transform;
            newchild.transform.position = child.transform.position;
            newchild.transform.rotation = child.transform.rotation;
            newchild.transform.localScale = child.transform.localScale;

            DestroyImmediate(child.gameObject);
        }
    }
}
