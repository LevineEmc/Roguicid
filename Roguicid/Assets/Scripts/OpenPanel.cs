using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Panel = null;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void open()
    {
        Panel.SetActive(true);
    }
}
 