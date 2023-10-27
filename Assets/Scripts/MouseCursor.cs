using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        cam = FindObjectOfType<CameraController>().GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }
}
