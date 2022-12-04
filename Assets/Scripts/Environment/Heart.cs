using Manasoup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    public float _timeBeforeDeactivate;
    public int _amountToHeal;
    private void OnEnable()
    {
        StartCoroutine(StartDisappearCount());
    }


    private IEnumerator StartDisappearCount()
    {
        yield return new WaitForSeconds(_timeBeforeDeactivate);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        GameManager.Instance.player.Heal(_amountToHeal);
        gameObject.SetActive(false);
    }
}

