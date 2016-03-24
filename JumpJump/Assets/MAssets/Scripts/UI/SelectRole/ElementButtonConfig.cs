using UnityEngine;
using System.Collections;
using UI.UIComponent.ScrollList;
using System;
using System.Collections.Generic;

public enum ButtonConfigType
{
    NODefine,
    BuyCoins,
    BuySpecialCoins,
    BuyLockMachine
}

public class ElementButtonConfig : SGElementConfig
{

    public static bool IsShowSpecialDialog = false;

    [Serializable]
    public  class ConfigInf
    {
        public Sprite sprite;
        public ButtonConfigType buttonConfigType;
        public string desc ;
        public bool IsSpecial = false;
        
        public ConfigInf (Sprite sprite, ButtonConfigType buttonConfigType)
        {
            this.sprite = sprite;
            this.buttonConfigType = buttonConfigType;
        }
        
        public ConfigInf (Sprite sprite, String buttonConfigType)
        {
            this.sprite = sprite;
            this.buttonConfigType = (ButtonConfigType)Enum.Parse (typeof(ButtonConfigType), buttonConfigType);
        }
    }
    
    public ConfigInf[] ConfigInfs;
    
    public override int GetCount ()
    { 
        if (ConfigInfs != null)
            return ConfigInfs.Length;
        return 0;
    }
    
    public void ChoiseEvent (ButtonConfigType buttonConfigType, ElementButton elementButton)
    {
      
    }
    
   
        

}
