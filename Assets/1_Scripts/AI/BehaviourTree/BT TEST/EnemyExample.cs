
namespace AI.BTTest
{
    using UnityEngine;

    public class EnemyExample : MonoBehaviour
    {
        public Transform target;

        FieldOfView fov;
        Seek seek;
        PatrolWp patrol;
        HealthPack healthPack;

        [SerializeField] int life = 100;


        Node root;

        public void DoDamage()
        {
            life -= Random.Range(18,23);
        }

        void Start()
        {
            fov = GetComponent<FieldOfView>();
            seek = GetComponent<Seek>();
            patrol = GetComponent<PatrolWp>();
            healthPack = GetComponent<HealthPack>();

            root = new Selector
                (
                    new Sequence
                    (
                        new Leaf(_action: LowLifeNode),
                        new RepearUntilFail(new Leaf(_action: Heal))
                    )
                    ,
                    new Sequence
                    (
                        new Leaf(_action: CanSeePlayerNode),
                        new WaitNode(1f),
                        new Leaf(_action: FollowPlayerNode)
                    ),
                    new Leaf(PatrolNode)
                );
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                DoDamage();
            }

            root.Evaluate();

            
        }

        #region Acciones
        public bool CanSeePlayer()
        {
            return fov.FOV(target);
        }
        public void FollowPlayer()
        {
            seek.DoSeek(target);
        }

        public void Patrol()
        {
            Vector3 next = patrol.GetWp(transform.position);
            seek.DoSeek(next);
        }
        #endregion

        #region Acciones con Status
        public Status CanSeePlayerNode()
        {
            return fov.FOV(target) ? Status.sucess : Status.failure;
        }
        public Status FollowPlayerNode()
        {
            if (LowLife()) return Status.failure;

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

       
        public Status PatrolNode()
        {
            
            

            if (LowLife()) return Status.failure;

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
        public Status LowLifeNode()
        {
            return life < 50 ? Status.sucess : Status.failure;
        }
        public bool LowLife()
        {
            return life < 50;
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

            life += healthPack.HealAction();
            cdHeal = true;
            if (life >= 100)
            {

                life = 100;
            }
            
            return Status.sucess;
        }

        float timer = 0;
        public Status Wait(float toWait)
        {
            timer += Time.deltaTime;

            if (timer < toWait)
            {
                return Status.running;
            }
            return Status.sucess;
        }
        #endregion
    }

}
