using System.Data.Entity;

namespace TrainTrain.Dal.Entities
{
    public class TrainTrainContext : DbContext
    {
        public TrainTrainContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }


        public DbSet<TrainEntity> Trains { get; set; }
        public DbSet<SeatEntity> Seats { get; set; }
    }
}