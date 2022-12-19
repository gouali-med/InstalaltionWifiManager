namespace FBCOMSystemManagement.Models
{
    public class Telephone
    {
        public int ID { get; set; }
        public string? SN{ get; set; }
        public string? Installed { get; set; }
        public string? Description { get; set; }
        public DateTime? DateAjout { get; set; }
        public virtual List<Installation>? Installations { get; set; }
    }
}
