using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed;
    public Text displayText;
    // Use this for initialization
    void Start()
    {

    }

    public void SetText(string s, Color col)
    {
        displayText.text = s;
        displayText.color = col;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
    }
}
