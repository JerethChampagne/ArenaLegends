using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour 
{
    public List<Material> material;
    public GameObject pSys;
    public Shader[] shader;

    public MeshRenderer render;
    public Shader shade;

	// Use this for initialization
	void Start () 
    {
        render = GetComponent<MeshRenderer>();
        //shade = render.material.shader;
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            //Shader shade = gameObject.GetComponent<Shader>();
            render.material.shader = shader[1];
            
        }

        if (Input.GetKeyUp(KeyCode.Q)) 
        {
            render.material.shader = shader[0];
        }
		
	}

    public void PlayParticle() 
    {
        pSys = Instantiate(pSys, this.transform.position, this.transform.rotation);
        pSys.GetComponent<ParticleSystem>().Play();
    }
}
