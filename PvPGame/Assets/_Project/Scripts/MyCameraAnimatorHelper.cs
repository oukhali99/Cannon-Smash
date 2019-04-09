using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraAnimatorHelper : MonoBehaviour {
    [SerializeField] private float screenChangeCooldown;

    private Animator anim;
    private Camera cam;

    private GameObject player;
    private float timer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        cam = GetComponent<Camera>();
        timer = 0;
    }

    private void Start()
    {
        player = Global.Instance.Player;        
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (cam.WorldToScreenPoint(player.transform.position).x > cam.pixelWidth && timer > screenChangeCooldown)
        {
            timer = 0;
            anim.SetInteger("screen", anim.GetInteger("screen") + 1);
        }
        else if (cam.WorldToScreenPoint(player.transform.position).x < 0 && timer > screenChangeCooldown)
        {
            timer = 0;
            anim.SetInteger("screen", anim.GetInteger("screen") - 1);
        }
    }
}
