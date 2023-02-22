using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject gameOverScreen;
    public UIOnClick uicontroller;
    public LayerMask goalOnly;

    public void OnCollisionEnter(Collision other)
    {
        if ((goalOnly.value & (1 << other.transform.gameObject.layer)) <= 0)
        {
            gameOverScreen.SetActive(true);
            Destroy(gameObject);
            uicontroller.done = true;
        }
    }
}
