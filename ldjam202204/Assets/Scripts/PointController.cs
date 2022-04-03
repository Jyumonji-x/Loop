using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public GameObject particular;
    private bool shown;
    // Start is called before the first frame update
    void Awake()
    {
        particular = transform.Find("particular").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Show(){
        GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
        particular.GetComponent<ParticleSystem>().Play();
        shown = true;
        
    }
    public void Hide(){
        GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.3f);
        shown = false;
    }
    public bool isShown(){
        return shown;
    }
}
