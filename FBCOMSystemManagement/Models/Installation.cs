namespace FBCOMSystemManagement.Models
{
    public class Installation
    {
        public int ID { get; set; } 
        public string? ClientName { get; set; }
        public string? ClientAdress { get; set; }
        public string? ContactName { get; set; }
        public string? ContactPhone { get; set; }
        public string? Ville { get; set; }
        public string? TypeCient { get; set; }
        public string? ICCID { get; set; }
        public string? NGSM { get; set; }
        public string? CompteSIP { get; set; }
        public string? IdConnexion { get; set; }
        public string? Offre { get; set; }
        public string? Distributeur { get; set; }

        public string? GPSLatitude { get; set; }
        public string? GPSLongitude { get; set; }
        public DateTime? DATEIntervention { get; set; }
        public string? Etage { get; set; }
        public string? Emplacement { get; set; } /*(FACADE/TOIT)*/
        public string? Immeublefibre { get; set; }/*(OUI/NON)*/
        public string? TelephoneBluetooth  { get; set; }/*(OUI/NON)*/
        public string? IMSI { get; set; }
        public string? Speed1 { get; set; }
        public string? Speed2 { get; set; }
        public string? Speed3 { get; set; }

        public string? eNodeBID { get; set; }
        public string? CELLband { get; set; }
        public string? RSRP { get; set; }
        public string? SINR { get; set; }
        public string? BandAuto { get; set; }
        public string? PRB_BH { get; set; }
        public string? PRB_NBH { get; set; }

        public string? StatueScreen { get; set; }
        public string? SNRScreen { get; set; }
        public string? SpeedScreen { get; set; }
        public string? LTEScreen { get; set; }
        public string? VoipScreen { get; set; }
        public string? PVPicture { get; set; }
        public string? CarteCINRecto { get; set; }
        public string? CarteCINVerso { get; set; }
        public string? PictureInside { get; set; }
        public string? PictureOutside { get; set; }
        public string? PictureOutside2 { get; set; }

        public string? SNRNegative { get; set; }
        public string? Installed { get; set; }
        public string? Description { get; set; }
        public int? CPEID { get; set; }
        public int? TelephoneID { get; set; }
        public virtual CPE? CPE { get; set; }
        public virtual Telephone? Telephone { get; set; }
    }
}
