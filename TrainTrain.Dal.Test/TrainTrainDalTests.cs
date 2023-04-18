using System.Collections.Generic;
using NFluent;
using NUnit.Framework;
using TrainTrain.Dal.Entities;

namespace TrainTrain.Dal.Test
{
    public class TrainTrainDalTests
    {
        [SetUp]
        public void SetUp()
        {
            Factory.Create().RemoveAll();
        }

        [TearDown]
        public void TearDown()
        {
           Factory.Create().RemoveAll();
        }

        [Test]
        public void Should_add_a_new_train_when_this_one_dont_exist()
        {
            const string trainId = "express_2000";
            const int expectedNbSeat = 5;

            var train = MakeTrain(trainId, expectedNbSeat);

            Factory.Create().Save(train);
            var train2 = Factory.Create().Get(trainId);
            var nbSeat = train2.Seats.ToArray().Length;

            Check.That(nbSeat).IsEqualTo(expectedNbSeat);
        }

        [Test]
        public void Should_retrieve_all_trains()
        {
            var trains = new List<TrainEntity>
            {
                MakeTrain("express_2001", 1),
                MakeTrain("express_2002", 2),
                MakeTrain("express_2003", 3),
                MakeTrain("express_2004", 4),
                MakeTrain("express_2005", 5),
                MakeTrain("express_2006", 6),
                MakeTrain("express_2007", 7),
                MakeTrain("express_2008", 8),
                MakeTrain("express_2009", 9),
                MakeTrain("express_2010", 10),
            };

            var repository = Factory.Create();
            repository.SaveAll(trains.ToArray());

            Check.That(repository.GetAll().Count).IsEqualTo(trains.Count);
        }

        [Test]
        public void Should_remove_one_train()
        {
            const string trainId = "express_2000";

            var repository = Factory.Create();
            repository.Remove(trainId);
            Check.That(repository.Get(trainId)).IsNull();
        }

        private static TrainEntity MakeTrain(string trainId, int nbSeat)
        {
            var train = new TrainEntity {TrainId = trainId};
            for (var i = 0; i < nbSeat; i++)
            {
                train.Seats.Add(new SeatEntity {TrainId = trainId, CoachName = "A", SeatNumber = i});
            }
            return train;
        }
    }
}