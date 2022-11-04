using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public  Vector2 CameraChange;
    public  Vector3 PlayerChange;
    private CameraMovement camera;
    public bool needText;
    public string placename;
    public GameObject text;
    public Text placetext;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            camera.minPosition += CameraChange;
            camera.maxPosition += CameraChange;
            other.transform.position += PlayerChange;
            if(needText)
            {
                StartCoroutine(placeName());
            }
        }
    }

    private IEnumerator placeName()
    {
        text.SetActive(true);
        placetext.text = placename;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
