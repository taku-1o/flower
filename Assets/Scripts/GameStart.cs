using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public bool m_IsAnimEnded { get; private set; } = false;
    public bool m_IsHideNow { get; private set; } = false;
    public bool m_IsHideEnded { get; private set; } = false;

    private Animator m_animator;
    private AudioSource m_audioSource;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
    }

    public void PlayClashSound(AudioClip audioClip)
    {
        m_audioSource.PlayOneShot(audioClip);
    }

    public void SetAnimatorStop(bool stop)
    {
        if (stop)
        {
            m_animator.speed = 0;
        }
        else
        {
            m_animator.speed = 1;
        }
    }

    public void AnimEnded()
    {
        m_IsAnimEnded = true;
    }

    public void Hide()
    {
        if (!m_IsHideNow)
        {
            m_IsHideNow = true;
            GetComponent<Animator>().Play("Hide");
        }
    }

    public void HideEnded()
    {
        m_IsHideNow = false;
        m_IsHideEnded = true;
    }

    public bool IsAnimStopping()
    {
        return m_animator.speed == 0;
    }
}
