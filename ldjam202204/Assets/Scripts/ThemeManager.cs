using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ThemeManager : MonoBehaviour
{
    public int level;
    public TextMesh turnText;
    public TextMesh nextText;
    public Transform instruBar;
    public Transform timeBar;
    public AudioSource audioSource;
    public GameObject sanjiaotieFigure;
    public GameObject guFigure;
    public GameObject shachuiFigure;
    public GameObject luoFigure;
    public GameObject sanjiaotie;
    public GameObject gu;
    public GameObject shachui;
    public GameObject luo;
    public  List<int> ready;
    private List<GameObject> readyList;
    // Start is called before the first frame update
    private float[] theme1;
    private float[] theme2;
    private float[] theme3;
    private float[] theme4;
    private float[] theme01;
    private float[] theme02;
    private float[] theme03;
    private float[] theme04;
    public float time = 4;
    private float startTime;
    private float curProcess;
    private bool mode = false;
    private GameObject[] tos;
    public AudioClip drum;
    public int next;
    public int loop = 0;
    private GameObject[] objectsFigure;
    public int last = -1;
    private float barProcess;
    private int instruProcess;
    public int instruAmount = 50;
    public bool over = false;
    void Start() {
        readyList = new List<GameObject>();
        for(int i =0;i<4;i++){
            ready.Add(Random.Range(0,4));
        }
        instruProcess = instruAmount;
        instruBar.localScale = new Vector3(0.52f,1.38f,1);
        timeBar.localScale = new Vector3(0.52f,1.38f,1);
        GameObject[] objects = new GameObject[4]{gu,luo,shachui,sanjiaotie};
        objectsFigure = new GameObject[4]{guFigure,luoFigure,shachuiFigure,sanjiaotieFigure};
        theme01 = new float[2]{0.125f,0.625f};
        theme02 = new float[2]{0.25f,0.75f};
        theme03 = new float[2]{0.375f,0.875f};
        theme04 = new float[1]{0.5f};
        theme1 = new float[4]{6f/4f/12f,15f/4f/12f,24f/4f/12f,33f/4f/12f};
        theme2 = new float[9]{6f/4f/12f,9f/4f/12f,18f/4f/12f,27f/4f/12f,30f/4f/12f,33f/4f/12f,39f/4f/12f,42f/4f/12f,45f/4f/12f};
        theme3 = new float[2]{0.5f,0.75f};
        theme4 = new float[8]{6f/4f/12f,9f/4f/12f,18f/4f/12f,21f/4f/12f,27f/4f/12f,30f/4f/12f,39f/4f/12f,45f/4f/12f};
        startTime = Time.time;
        audioSource.Play();
        tos = new GameObject[4];
        for(int i =0;i<4;i++){
            tos[i] = Instantiate(objects[i]);
            tos[i].GetComponent<ThemeController>().setTurnOff();
            tos[i].GetComponent<ThemeController>().setThemeNumberAndRemake(getTheme(i+1,level));
            tos[i].transform.position = new Vector3(5f,1f-1.5f*i,-3f);
        }
        showReady();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)){
            Quit();
        }
        barProcess = (120f+startTime-Time.time)/120f;
        if(barProcess < 0) barProcess = 0f;
        timeBar.localScale = new Vector3(0.52f,barProcess*1.38f,1);
        curProcess = (Time.time-startTime-time*loop)/time;

        instruBar.localScale = new Vector3(0.52f,(float)instruProcess/(float)instruAmount*1.38f,1);
        if(curProcess>1){
            mode = !mode;
            curProcess=0;
            loop+=1;
            if(!mode){
                for(int i =0;i<4;i++){
                    if(tos[i].GetComponent<ThemeController>().getTurn()){
                        tos[i].GetComponent<ThemeController>().setTurnOff();
                    }
                }
                turnText.text = "Listen!";
            }else{
                relist();
                turnText.text = "Your Turn!";
            }
        }
        if(Input.GetKeyDown(KeyCode.Space) & mode & !over){
            freshList();
        }
        if(barProcess == 0 | instruProcess == 0){
            timeBar.localScale = new Vector3(0.52f,barProcess*1.38f,1);
            instruBar.localScale = new Vector3(0.52f,(float)instruProcess/(float)instruAmount*1.38f,1);
            over = true;
            nextText.text = "You lose!\nYou can\nquit by\npressing\n \"Z\".";
            for(int i =ready.Count-1;i>=0;i--){
                ready.RemoveAt(i);
            }
            showReady();
        }
        if(isFinished()){
            for(int i =ready.Count-1;i>=0;i--){
                ready.RemoveAt(i);
            }
            over = true;
            nextText.text = "You win!\nYou can\nquit by\npressing\n \"Z\".";
            for(int i =ready.Count-1;i>=0;i--){
                ready.RemoveAt(i);
            }
            showReady();
            return;
        }
    }
    public float[] getTheme(int i,int level){
        if(level==2){
            if(i ==1){
                return theme1;
            }else if(i==2){
                return theme2;
            }else if(i==3){
                return theme3;
            }else if(i==4){
                return theme4;
            }else return null;
        }else if(level==1){
            if(i ==1){
                return theme01;
            }else if(i==2){
                return theme02;
            }else if(i==3){
                return theme03;
            }else if(i==4){
                return theme04;
            }else return null;
        }
        else return null;

    }
    public float getTime(){
        return time;
    }
    public float getCurProcess(){
        if(curProcess>1) curProcess = 1;
        if(curProcess<0) curProcess = 0;
        return curProcess;
    }
    public bool getMode(){
        return mode;
    }
    private void showReady(){
        for(int i =readyList.Count-1;i>=0;i--){
            Destroy(readyList[i]);
            readyList.RemoveAt(i);
        }
        for(int i =0;i<ready.Count;i++){            
            GameObject go =  Instantiate(objectsFigure[ready[i]]);
            go.transform.position = new Vector3(-4.45f,2f-i,-1);
            readyList.Add(go);
        }
    }
    private void freshList(){
        if(isFinished()){
            for(int i =ready.Count-1;i>=0;i--){
                ready.RemoveAt(i);
            }
            over = true;
            nextText.text = "You win!\nYou can\nquit by\npressing\n \"Z\".";
            for(int i =ready.Count-1;i>=0;i--){
                ready.RemoveAt(i);
            }
            showReady();
            return;
        }
        int input =0;
        int next = ready[0];
        ready.RemoveAt(0);
        while(true){
            input = Random.Range(0,4);
            if(!tos[input].GetComponent<ThemeController>().isFinished()) break;
        }
        ready.Add(input);
        showReady();
        for(int i =0;i<4;i++){
            tos[i].GetComponent<ThemeController>().setTurnOff();
        }
        tos[next].GetComponent<ThemeController>().setTurnOn();
        instruProcess--;
    }
    private void relist(){
        int next = ready[0];
        for(int i =0;i<4;i++){
            tos[i].GetComponent<ThemeController>().setTurnOff();
        }
        tos[next].GetComponent<ThemeController>().setTurnOn();
    }
    private bool isFinished(){
        for(int i =0;i<4;i++){
            if(!tos[i].GetComponent<ThemeController>().isFinished()) return false;
        }
        return true;
    }
    private void Quit(){
        SceneManager.LoadScene("Mainmenu");
    }
}

