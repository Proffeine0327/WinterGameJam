using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor4Ghost : MonoBehaviour
{
    public Transform[] moniters;
    public GameObject ghost;

    public float time = 30f;

    public Material changeMonitor;

    public Material interactMointor;

    public bool isEnable = false;
    

    public void MonitorOn(Transform monitor)
    {
        MeshRenderer meshRenderer = monitor.GetComponent<MeshRenderer>();
        meshRenderer.material = changeMonitor;
        StartCoroutine(ComputerOn());
        
    }



    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = moniters[Random.Range(0, moniters.Length)].GetChild(3).GetComponent<MeshRenderer>();
        meshRenderer.material = interactMointor;
        InteractMonitor interM = meshRenderer.transform.gameObject.AddComponent<InteractMonitor>();
        BoxCollider collider = meshRenderer.transform.gameObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(0.1f, collider.size.y, collider.size.z);
        interM.floor4Ghost = this;


        
    }


    IEnumerator ComputerOn()
    {
        List<MeshRenderer> renderers = new List<MeshRenderer>();
        for(int i = 0; i < moniters.Length; i++)
        {
            renderers.Add(moniters[i].GetChild(3).GetComponent<MeshRenderer>());
        }
        int random = Random.Range(0, renderers.Count);
        renderers[random].material = changeMonitor;
        renderers.RemoveAt(random);
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < 4; i++)
        {
            random = Random.Range(0, renderers.Count);
            renderers[random].material = changeMonitor;
            renderers.RemoveAt(random);
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 10; i++)
        {
            random = Random.Range(0, renderers.Count);
            renderers[random].material = changeMonitor;
            renderers.RemoveAt(random);
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 15; i++)
        {
            random = Random.Range(0, renderers.Count);
            
            renderers[random].material = changeMonitor;
            renderers.RemoveAt(random);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(3f);
        GameObject[] ghosts = new GameObject[4];
        for(int i = 0; i < 4; i++)
        {
            ghosts[i] = Instantiate(ghost, moniters[Random.Range(0, moniters.Length)].position, Quaternion.identity);
            yield return new WaitForSeconds(30f);
        }
        yield return new WaitForSeconds(180f);
        foreach(GameObject g in ghosts)
        {
            g.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
