using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumButton : MonoBehaviour
{
    
    private Vector3 startPos;
    private Vector3 endPos;
    public float movement;
    public int buttonID;
    private int index;
    private GameObject bigObject;
    public GameObject[] objectList;
    public Transform spawnPoint;
    public Material objectMat;
    private AudioSource audioSource;
    public GameObject canvas;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 200.0f))
            {
                if (hit.transform.gameObject == this.transform.gameObject)
                {
                    StartCoroutine(ButtonPush(0.5f));
                    if (buttonID == 1)
                    {
                        ChangeObject();
                    }
                    else if (buttonID == 2)
                    {
                        ToggleCanvas();
                    }

                }
            }
        }
    }

    IEnumerator ButtonPush(float seconds)
    {
        startPos = this.transform.position;
        endPos = new Vector3(startPos.x, startPos.y - movement, startPos.z);

        audioSource.Play();
        this.transform.position = endPos;
        yield return new WaitForSeconds(seconds);
        this.transform.position = startPos;
    }

    void ChangeObject()
    {
        if (bigObject == null)
        {
            index = Random.Range(0, 2);
        }
        else
        {
            Destroy(bigObject.gameObject);
            index ++;
        }
        bigObject = Instantiate(objectList[index % 3], spawnPoint);

    }

    void ChangeObjectColor()
    {
        objectMat.SetColor("_BaseColor", Random.ColorHSV());
    }

    void ToggleCanvas()
    {
        canvas.SetActive(!canvas.activeSelf);
    }

    
}
