using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
		float h = 1f;
		float v = 1f;
		bool start = true;
		bool cpuCollided = false;
		bool enventoryOn=false;
		public GameObject E;
		public GameObject craftSystem;
		private GameObject tooltip;
		private AudioSource source;
		public AudioClip menu;
		private float volLowRange = 1.2f;
		private float volHighRange = 2.0f;

		void Awake() {
			source = GetComponent<AudioSource> ();
		}

		void toggleInventory(){
			if (E.GetComponent<Canvas>().isActiveAndEnabled && !Input.GetMouseButton(0)) {
				if (GameObject.Find ("Description")!=null) {
					tooltip = GameObject.Find ("Description");
					tooltip.SetActive (false);
				}
				enventoryOn = false;
				E.GetComponent<Canvas>().enabled = false;
			}
			else{
				E.GetComponent<Canvas>().enabled = true;
				enventoryOn = true;
			}
		}
		void toggleCraft(){
			if (cpuCollided) {

				if (craftSystem.activeSelf) {
					craftSystem.SetActive (false);
				} else {
					craftSystem.SetActive (true);
				}

			}

		}
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();

        }


        private void Update()
		{
			if (start) {
				E.GetComponent<Canvas> ().enabled = false;
				craftSystem.SetActive (false);
				start = false;
			}
			if(Input.GetKeyUp(KeyCode.I)){
				float vol = UnityEngine.Random.Range(volLowRange, volHighRange);
				source.PlayOneShot (menu,vol);
				toggleInventory ();
				toggleCraft ();
			}
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            
			if (enventoryOn) {
				h = h - .3f;
				if (v < 0) {
					v = v + .3f;
					if (v > 0) {
						v = 0;
					}
				} else {
					if (v > 0) {
						v = v - .3f;
						if (v < 0) {
							v = 0;
						}
					}
				}

				if (h < 0) {
					h = 0;
				}

			} else {
				h = CrossPlatformInputManager.GetAxis ("Horizontal");
				v = CrossPlatformInputManager.GetAxis ("Vertical");
			}
			v=v + Math.Abs (h / 2);
			if (v > 1f) {
				v = 1f;
			}
			m_Move = new Vector3 (h/2, 0,v);
	
			m_Character.Move (m_Move, m_Jump);
        }
		void OnTriggerExit(Collider col){
			if (col.gameObject.tag == "CPU") {
				cpuCollided = false;
			}
		}
		void OnTriggerEnter(Collider col){
			if (col.gameObject.tag == "CPU") {
				cpuCollided = true;
			}
		}
    }
}
