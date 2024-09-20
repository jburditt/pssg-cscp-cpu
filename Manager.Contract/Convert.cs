namespace Manager.Contract;

public class Convert
{
    public static Quarter ToQuarter(int quarter)
    {
        switch (quarter)
        {
            case 1: return Quarter._1StQuarter;
            case 2: return Quarter._2NdQuarter;
            case 3: return Quarter._3RdQuarter;
            case 4: return Quarter._4ThQuarter;
            default: throw new ArgumentOutOfRangeException(nameof(quarter), $"Value {quarter} is out of range.");
        }
    }
}
