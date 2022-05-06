using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Spawner : MonoBehaviour
{
		public GEditor editor;
		public GameObject GBlock;
    public void spawn_block(Type type){
    	GameObject spawned = Instantiate(GBlock);
    	Transform block_child = spawned.transform.GetChild(0);
    	block_child.gameObject.GetComponent<Block>().set_type(type);
			editor.set_block_as_new(spawned);
    }
}
