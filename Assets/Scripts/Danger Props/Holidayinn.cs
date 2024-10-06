using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holidayinn : MonoBehaviour
{
    private Animator anim;
    private bool isWorking;
    [SerializeField] private float cooldown;
    private float cooldownTimer;
    [SerializeField] private AudioClip sniperSound;
    [SerializeField] private GameObject canvas;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        canvas.SetActive(false);
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0)
        {
            isWorking = true;
            SoundManager.instance.PlaySound(sniperSound);
            cooldownTimer = cooldown;

            StartCoroutine(DisplayCanvasAndDisable());
        }
        anim.SetBool("isWorking", isWorking);
    }

    private IEnumerator DisplayCanvasAndDisable()
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        canvas.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        isWorking = false;
    }
}
