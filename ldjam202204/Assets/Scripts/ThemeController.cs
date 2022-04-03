using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeController : MonoBehaviour
{
    // Start is called before the first frame update
    private LineController line;
    private GameObject sign;
    private Animator signAnimate;
    void Awake()
    {      
        line = GetComponent<Transform>().Find("line").GetComponent<LineController>();
        sign = GetComponent<Transform>().Find("sign").gameObject;
        signAnimate = sign.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void setThemeNumberAndRemake(float[] theme){
        line.Remake(theme);
    }
    public void setTurnOn(){
        if(!line.getTurn()){
            line.turnOn();
            sign.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }
    }
    public bool getTurn(){
        return line.getTurn();
    }
    public void setTurnOff(){
        if(line.getTurn()){
            line.turnOff();
            sign.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }
    public bool isFinished(){
        return line.isFinished();
    }
}
