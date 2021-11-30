using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caught : MonoBehaviour
{
    private AudioSource source;

    void Awake(){
        source = GetComponent<AudioSource>();
    }
    public void Catch(Collider2D col) {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        source.Play();
        sprite.enabled = false;
        StartCoroutine(turnOffBug(col));
        Debug.Log("Sound has Played");
        
        
    }

    IEnumerator turnOffBug(Collider2D col){
        yield return new WaitForSeconds(source.clip.length);
        col.gameObject.SetActive(false);
        
    }
}
