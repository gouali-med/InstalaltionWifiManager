using FBCOMSystemManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace FBCOMSystemManagement.VIEWMODEL
{
    public class InstallationVM
    {

        public int ID { get; set; }
        public List<Telephone>? telephones { get; set; }
        public List<CPE>? cpes { get; set; }

        public int? CPEID { get; set; }
        public int? TelephoneID { get; set; }

        public string ClientName { get; set; }
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




        [Required]
        public string? GPSLatitude { get; set; }
        [Required]
        public string? GPSLongitude { get; set; }
    
        public DateTime? DATEIntervention { get; set; }

        public string? Etage { get; set; }
        public string? Emplacement { get; set; } /*(FACADE/TOIT)*/
        public string? Immeublefibre { get; set; }/*(OUI/NON)*/
        public string? TelephoneBluetooth { get; set; }/*(OUI/NON)*/
        [Required]
        public string? IMSI { get; set; }
        [Required]
        public string? Speed1 { get; set; }
        [Required]
        public string? Speed2 { get; set; }
        [Required]
        public string? Speed3 { get; set; }
        [Required]

        public string? eNodeBID { get; set; }
        [Required]
        public string? CELLband { get; set; }
        [Required]
        public string? RSRP { get; set; }
        [Required]
        public string? SINR { get; set; }
        [Required]
        public string? BandAuto { get; set; }
        [Required]
        public string? PRB_BH { get; set; }
        [Required]
        public string? PRB_NBH { get; set; }
        [Required]
        public IFormFile StatueScreen { get; set; }
        [Required]
        public IFormFile SNRScreen { get; set; }
        [Required]
        public IFormFile SpeedScreen { get; set; }
        [Required]
        public IFormFile LTEScreen { get; set; }
        [Required]
        public IFormFile VoipScreen { get; set; }
        [Required]
        public IFormFile PVPicture { get; set; }
        [Required]
        public IFormFile CarteCINRecto { get; set; }
        [Required]
        public IFormFile CarteCINVerso { get; set; }
        [Required]
        public IFormFile PictureInside { get; set; }
        [Required]
        public IFormFile PictureOutside { get; set; }
        [Required]
        public IFormFile PictureOutside2 { get; set; }




        public string StatueScreenURL { get; set; }
        public string SNRScreenURL { get; set; }
        public string SpeedScreenURL { get; set; }
        public string LTEScreenURL { get; set; }
        public string VoipScreenURL { get; set; }
        public string PVPictureURL { get; set; }
        public string CarteCINRectoURL { get; set; }
        public string CarteCINVersoURL { get; set; }
        public string PictureInsideURL { get; set; }
        public string PictureOutsideURL { get; set; }
        public string PictureOutside2URL { get; set; }


        public string? SNRNegative { get; set; }
        public string? Installed { get; set; }
        [Required]
        public string? Description { get; set; }



    }
}
