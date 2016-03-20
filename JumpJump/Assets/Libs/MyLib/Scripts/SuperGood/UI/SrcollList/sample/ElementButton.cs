using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UI.UIComponent.ScrollList;
using System;

public class ElementButton : SGElementBase
{

//	 //Defined in Configuration class
//    public static string SPECIAL_COINS_DELAYTIME = "SpecialCoinsDelaytime"; 
    private Text mytext;
    private Button b;
    private bool isSpecial = false;
    public static string SPECIAL_TAG = "Special";
    private static string SPECIAL_START_TIME = "SpecialStartTime";
    private long specialStartTime;
    private DateTime specialStartDateTime;

    public override bool Init (int index, SGScrollPanel mySkyScrollPanel)
    {
        base.Init (index, mySkyScrollPanel);
        ElementButtonConfig config = ((ElementButtonConfig)(MySkyScrollPanel.Config));
        gameObject.name = "ElementButton" + index;
        b = gameObject.transform.parent.Find (gameObject.name).GetComponent<Button> ();
        mytext = gameObject.transform.Find ("Text").GetComponent<Text> ();
        isSpecial = config.ConfigInfs [index].IsSpecial;
        if (((ElementButtonConfig)(MySkyScrollPanel.Config)).ConfigInfs [index].sprite == null)
            mytext.text = config.ConfigInfs [index].desc;
        else
            b.image.sprite = config.ConfigInfs [index].sprite;
		return true;
      
    }

   
    
    void Update ()
    {
       
    }
    
 

    void OnDestroy ()
    {
      
    }

  
}
