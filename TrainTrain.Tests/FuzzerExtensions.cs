using Diverse;

namespace TrainTrain.Tests;

public static class FuzzerExtensions
{
    public static string GenerateTrainId(this IFuzz fuzzer)
    {
        return fuzzer.GenerateStringFromPattern("#xxXX-###");
    }
    
    public static string GenerateBookingReference(this IFuzz fuzzer)
    {
        return fuzzer.GenerateStringFromPattern("##xxx##");
    }
}