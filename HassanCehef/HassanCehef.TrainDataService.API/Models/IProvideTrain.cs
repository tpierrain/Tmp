namespace HassanCehef.TrainDataService.Models
{
    public interface IProvideTrain
    {
        Train GetTrain(string trainId);
        void UpdateTrainReservations(TrainUpdateDTO trainUpdateDto);
    }
}