using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor4Ghost : MonoBehaviour
{
    public Transform[] moniters;
    //public GameObject ghost;

    public float time = 30f;

    public Sprite deathImage;

    public Material changeMonitor;

    public Material interactMointor;

    public GameObject shuter;

    public GameObject[] hands;

    public bool isEnable = false;

    public float turnOnTime = 3f;
    
    public List<MeshRenderer> renderers = new List<MeshRenderer>();

    public GameObject key;

    public GameObject endTrigger;

    public List<GameObject> schoolDoors = new List<GameObject>();

    public float curTime = 0;
    float noiseTime = 0;
    float beatTime = 2f;
    public void MonitorOn(Transform monitor)
    {
        MeshRenderer meshRenderer = monitor.GetComponent<MeshRenderer>();
        meshRenderer.material = changeMonitor;
        SoundManager.PlaySound("MonitorSound", 0, 1, monitor.position);
        Instantiate(hands[Random.Range(0, hands.Length)], monitor.transform.parent);

        StartCoroutine(Timer());
        shuter.SetActive(true);
        isEnable = true;
        SoundManager.PlaySound("metalDoor", 0, 1, shuter.transform.position);
       
    }

    public void MonitorOff(Transform monitor)
    {
        MeshRenderer meshRenderer = monitor.GetComponent<MeshRenderer>();
        meshRenderer.material = interactMointor;
        SoundManager.PlaySound("MonitorSound", 0, 1, monitor.position);
        renderers.Add(meshRenderer);
        InteractMonitor interM = meshRenderer.transform.GetComponent<InteractMonitor>();
        monitor.parent.transform.GetChild(4).gameObject.SetActive(false);
        interM.enabled = false;


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
        for(int i = 0; i < moniters.Length; i++)
        {
            renderers.Add(moniters[i].GetChild(3).GetComponent<MeshRenderer>());
        }
        


        
    }


    IEnumerator Timer()
    {
       
        yield return new WaitForSeconds(90f);
        key.SetActive(true);
        
        shuter.SetActive(false);
        endTrigger.SetActive(true);
        foreach(var door in schoolDoors) door.SetActive(false);
        isEnable = false;
        SoundManager.PlaySound("metalDoor", 1, 1, shuter.transform.position);
        for(int i = 0; i < moniters.Length; i++)
        {
            MeshRenderer meshRenderer = moniters[i].transform.GetChild(3).GetComponent<MeshRenderer>();
            meshRenderer.material = interactMointor;
            if(moniters[i].transform.childCount > 4)
            {
                moniters[i].transform.GetChild(4).gameObject.SetActive(false);
            }
            InteractMonitor interactMonitor = meshRenderer.transform.gameObject.GetComponent<InteractMonitor>();
            if(interactMonitor != null)
            {

                interactMonitor.isEnable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnable)
        {
            
            
           
            int count = 0;
            
                
            
           
            curTime += Time.deltaTime;
            noiseTime += Time.deltaTime;
            beatTime += Time.deltaTime;
            if(beatTime >= 2.25f)
            {
                beatTime = 0;
                SoundManager.PlaySound("HeartBeats", 0, 0.6f, Player.player.transform.position);
            }

            ComputerOn();
            Noise();
        }
    }

    IEnumerator delay()
    {
        SoundManager.PlaySound("Noise", 0, 1, Player.player.transform.position);
        yield return new WaitForSeconds(1f);
        DeathUI.Death(deathImage);
        isEnable = false;
    }

    void ComputerOn()
    {
        if(curTime >= turnOnTime) { 
        
            curTime = 0;
            int random = Random.Range(0, renderers.Count);
            if(renderers.Count == 1)
                {
                
                    StartCoroutine(delay());
                }
            renderers[random].material = changeMonitor;
            InteractMonitor interM = renderers[random].gameObject.AddComponent<InteractMonitor>();
            interM.one = true;
            interM.isEnable=true;
            BoxCollider collider = renderers[random].transform.gameObject.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.1f, collider.size.y, collider.size.z);
            interM.floor4Ghost = this;
            if(renderers[random].transform.parent.transform.childCount < 5)
            {
                Instantiate(hands[Random.Range(0, hands.Length)], renderers[random].transform.parent);
            } else
            {
                renderers[random].transform.parent.GetChild(4).gameObject.SetActive(true);  
            }
            renderers.RemoveAt(random);
            if (turnOnTime > 1.6f)
            {
                turnOnTime -= 0.1f;
            }
        }
        
    }

    void Noise()
    {
        if (noiseTime >= 6f)
        {

            
            
            if (renderers.Count < 16)
            {

                SoundManager.PlaySound("Noise", 0,  0.65f, Player.player.transform.position);
                noiseTime = 0;


            }
        }
    }
}
