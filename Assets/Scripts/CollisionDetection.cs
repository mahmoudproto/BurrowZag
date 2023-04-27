using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazards"))
        {
            VibratorController.Instance.Vibrate(0.25f);
            //long[] pattern = { 0, 100, 500, 1000, 500, 200, 100 };
            //VibratorController.Instance.Vibrate(pattern, -1);
            GameManager.instance.PlayerHitHazard();
        }
    }
}
