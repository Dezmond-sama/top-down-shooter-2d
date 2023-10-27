using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();        
    }
    public void FadeIn()
    {
        Debug.Log("FadeIn");
        anim.SetTrigger("fadeIn");
    }
    public void FadeOut()
    {
        Debug.Log("FadeOut");
        anim.SetTrigger("fadeOut");
    }
}
