using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Speakers;

namespace ConferencePlanner.GraphQL
{
    public class AddSpeakerPayload : SpeakerPayloadBase
    {
        public AddSpeakerPayload(Speaker speaker) : base(speaker)
        {
        }

        public Speaker Speaker { get; }
    }
}