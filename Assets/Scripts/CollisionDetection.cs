using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public static event Action onPlayerHitHazard;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazards"))
        {
            //VibratorController.Instance.Vibrate();
            VibratorController.Instance.Vibrate(.25f,255);
            //long[] pattern = { 0, 100, 500, 1000, 500, 200, 100 };
            //VibratorController.Instance.Vibrate(pattern, -1);
            Hazard other = collision.gameObject.GetComponent<Hazard>();
            if (other != null && other.Shatter())
            {

            }
            else
            {
                onPlayerHitHazard?.Invoke();
                GameManager.instance.PlayerHitHazard();
                SoundManager.Instance.PlayBlockHit();
            }
        }
    }

    //near hit warning

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Hazards"))
    //        this.GetComponent<SpriteRenderer>().color = Color.yellow;
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Hazards"))
    //        this.GetComponent<SpriteRenderer>().color = Color.white;
    //}

    private void OnDestroy()
    {
        onPlayerHitHazard = null;
    }

}
