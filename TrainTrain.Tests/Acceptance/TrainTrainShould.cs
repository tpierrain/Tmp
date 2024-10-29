using Diverse;
using NFluent;
using NSubstitute;

namespace TrainTrain.Tests.Acceptance;

public class TrainTrainShould
{
    [Test]
    public async Task Reserve_seats_when_they_are_not_already_reserved()
    {
        var fuzzer = new Fuzzer();
        var (trainId, bookingReference) = FuzzTrainIdAndBookingReference(fuzzer);
        const int seatsRequestedCount = 3;

        var trainDataService = BuildTrainDataService(trainId, TrainTopology.With_10_available_seats());
        var bookingReferenceService = BuildBookingReferenceService(bookingReference);

        var webTicketManager = new WebTicketManager(trainDataService, bookingReferenceService);
        var reservation = await webTicketManager.Reserve(trainId, seatsRequestedCount);

        Check.That(reservation)
            .IsEqualTo($"{{\"train_id\": \"{trainId}\", \"booking_reference\": \"{bookingReference}\", \"seats\": [\"1A\", \"2A\", \"3A\"]}}");
    }

    [Test]
    public async Task Not_reserve_seats_when_it_exceed_max_capacity_threshold_of_70_percents()
    {
        var fuzzer = new Fuzzer();
        var (trainId, bookingReference) = FuzzTrainIdAndBookingReference(fuzzer);
        const int seatsRequestedCount = 3;

        var trainDataService = BuildTrainDataService(trainId, TrainTopology.With_10_seats_and_6_already_reserved());
        var bookingReferenceService = BuildBookingReferenceService(bookingReference);

        var webTicketManager = new WebTicketManager(trainDataService, bookingReferenceService);
        var reservation = await webTicketManager.Reserve(trainId, seatsRequestedCount);

        Check.That(reservation)
            .IsEqualTo($"{{\"train_id\": \"{trainId}\", \"booking_reference\": \"\", \"seats\": []}}");
    }

    [Test]
    public async Task Reserve_all_seats_in_the_same_coach()
    {
        var fuzzer = new Fuzzer();
        var (trainId, bookingReference) = FuzzTrainIdAndBookingReference(fuzzer);
        const int seatsRequestedCount = 2;

        var trainDataService = BuildTrainDataService(trainId, TrainTopology.With_2_coaches_and_9_seats_already_reserved_in_the_first_coach());
        var bookingReferenceService = BuildBookingReferenceService(bookingReference);

        var webTicketManager = new WebTicketManager(trainDataService, bookingReferenceService);
        var reservation = await webTicketManager.Reserve(trainId, seatsRequestedCount);

        Check.That(reservation).IsEqualTo($"{{\"train_id\": \"{trainId}\", \"booking_reference\": \"{bookingReference}\", \"seats\": [\"1B\", \"2B\"]}}");
    }


    #region Helpers

    private static (string trainId, string bookingReference) FuzzTrainIdAndBookingReference(IFuzz fuzzer)
    {
        var trainId = fuzzer.GenerateTrainId();
        var bookingReference = fuzzer.GenerateBookingReference();
        return (trainId, bookingReference);
    }

    private static IBookingReferenceService BuildBookingReferenceService(string bookingReference)
    {
        var bookingReferenceService = Substitute.For<IBookingReferenceService>();
        bookingReferenceService.GetBookingReference().Returns(bookingReference);
        return bookingReferenceService;
    }

    private static ITrainDataService BuildTrainDataService(string trainId, string trainTopology)
    {
        var trainDataService = Substitute.For<ITrainDataService>();
        trainDataService.GetTrain(trainId).Returns(trainTopology);
        return trainDataService;
    }

    #endregion
}