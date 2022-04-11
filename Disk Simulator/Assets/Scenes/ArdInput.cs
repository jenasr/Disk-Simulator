using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArdInput : MonoBehaviour
{
    
    bool has_Card = false;
    int card_Id;
    SerialPort sp = new SerialPort("COM3", 9600);
    // Start is called before the first frame update
    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (has_Card)
        {
            return;
        }

        if (sp.IsOpen)
        {
            try
            {
                string filename = sp.ReadLine();
                int id;
                if (int.TryParse(filename, out id))
                {
                    card_Id = id;
                    has_Card = true;
                }
                else
                {
                    print(filename);
                }
                
            }
            catch (System.Exception e)
            {
                
            }
        }
    }
    void OnDestroy()
    {
        print("Disconnected");
        sp.Close();
    }

    public bool HasCard()
    {
        return has_Card;
    }
    public int GetCardId()
    {
        has_Card = false;
        return card_Id;
    }


}
