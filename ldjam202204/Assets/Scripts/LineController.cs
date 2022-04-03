using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public ThemeManager themeManager;
    public GameObject pointer;
    public Transform left;
    public Transform right;
    private Transform self;
    private float distance;
    public GameObject triggered;
    public List<GameObject> p;
    public Transform theme;
    private bool turn = true;
    // Start is called before the first frame update
    void Awake()
    {
        p = new List<GameObject>();
        themeManager = GameObject.Find("ThemeManager").GetComponent<ThemeManager>();
        self = GetComponent<Transform>();
        theme = self.transform.parent;
        distance = right.position.x-left.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float curPorcess = themeManager.getCurProcess();
        if(themeManager.getMode() & turn){
            if(Input.GetKeyDown(KeyCode.Space) & !GameObject.Find("ThemeManager").GetComponent<ThemeManager>().over){
                if(triggered!=null){
                    triggered.GetComponent<PointController>().Show();
                    triggered.gameObject.transform.GetChild(0).GetComponent<BeatController>().Play();
                    triggered = null;
                }
            }
        }
        self.position = new Vector2(left.position.x+distance*curPorcess,self.position.y);
    }
    public void Remake(float[] themeNums){
        for(int i =0;i<p.Count;i++){
            Destroy(p[i]);
        }
        p.Clear();
        for(int i =0;i<themeNums.Length;i++){
            p.Add(Instantiate(pointer));
            p[i].gameObject.transform.parent = theme;
            p[i].transform.position = new Vector2(left.transform.position.x+themeNums[i]*distance,pointer.transform.position.y);
            p[i].gameObject.GetComponent<Renderer>().enabled = true;
            p[i].gameObject.GetComponent<PointController>().Hide();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(themeManager.getMode()){
            if (other.tag=="Point"){
                if(!other.gameObject.GetComponent<PointController>().isShown()){
                    triggered = other.gameObject;
                }
            }else if(other.tag=="Beat"){
                if(other.gameObject.transform.parent.GetComponent<PointController>().isShown()){
                    other.gameObject.GetComponent<BeatController>().Play();
                }
            }
        }else{
            if(other.tag=="Beat"){
                other.gameObject.GetComponent<BeatController>().Play();
            }
        }

        
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag=="Point"){
            if (!other.gameObject.GetComponent<PointController>().isShown()){
                triggered = null;
            }
        }
    }
    public bool getTurn(){
        return turn;
    }
    public void turnOn(){
        turn = true;
    }
    public void turnOff(){
        turn = false;
    }
    public bool isFinished(){
        for(int i =0;i<p.Count;i++){
            if(!p[i].GetComponent<PointController>().isShown()) return false;
        }
        return true;
    }
}

