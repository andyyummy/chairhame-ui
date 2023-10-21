using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.IO.Ports;

public class Key : MonoBehaviour
{
    [DllImport ("user32.dll",EntryPoint ="keybd_event")]
    public static extern void keybd_event(
        byte bvk,
        byte bScan,
        int dwFlags,
        int dwExtraInfo
    );
    SerialPort serialPort;
    // Start is called before the first frame update
    void Start()
    {
        serialPort = new SerialPort("COM3", 9600);
        serialPort.Open();

    }
    void Update()
    {
        
        if (serialPort.IsOpen)
        {
            try{
                string data = serialPort.ReadLine(); 
                Keyboard(data);
                Debug.Log(data);
            }
            catch(System.Exception e){
                Debug.LogError(e.Message);
            }

        }
    }
    // void Keyboard(string data){
    //     if(data=="L"){
    //         Debug.Log("L");
    //         keybd_event(37,0,0,0);
    //     }
    //     else if(data=="R"){
    //         Debug.Log("R");
    //         keybd_event(39,0,0,0);
    //     }
    //     else if(data=="F"){
    //         Debug.Log("F");
    //         keybd_event(38,0,1,0);
    //     }
    //     else if(data=="B"){     
    //         Debug.Log("B");
    //         keybd_event(40,0,1,0);
    //     }
    //     }
    void Keyboard(string data)
{
    keybd_event(37, 0, 2, 0); // 释放左箭头键
    keybd_event(39, 0, 2, 0); // 释放右箭头键
    keybd_event(38, 0, 2, 0); // 释放上箭头键
    keybd_event(40, 0, 2, 0); // 释放下箭头键
    if (data == "L")
    {
        keybd_event(37, 0, 0, 0); // 按下左箭头键
    }
    else if (data == "R")
    {  
        keybd_event(39, 0, 0, 0); // 按下右箭头键
    }
    else if (data == "F")
    {
        keybd_event(38, 0, 0, 0); // 按下上箭头键
    }
    else if (data == "B")
    {
        keybd_event(40, 0, 0, 0); // 按下下箭头键
    }
}

}
