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

    public Vector3 startPos;
    public Vector3 endPos;

    private bool isMoving;
    private bool canBurst;

    public float moveSpeed;

    private void Start()
    {
        particleSystemBurst.transform.position = endPos;
        startPos = particleSystemMove.transform.position;
        canBurst = true;
    }

    #region Kommentare
    //Ruft die Funktion HandleInput() auf um die Eingaben des Spielers zu überprüfen
    //Wenn das Partikelsystem sich bewegt (der isMoving bool wahr ist), wird die Funktion MoveParticleSystem() aufgerufen
    #endregion
    void Update()
    {
        HandleInput();

        if (isMoving)
            MoveParticleSystem();
    }

    #region Kommentare
    //Wenn das Partikelsystem zerstört wurde wird die Funktion beendet und der Spieler kann die Partikelsysteme nicht mehr starten
    //Überprüft ob der Spieler die Taste "Space" gedrückt hat um die Partikelsysteme zu starten
    #endregion
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canBurst)
            StartParticleSystems();
    }

    #region Kommentare
    //Verändert die Farbe des Tutorial Textes zu einem dunkleren Grau um den Spieler darauf hinzuweisen das er nun nicht mehr interagieren kann
    //Startet die Beam-Partikel
    //Spielt den Sound Resistence Sound ab
    #endregion
    void StartParticleSystems()
    {
        tutorialParticleSimText.color = new Color(.3f, .3f, .3f);
        particleSystemMove.Play();
        resistenceAudiosource.Play();
        isMoving = true;
        canBurst = false;
    }

    #region Kommentare
    //Bewegt das Partikelsystem von der Startposition zur Endposition
    //Wenn die Endposition erreicht wurde wird OnBeamReachedPosition() aufgerufen
    #endregion
    void MoveParticleSystem()
    {
        Transform moveTransform = particleSystemMove.transform;
        moveTransform.position = Vector3.MoveTowards(moveTransform.position, endPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(moveTransform.position, endPos) < 0.01f)
        {
            OnBeamReachedPosition();
        }
    }

    #region Kommentare
    //Wird aufgerufen wenn das Partikelsystem die Endposition erreicht hat
    //Stoppt die Beam-Partikel und startet die Burst-Partikel und spielt den Sound Explosions Sound ab
    //Ruft die Funktion DestroyParticleSystems() auf um die Partikelsysteme zu zerstören
    #endregion
    private void OnBeamReachedPosition()
    {
        particleSystemMove.Stop();
        resistenceAudiosource.Stop();
        particleSystemBurst.Play();
        explosionAudiosource.Play();
        isMoving = false;
        DestroyParticleSystems();
    }

    #region Kommentare
    //Zerstört die Partikelsysteme damit sie nicht mehr sichtbar sind
    #endregion
    void DestroyParticleSystems()
    {
        Destroy(particleSystemMove);
        Destroy(particleSystemBurst);
    }
}
