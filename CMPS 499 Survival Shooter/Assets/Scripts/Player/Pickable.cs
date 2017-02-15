using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShaderEvent : UnityEvent 
{

}

public class Pickable : MonoBehaviour 
{
    public List<Material> material;
    public GameObject pSys;
    public Shader[] shader;
    public MeshRenderer render;
    public Shader shade;

    ShaderEvent ChangeShaderEvent;

	// Use this for initialization
	void Start () 
    {
        if (ChangeShaderEvent == null) 
        {
            ChangeShaderEvent = new ShaderEvent();
        }

        ChangeShaderEvent.AddListener(ChangeShader);
        shade = shader[1];
        render = GetComponent<MeshRenderer>();
        //shade = render.material.shader;
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void ChangeShader() 
    {
        render.material.shader = shade;

        if (shade == shader[0]) 
        {
            shade = shader[1];
        }
        else { shade = shader[0]; }
    }

    void ChangeMaterial() 
    {

    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("I've entered OnTriggerEnter!");

        render.material = material[1];
        col.GetComponent<MeshRenderer>().material = material[1];
        
    }

    void OnTriggerExit(Collider col)
    {
        Debug.Log("I've entered OnTriggerExit!");

        render.material = material[0];
        col.GetComponent<MeshRenderer>().material = material[0];
    }
    
}
