

namespace DTO
{
    public class MakeDTO
    {
        public int? MakeID { get; set; }
        public string Make { get; set; }

        public MakeDTO(int? makeID, string make)
        {
            MakeID = makeID;
            Make = make;
        }
    }
}
