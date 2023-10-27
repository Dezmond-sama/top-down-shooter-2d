using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    static bool isCreated = false;
    public float smoothTime;
    //private static bool isCreated;

    public BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    public float halfHeight;
    public float halfWidth;

    private Camera theCamera;
    Vector3 velocity = Vector3.zero;

    //private AudioListener listener;

    // Use this for initialization
    void Start()
    {
        if (isCreated) {
        	Destroy (transform.gameObject);
        } else {
        	DontDestroyOnLoad (transform.gameObject);
        	isCreated = true;
            theCamera = GetComponent<Camera>();

            GetBounds();

            halfHeight = theCamera.orthographicSize;
            halfWidth = halfHeight * Screen.width / Screen.height;
        }
        
        //listener = FindObjectOfType<AudioListener> ();
        //if (PlayerPrefs.HasKey("Volume")) AudioListener.volume = PlayerPrefs.GetFloat("Volume");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null) theCamera.transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x, target.position.y, theCamera.transform.position.z), ref velocity,smoothTime);

        if (boundBox == null)
        {
            GetBounds();
        }

        float clampedX, clampedY;
        if (minBounds.x + halfWidth < maxBounds.x - halfWidth)
        {
            clampedX = Mathf.Clamp(theCamera.transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        }
        else
        {
            clampedX = (minBounds.x + maxBounds.x) / 2;
        }
        if (minBounds.y + halfHeight < maxBounds.y - halfHeight)
        {
            clampedY = Mathf.Clamp(theCamera.transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        }
        else
        {
            clampedY = (minBounds.y + maxBounds.y) / 2;
        }

        theCamera.transform.position = new Vector3(clampedX, clampedY, theCamera.transform.position.z);
    }

    public void SetCameraPosition()
    {
        if (theCamera == null) return;
        if (target != null) theCamera.transform.position = new Vector3(target.position.x, target.position.y, theCamera.transform.position.z);
        
        if (boundBox == null)
        {
            GetBounds();
        }

        float clampedX, clampedY;
        if (minBounds.x + halfWidth < maxBounds.x - halfWidth)
        {
            clampedX = Mathf.Clamp(theCamera.transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        }
        else
        {
            clampedX = (minBounds.x + maxBounds.x) / 2;
        }
        if (minBounds.y + halfHeight < maxBounds.y - halfHeight)
        {
            clampedY = Mathf.Clamp(theCamera.transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        }
        else
        {
            clampedY = (minBounds.y + maxBounds.y) / 2;
        }

        theCamera.transform.position = new Vector3(clampedX, clampedY, theCamera.transform.position.z);
    }

    //public void EndGame()
    //{
    //    StartCoroutine("Gameover");
    //}
    //IEnumerator Gameover()
    //{
    //    //gameObject.SetActive (false);
    //    yield return new WaitForSeconds(2);
    //    SceneManager.LoadScene("gameover");
    //}
    public void GetBounds()
    {
        if (boundBox == null)
        {
            BoundsController bc = FindObjectOfType<BoundsController>();
            if (bc != null) boundBox = bc.GetComponent<BoxCollider2D>();
        }
        if (boundBox != null)
        {
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
        }
        else
        {
            minBounds = Vector3.zero;
            maxBounds = Vector3.zero;
        }
    }
    public void SetBounds(BoxCollider2D newBounds)
    {
        boundBox = newBounds;

        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
    }
}
