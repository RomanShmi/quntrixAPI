using ConferenceDTO;
namespace quntrixAPI.Data;

public class Speaker : ConferenceDTO.Speaker
{
    public virtual ICollection<SessionSpeaker> SessionSpeakers { get; set; } = new List<SessionSpeaker>();
}
