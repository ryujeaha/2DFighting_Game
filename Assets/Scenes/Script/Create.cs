using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Create : MonoBehaviour
{
    
   public GameObject On_Ui;
   public GameObject False_btn;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick(){
        
        On_Ui.SetActive(true);
        False_btn.SetActive(false);
        this.gameObject.SetActive(false);
        
    }
}
