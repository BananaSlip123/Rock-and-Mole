using UnityEngine;

public class PlantController : MonoBehaviour, IStateMachineComponent
{
    [SerializeField] IStateComponent actualState;
    [SerializeField] IStateComponent lastState;
    [SerializeField] GameObject explosion;
    [SerializeField] Animator animator;

    void Awake()
    {
        actualState = new PlantLookingState(this, transform, animator);
        actualState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        MUpdate();
    }

    void FixedUpdate()
    {
        MFixedUpdate();
    }

    public void MUpdate()
    {
        actualState.Update();
    }

    public void MFixedUpdate()
    {
        actualState.FixedUpdate();
    }

    public void ChangeState(IStateComponent newState)
    {
        lastState = actualState;
        actualState = newState;

        lastState.Exit();
        actualState.Enter();
    }

    public void GenerateGameObject(Transform enemyTransform)
    {
        Instantiate(explosion, enemyTransform.position, enemyTransform.rotation).SetActive(true);
    }

    public void DestroyGameObject(GameObject d)
    {
        Destroy(d);
    }
}
