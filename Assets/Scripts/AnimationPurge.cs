using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPurge : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimatorDisable()
    {
        if (animator == null) return;
        animator.enabled = false;
    }
}
