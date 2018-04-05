using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

public class Unit : Ownable {
    public float maxHealth;
    public float Health;
    public float movementSpeed = 1f;

    [Space] public bool TimedDeath = false;
    [Space] public int lifeTime = 2;

    private bool move;
    private Transform target;

    private ParticleSystem _particleSystem;
    private TurnManager _turnManager;
    private Animator _animator;

    public bool Moved;

    [HideInInspector] public Turret turret;

    public void Start() {
        if (GetComponent<Turret>() != null)
            turret = GetComponent<Turret>();

        _particleSystem = GameObject.Find("GameManager").GetComponent<ParticleSystem>();
        _turnManager = GameObject.Find("GameManager").GetComponent<TurnManager>();
        _animator = GetComponentInChildren<Animator>();

        if (owner == null)
            owner = GameObject.Find("Civ").GetComponent<Player>();

        FixTeam();
        _turnManager.StartTurn.AddListener(TurnPreparation);

        if (_turnManager.Players.FirstOrDefault(p => p.name == owner.name) == null) {
            TimedDeath = true;
            Debug.Log("AI Car Spawned");
        }
    }

    private void TurnPreparation() {
        if (TimedDeath)
            if (lifeTime-- == 0) {
                Leave();
                return;
            }

        Moved = false;
        if (turret != null)
            turret.Fired = false;
    }


    private void Leave() {
        _animator.SetTrigger("Leave");
        Destroy(gameObject, 2f);
    }
    
    public void TakeDamage(float damage) {
        if (Health > damage) {
            Health -= damage;
        }
        else {
            Die();
        }
    }

    private void Die() {
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.position = gameObject.transform.position;
        emitParams.startSize = 15;
        _particleSystem.Emit(emitParams, 50);
        //TODO: Maybe a fade out effect
        _animator.SetTrigger("Crash");
        Destroy(gameObject, 5f);
    }

    public void Move(Transform target) {
        Moved = true;
        transform.parent = target.transform;
        StartCoroutine(MoveToPosition(transform.position, target.transform.position, movementSpeed));
    }

    IEnumerator MoveToPosition(Vector3 startPosition, Vector3 targetPosition, float time) {
        float startTime = Time.time;
        _animator.SetBool("moving", true);
        while (Time.time < startTime + time) {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (Time.time - startTime) / time);
            yield return null;
        }

        transform.position = targetPosition;
        _animator.SetBool("moving", false);
    }

    private void FixTeam() {
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>()) {
            List<Material> mats = renderer.materials.ToList();
            if (mats.FindIndex(m => m.name == "Car Color (Instance)") != -1) {
                int i = mats.FindIndex(m => m.name == "Car Color (Instance)");
                mats[i] = owner.GetComponent<MatList>().RandomMaterial();
            }

            renderer.materials = mats.ToArray();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.GetComponentInParent<Unit>()) {
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
            emitParams.position = other.contacts.First().point;
            emitParams.startSize = .5f;
            _particleSystem.Emit(emitParams, 50);
        }
    }
}