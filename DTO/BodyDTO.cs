namespace DTO
{
    public class BodyDTO
    {
        public int? BodyID { get; set; }
        public string BodyName { get; set; }

        public BodyDTO(int? bodyID, string bodyName)
        {
            BodyID = bodyID;
            BodyName = bodyName;
        }
    }
}
