using System;
using System.Collections.Generic;
using System.Linq;
using AI.BTTest;
/*
public class Inverter : Node
{
    public Inverter(Node child) : base(child) { }

    public override Status Evaluate()
    {
        var status = childs[0].Evaluate();

        if (status == Status.sucess)
            return Status.failure;

        if (status == Status.failure)
            return Status.sucess;

        return Status.running;
    }
}

public class Repeater : Node
{
    int repeatCount;
    int currentCount;

    public Repeater(Node child, int times = -1) : base(child)
    {
        repeatCount = times; // -1 = infinito
        currentCount = 0;
    }

    public override Status Evaluate()
    {
        var status = childs[0].Evaluate();

        // si sigue corriendo, no hay que resetear
        if (status == Status.running)
            return Status.running;

        // si terminó (success o fail): reseteo y repito
        childs[0].Reset();
        currentCount++;

        if (repeatCount < 0 || currentCount < repeatCount)
        {
            return Status.running;
        }

        // terminó todas las repeticiones → success
        Reset();
        return Status.sucess;
    }

    public override void Reset()
    {
        currentCount = 0;
        base.Reset();
    }
}

public class RepeatUntilFail : Node
{
    public RepeatUntilFail(Node child) : base(child) { }

    public override Status Evaluate()
    {
        var status = childs[0].Evaluate();

        if (status == Status.failure)
        {
            // cuando falle → success del nodo
            childs[0].Reset();
            return Status.sucess;
        }

        if (status == Status.sucess)
        {
            childs[0].Reset();
            return Status.running; // seguir repitiendo
        }

        return Status.running; // si está corriendo, sigo
    }
}

public class RandomSelector : Node
{
    int current = 0;
    List<Node> randomized;

    public RandomSelector(params Node[] _childs) : base(_childs)
    {
        randomized = new List<Node>(childs);
        Shuffle();
    }

    void Shuffle()
    {
        randomized = randomized.OrderBy(a => Random.value).ToList();
    }

    public override Status Evaluate()
    {
        while (current < randomized.Count)
        {
            var status = randomized[current].Evaluate();

            if (status == Status.sucess)
            {
                Reset();
                return Status.sucess;
            }

            if (status == Status.running)
                return Status.running;

            if (status == Status.failure)
            {
                randomized[current].Reset();
                current++;
            }
        }

        Reset();
        return Status.failure;
    }

    public override void Reset()
    {
        current = 0;
        Shuffle();
        base.Reset();
    }
}
*/