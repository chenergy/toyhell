using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour {
	/*
	void Start(){
	}
	
	void OnTriggerStay(Collider collider){
		
		GameObject player = collider.gameObject;
        //PlayerCharacter pc = (PlayerCharacter)player.GetComponent<PlayerCharacter>();
		PlayerInput input = player.GetComponent<PlayerInput>();
        if (input)
        {
            //bool climbing = input.Climbing;
            //float P1_movement = Input.GetAxis("P1moveY");
            //float P2_movement = Input.GetAxis("P2moveY");

            if (input.Climbing)
            {
				//player.transform.LookAt(player.transform.position + new Vector3(0.0f, 0.0f, 1.0f));
			}
		}
				/*
                if (Mathf.Abs(P1_movement) > 0)
                {
                    //if (player.GetComponent<PlayerCharacter>().m_player == gamepad.one)
					if (player.GetComponent<PlayerInput>().playerNumber == gamepad.one)
                    {
                        float sign = (P1_movement / Mathf.Abs(P1_movement));
                        player.GetComponent<CharacterController>().Move(new Vector3(0.0f, -0.1f * sign, 0.0f));
                    }
                }
                if (Mathf.Abs(P2_movement) > 0)
                {
                    //if (player.GetComponent<PlayerCharacter>().m_player == gamepad.two)
					if (player.GetComponent<PlayerInput>().playerNumber == gamepad.two)
                    {
                        float sign = (P2_movement / Mathf.Abs(P2_movement));
                        player.GetComponent<CharacterController>().Move(new Vector3(0.0f, -0.1f * sign, 0.0f));
                    }
                }
            }
        }
	}*/
	
	void OnTriggerEnter(Collider collider){
		GameObject player = collider.gameObject;
		if (player.tag == "Player"){
			//player.GetComponent<PlayerCharacter>().Climbing = true;
			player.GetComponent<PlayerInput>().Climbing = true;
		}
	}
	
	void OnTriggerExit(Collider collider){
		GameObject player = collider.gameObject;
		if (player.tag == "Player"){
            //player.GetComponent<PlayerCharacter>().Climbing = false;
			player.GetComponent<PlayerInput>().Climbing = false;
		}
	}
}