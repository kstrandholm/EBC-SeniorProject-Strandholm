using System;
using System.Collections.Generic;
using DubuqueCodeCamp.DatabaseConnection;
using System.Linq;

namespace DubuqueCodeCamp.Registration
{
    public class DatabaseOperations
    {
        private static readonly DCCKellyDatabase DATABASE = new DCCKellyDatabase();

        // TODO: potentially get rid of this method
        public static List<Talk> GetTalks()
        {
            // TODO: remove null check once database nullability stuff is taken care of
            return DATABASE.Talks.Where(talk => talk.DateGiven == DateTime.Today || talk.DateGiven == null).ToList();
        }

        public static List<ChosenTalk> GetChosenTalks()
        {
            const bool NOT_CHOSEN = false;

            // TODO: remove null check once database nullability stuff is taken care of
            return DATABASE.Talks.Where(talk => talk.DateGiven == DateTime.Today || talk.DateGiven == null)
                            .Select(talk => new ChosenTalk
                            {
                                Talk = talk,
                                Chosen = NOT_CHOSEN
                            }).ToList();
        }

        public static void SaveRegistration(RegistrationInformation registration)
        {
            //TODO: implement the other items to save

            var registrant = new Registrant
            {
                FirstName = registration.FirstName,
            };
            DATABASE.Registrants.InsertOnSubmit(registrant);

            foreach (var talk in registration.ChosenTalks)
            {
                if (talk.Chosen)
                {
                    var talkInterest = new TalkInterest
                    {
                        InterestedRegistrantID = 7,
                        TalkID = 8,

                    };
                    DATABASE.TalkInterest.InsertOnSubmit(talkInterest);
                }
            }

            //_database.SubmitChanges();
        }
    }
}
