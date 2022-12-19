using FBCOMSystemManagement.Models;

namespace FBCOMSystemManagement.Helpers
{
    public class UploadFile
    {
        public IFormFile file { get; set; } 
        public Installation installation { get; set; } 
    }
}
