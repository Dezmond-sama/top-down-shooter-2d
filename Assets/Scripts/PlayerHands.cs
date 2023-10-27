using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    private Vector2 mousePos = Vector2.zero;
    public Rigidbody2D rb;
    public LineRenderer line_1;
    public LineRenderer line_2;
    public Transform point_1;
    public Transform point_2;


    void LateUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        if (angle > -180f && angle < 0f)
        {
            line_1.SetPosition(1, point_1.position - line_1.gameObject.transform.position);
            line_2.SetPosition(1, point_2.position - line_2.gameObject.transform.position);
        }
        else
        {
            line_1.SetPosition(1, point_2.position - line_1.gameObject.transform.position);
            line_2.SetPosition(1, point_1.position - line_2.gameObject.transform.position);
        }
    }
}
