using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] m_WayPoints = null;
    private int m_Count = 0;
    private float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = m_WayPoints[0].position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, m_WayPoints[m_Count].position, Time.deltaTime*speed);
        if (Vector2.Distance(transform.position, m_WayPoints[m_Count].position) <= 0.5f)
        {
            m_Count++;
            if(m_Count == m_WayPoints.Length)
            {
                m_Count = 0;
            }
        }

    }
}
