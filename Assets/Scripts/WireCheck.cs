using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireCheck : MonoBehaviour
{
    private int m_isGround = 0;
    private int m_isWire = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wire"))
        {
            m_isWire++;
            //Debug.Log("Wire enter");
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGround++;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wire"))
        {
            //Debug.Log("Wire stay");
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            //
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wire"))
        {
            if (m_isWire > 0) m_isWire--;
            //Debug.Log("Wire exit");
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (m_isGround > 0) m_isGround--;
        }
    }

    public bool IsWire()
    {
        return m_isWire > 0 && !IsGround();
    }

    public bool IsGround()
    {
        return m_isGround > 0;
    }
}
