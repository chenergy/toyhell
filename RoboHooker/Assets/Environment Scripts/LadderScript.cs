using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour {
	
	void Start(){
	}
	
	void OnTriggerStay(Collider collider){
		GameObject player = collider.gameObject;
        PlayerCharacter pc = (PlayerCharacter)player.GetComponent<PlayerCharacter>();
        if (pc != null)
        {
            bool climbing = pc.Climbing;
            float P1_movement = Input.GetAxis("P1moveY");
            float P2_movement = Input.GetAxis("P2moveY");

            if (climbing)
            {
				player.transform.LookAt(player.transform.position + new Vector3(0.0f, 0.0f, 1.0f));
                if (Mathf.Abs(P1_movement) > 0)
                {
                    if (player.GetComponent<PlayerCharacter>().m_player == gamepad.one)
                    {
                        float sign = (P1_movement / Mathf.Abs(P1_movement));
                        player.GetComponent<CharacterController>().Move(new Vector3(0.0f, -0.1f * sign, 0.0f));
                    }
                }
                if (Mathf.Abs(P2_movement) > 0)
                {
                    if (player.GetComponent<PlayerCharacter>().m_player == gamepad.two)
                    {
                        float sign = (P2_movement / Mathf.Abs(P2_movement));
                        player.GetComponent<CharacterController>().Move(new Vector3(0.0f, -0.1f * sign, 0.0f));
                    }
                }
                /*	
                if ((Input.GetAxis("P1moveY") < 0) || (Input.GetAxis("P2moveY") < 0)){
                    player.GetComponent<CharacterController>().Move(new Vector3(0.0f, 0.1f, 0.0f));
                }
                else if ((Input.GetAxis("P1moveY") > 0) || (Input.GetAxis("P2moveY") > 0)){
                    player.GetComponent<CharacterController>().Move(new Vector3(0.0f, -0.1f, 0.0f));
                }
                */
            }
        }
	}
	
	void OnTriggerEnter(Collider collider){
		GameObject player = collider.gameObject;
		if (player.tag == "Player"){
			player.GetComponent<PlayerCharacter>().Climbing = true;
		}
	}
	
	void OnTriggerExit(Collider collider){
		GameObject player = collider.gameObject;
		if (player.tag == "Player"){
            player.GetComponent<PlayerCharacter>().Climbing = false;
		}
	}
}