using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Create : MonoBehaviour
{
    
   public GameObject On_Ui;
   public GameObject False_btn;

   public GameObject Info_input;
   public GameObject Btn;

   public void finish_info(){
    Info_input.SetActive(false);
    Btn.SetActive(true);
   }

    public void OnClick(){
        
        On_Ui.SetActive(true);
        False_btn.SetActive(false);
        this.gameObject.SetActive(false);
        
    }
}
