using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParticleSystems : MonoBehaviour
{
    public ParticleSystem particleSystemMove;

    public ParticleSystem particleSystemBurst;

    public AudioSource explosionAudiosource;

    public AudioSource resistenceAudiosource;

    public Vector3 startPos;
    public Vector3 endPos;

    private bool isMoving;

    private bool canBurst;

    public float moveSpeed;

    #region Start Kommentare
    //Setzt die Anfangsposition des Burst-Partikelsystems auf die Endposition damit ich es nicht manuell machen muss.
    //Setzt die Startposition des Move-Partikelsystems.
    //Setzt den bool zur Erlaubnis des Startens des Burst-Partikelsystems auf true.
    #endregion
    private void Start()
    {
        particleSystemBurst.transform.position = endPos;
        startPos = particleSystemMove.transform.position;
        canBurst = true;
    }

    #region Update Kommentare
    //Wenn das Partikelsystem in Bewegung ist (der isMoving-Bool ist trur), wird die Funktion MoveParticleSystem() jeden Frame aufgerufen.
    #endregion
    void Update()
    {
        if (isMoving)
            MoveParticleSystem();
    }

    #region StartParticleSystems Kommentare
    //Startet die Bewegung des Partikelsystems, wenn das Burst-Partikelsystem gestartet werden kann.
    //Spielt den resistence-Sound ab und setzt den isMoving-Bool auf true.
    #endregion
    public void StartParticleSystems(Button button)
    {
        if (!canBurst)
            return;

        particleSystemMove.Play();
        resistenceAudiosource.Play();
        button.interactable = false;
        isMoving = true;
        canBurst = false;
    }

    #region MoveParticleSystem Kommentare
    //Bewegt das Partikelsystem von der Startposition zur Endposition.
    //Wenn die Endposition erreicht wurde, wird OnBeamReachedPosition() aufgerufen.
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

    #region OnBeamReachedPosition Kommentare
    //Wird aufgerufen, wenn das Partikelsystem die Endposition erreicht hat.
    //Stoppt das Move-Partikelsystem, startet das Burst-Partikelsystem und spielt den Explosions-Sound ab.
    //Ruft die Funktion DestroyParticleSystems() auf, um die Partikelsysteme zu zerstören.
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

    #region DestroyParticleSystems Kommentare
    //Zerstört die Partikelsysteme, damit sie nicht mehr sichtbar sind/Simuliert werden.
    #endregion
    void DestroyParticleSystems()
    {
        Destroy(particleSystemMove);
        Destroy(particleSystemBurst);
    }
}
