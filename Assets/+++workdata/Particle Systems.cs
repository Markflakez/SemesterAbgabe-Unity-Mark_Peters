using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ParticleSystems : MonoBehaviour
{
    public ParticleSystem particleSystemMove;
    public ParticleSystem particleSystemBurst;
    public AudioSource explosionAudiosource;
    public AudioSource resistenceAudiosource;
    public TextMeshProUGUI tutorialParticleSimText;
    public float moveSpeed;
    public Vector3 startPos;
    public Vector3 endPos;

    private bool isMoving;
    private bool isDestroyed;

    private void Start()
    {
        particleSystemBurst.transform.position = endPos;
        startPos = particleSystemMove.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            
        if (isDestroyed)
            return;


        if (Input.GetKeyDown(KeyCode.P))
            StartParticleSimulation();

        if (isMoving)
            MoveParticleSystem();
    }

    void StartParticleSimulation()
    {
        tutorialParticleSimText.color = new Color(.3f, .3f, .3f);
        resistenceAudiosource.Play();
        particleSystemMove.Stop();
        particleSystemMove.Play();
        particleSystemBurst.Stop();
        isMoving = true;
    }

    void MoveParticleSystem()
    {
        Transform moveTransform = particleSystemMove.transform;
        moveTransform.position = Vector3.MoveTowards(moveTransform.position, endPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(moveTransform.position, endPos) < 0.01f)
        {
            isMoving = false;
            particleSystemMove.Stop();
            resistenceAudiosource.Stop();
            particleSystemBurst.Play();
            explosionAudiosource.Play();
            DestroyParticleSistems();
        }
    }


    void DestroyParticleSistems()
    {
        Destroy(particleSystemMove);
        Destroy(particleSystemBurst);
        isDestroyed = true;
    }
}
