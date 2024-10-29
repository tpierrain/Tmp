namespace TrainTrain;

public interface IBookingReferenceService
{
    Task<string> GetBookingReference();
}