using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimation : MonoBehaviour
{
    [SerializeField]
    private float animationDuration = 1;
    [SerializeField]
    private AnimationCurve animationCurveForPosition;
    [SerializeField]
    private Vector2 targetPositionDelta = Vector2.zero;
    [SerializeField]
    private Vector2 startPosition = Vector2.zero;
    [SerializeField]
    private GameObject toBeAnimated;

    private float animationStartTime = 0;

    [ContextMenu("Animate")]
    public void Animate()
    {
        animationStartTime = Time.time;
        toBeAnimated.SetActive(true);
        startPosition = transform.position;
        
        StartCoroutine("AnimateCollectible");
    }

    IEnumerator AnimateCollectible()
    {
        float lerpValue = 0;
        Vector2 target = startPosition + targetPositionDelta;
        while (lerpValue < 1)
        {
            print("Animating");
            lerpValue = animationCurveForPosition.Evaluate((Time.time - animationStartTime) / animationDuration);
            toBeAnimated.transform.position = transform.position * (1 - lerpValue) + (Vector3)target * lerpValue;
            yield return new WaitForEndOfFrame();
            print("Animation Done");
        }
            toBeAnimated.SetActive(false);
    }
}
