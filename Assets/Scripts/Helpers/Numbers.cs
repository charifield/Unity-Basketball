using System;



public static class DRandom

{

    private static Random m_Rnd;

    private static int m_LastSeed;

    static DRandom()

    {

        var v = DateTime.Now.Ticks;

        m_LastSeed = (int)(v ^ (v >> 7) ^ (v >> 17));

        m_Rnd = new Random(m_LastSeed);

    }

    public static int seed

    {

        get { return m_LastSeed; }

        set { m_LastSeed = value; m_Rnd = new Random(m_LastSeed); }

    }

    public static double value

    {

        get { return m_Rnd.NextDouble(); }

    }

    public static double Range(double aMin, double aMax)

    {

        return aMin + m_Rnd.NextDouble() * (aMax - aMin);

    }

}