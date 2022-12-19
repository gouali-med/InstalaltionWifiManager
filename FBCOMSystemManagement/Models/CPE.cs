namespace FBCOMSystemManagement.Models
{
    public class CPE
    {
        public int ID { get; set; }
        public string? SNAntenne { get; set; }
        public string? SNRouteur { get; set; }
        public string? IMEI { get; set; }
        public string? Description { get; set; }
        public DateTime? DateAjout { get; set; }  
        public string? Installed { get; set; }  
       
        public virtual List<Installation>? Installations { get; set; }
    }
}
