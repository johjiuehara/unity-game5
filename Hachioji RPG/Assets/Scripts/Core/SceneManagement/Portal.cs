using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {

        enum PortalDestinationID
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToBeLoaded = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] PortalDestinationID portalID;
        [SerializeField] float FadeOutTimeout = 3f;
        [SerializeField] float FadeInTimeout = 0.5f;
        [SerializeField] float WaitTimeout = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(FadeOutTimeout);
            yield return SceneManager.LoadSceneAsync(sceneToBeLoaded);
            Portal otherPortal = GetOtherPortal();
            updatePlayer(otherPortal);
            yield return new WaitForSeconds(WaitTimeout);
            yield return fader.FadeIn(FadeInTimeout);
            Destroy(gameObject);
        }

        private void updatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.GetComponent<Transform>().rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.portalID != this.portalID) continue;
                return portal;
            }
            return null;
        }
    }
}
