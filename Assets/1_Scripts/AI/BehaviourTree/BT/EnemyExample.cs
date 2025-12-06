using AI.BTTest;
using UnityEngine;

public class EnemyExample : MonoBehaviour
{
    public Transform target;

    FieldOfView fov;
    Seek seek;
    PatrolWp patrol;
    HealthPack hpack;

    [SerializeField] int life = 100;

    Node root;

    void Start()
    {
        fov = GetComponent<FieldOfView>();
        seek = GetComponent<Seek>();
        patrol = GetComponent<PatrolWp>();
        hpack = GetComponent<HealthPack>();


        root = new Selector
            (
                new Sequencer
                (
                    new Inverter(new Leaf(_toDo: HasLifeNode)),
                    new Leaf(_toDo: WaitNode
                    , _onReset: () => timer = 0),
                    new RepeatUntilFail(new Leaf(_toDo: Heal))
                )
                ,
                new Sequencer
                (
                    new Leaf(_toDo: CanSeePlayerNode),
                    new Leaf(_toDo: WaitNode, _onReset: () => timer = 0),
                    new Leaf(_toDo: FollowPlayer)
                ),
                new Leaf(_toDo: PatrolNode)

            );

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            life -= Random.Range(18,23);
        }

        root.Evaluate();
    }


    bool CanSeePlayer()
    {
        return fov.FOV(target);
    }


    float timerHeal = 0;
    bool cdHeal = false;
    public Status Heal()
    {
        if (cdHeal)
        {
            timerHeal += Time.deltaTime;
            if (timerHeal < 0.5f)
            {
                return Status.running;
            }
            else
            {
                timerHeal = 0;
                cdHeal = false;
            }
        }
        if (life >= 100)
        {
            cdHeal = false;
            return Status.failure;
        }

        life += hpack.HealAction();
        cdHeal = true;
        if (life >= 100)
        {
            life = 100;
        }

        return Status.success;
    }

    Status HasLifeNode()
    {
        return life > 50 ? Status.success : Status.failure;
    }

    bool HasLife()
    {
        return life > 50 ;
    }

    Status CanSeePlayerNode()
    {
        return fov.FOV(target) ? Status.success : Status.failure;
    }

    Status FollowPlayer()
    {
        if (!HasLife()) return Status.failure;

        if (CanSeePlayer())
        {
            seek.DoSeek(target);
            return Status.running;
        }
        else
        {
            return Status.failure;
        }
    }

    Status PatrolNode()
    {
        if (!HasLife()) return Status.failure;

        if (CanSeePlayer())
        {
            return Status.failure;
        }
        else
        {
            Vector3 next = patrol.GetWp(transform.position);
            seek.DoSeek(next);
            return Status.running;
        }
    }



    float timer = 0;
    float timeToWait = 1;
    Status WaitNode()
    {
        timer += Time.deltaTime;

        if (timer < timeToWait)
        {
            return Status.running;
        }

        return Status.success;
    }
}
