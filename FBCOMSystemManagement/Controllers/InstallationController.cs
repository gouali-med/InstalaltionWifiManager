using FBCOMSystemManagement.DATA;
using FBCOMSystemManagement.Models;
using FBCOMSystemManagement.VIEWMODEL;
using Microsoft.AspNetCore.Mvc;
using WareHouse.Models.Reposotories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Data;
using System.Reflection;
using System;
using OfficeOpenXml;
using Newtonsoft.Json;
using FBCOMSystemManagement.Helpers;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Syncfusion.XlsIO.Parser.Biff_Records;
using OfficeOpenXml.Style;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.Data.SqlClient;

namespace FBCOMSystemManagement.Controllers
{
    public class InstallationController : Controller
    {

        private readonly IHostingEnvironment hosting;
        private readonly IFBCOMDBContext<Installation> _installationRepository;
        private readonly DBContextFBCOM _context;

        public InstallationController(DBContextFBCOM context, IHostingEnvironment hosting, IFBCOMDBContext<Installation> installationRepository)
        {
            this.hosting = hosting;
            _installationRepository = installationRepository;
            _context = context;
           
        }

        

         [HttpGet]
        public ActionResult IndexNegative()
        {
            var installations = _context.Installations.Include(b => b.CPE).Include(c => c.Telephone).Where(m=>m.SNRNegative=="OUI").ToList();
            return View(installations);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var installations = _context.Installations.Include(b => b.CPE).Include(c => c.Telephone).Where(m => m.Installed == "OUI").ToList();
            return View(installations);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var installation = _installationRepository.Find(id);

            return View(installation);
        }
        [HttpGet]
        public ActionResult Create(int id)
        {
         
            var preInstallation = _installationRepository.Find(id);

            var viewModel = new InstallationVM
            {
                ID = id,
                ClientName = preInstallation.ClientName,
                ClientAdress = preInstallation.ClientAdress,
                ContactName = preInstallation.ContactName,
                ContactPhone = preInstallation.ContactPhone,
                Ville = preInstallation.Ville,
                TypeCient = preInstallation.TypeCient,
                ICCID = preInstallation.ICCID,
                NGSM = preInstallation.NGSM,
                CompteSIP = preInstallation.CompteSIP,
                IdConnexion = preInstallation.IdConnexion,
                Offre = preInstallation.Offre,
                Distributeur = preInstallation.Distributeur,
                telephones = FillSelectListTelephone(),
                cpes = FillSelectListCPE()
            };

            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, InstallationVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    if (model.CPEID == -1)
                    {
                        ViewBag.Message = "Please select an cpe from the list!";

                        return View(FillSelectListCPE());
                    }
                    if (model.TelephoneID == -1)
                    {
                        ViewBag.Message = "Please select an telephone from the list!";

                        return View(FillSelectListTelephone());
                    }

                    var cpe = _context.CPES.Find(model.CPEID);
                    var telephone = _context.Telephones.Find(model.TelephoneID);



                    var installation = _context.Installations.Find(model.ID);
                    if (installation != null)
                    {


                        installation.ClientName = model.ClientName;
                        installation.ClientAdress = model.ClientAdress;
                        installation.ContactName = model.ContactName;
                        installation.ContactPhone = model.ContactPhone;
                        installation.Ville = model.Ville;
                        installation.TypeCient = model.TypeCient;
                        installation.ICCID = model.ICCID;
                        installation.NGSM = model.NGSM;
                        installation.CompteSIP = model.CompteSIP;
                        installation.IdConnexion = model.IdConnexion;
                        installation.Offre = model.Offre;
                        installation.Distributeur = model.Distributeur;

                        installation.GPSLatitude = model.GPSLatitude;
                        installation.GPSLongitude = model.GPSLongitude;
                        installation.Etage = model.Etage;
                        installation.Emplacement = model.Emplacement; /*(FACADE/TOIT)*/
                        installation.Immeublefibre = model.Immeublefibre;/*(OUI/NON)*/
                        installation.TelephoneBluetooth = model.TelephoneBluetooth;/*(OUI/NON)*/
                        installation.IMSI = model.IMSI;
                        installation.Speed1 = model.Speed1;
                        installation.Speed2 = model.Speed2;
                        installation.Speed3 = model.Speed3;

                        installation.eNodeBID = model.eNodeBID;
                        installation.CELLband = model.CELLband;
                        installation.RSRP = model.RSRP;
                        installation.SINR = model.SINR;
                        installation.BandAuto = model.BandAuto;
                        installation.PRB_BH = model.PRB_BH;
                        installation.PRB_NBH = model.PRB_NBH;

                        if(Convert.ToInt16( installation.SINR)>4 && Convert.ToInt16(installation.RSRP) > -105)
                        {
                            installation.SNRNegative = "NON";
                            installation.Installed = "OUI";
                            installation.VoipScreen = UploadFile(model.VoipScreen, model.ClientName) ?? string.Empty;
                            installation.PVPicture = UploadFile(model.PVPicture, model.ClientName) ?? string.Empty;
                            installation.CarteCINRecto = UploadFile(model.CarteCINRecto, model.ClientName) ?? string.Empty;
                            installation.CarteCINVerso = UploadFile(model.CarteCINVerso, model.ClientName) ?? string.Empty;
                            installation.PictureOutside2 = UploadFile(model.PictureOutside2, model.ClientName) ?? string.Empty;

                            installation.CPE = cpe;
                            installation.Telephone = telephone;

                            cpe.Installed = "OUI";
                            telephone.Installed = "OUI";
                            _context.CPES.Update(cpe);
                            _context.Telephones.Update(telephone);
                        }
                        else
                        {
                            installation.SNRNegative = "OUI";
                            installation.Installed = "NON";

                        }

                        installation.Description = model.Description;
                        installation.StatueScreen = UploadFile(model.StatueScreen, model.ClientName);
                        installation.SNRScreen = UploadFile(model.SNRScreen, model.ClientName) ?? string.Empty;
                        installation.SpeedScreen = UploadFile(model.SpeedScreen, model.ClientName) ?? string.Empty;
                        installation.LTEScreen = UploadFile(model.LTEScreen, model.ClientName) ?? string.Empty;

                        installation.PictureInside = UploadFile(model.PictureInside, model.ClientName) ?? string.Empty;
                        installation.PictureOutside = UploadFile(model.PictureOutside, model.ClientName) ?? string.Empty;
                      

                        _installationRepository.Update(model.ID, installation);

                    }
                    return RedirectToAction(nameof(Index));

                }
                catch
                {
                    return View();
                }
               


           
            }

            ModelState.AddModelError("", "You have to fill all the required fields!");
            return View();

        }


        public ActionResult Edit(int id)
        {
            var model = _installationRepository.Find(id);

            var viewModel = new InstallationVM
            {
            ID = id,
            ClientName = model.ClientName,
            ClientAdress = model.ClientAdress,
            ContactName = model.ContactName,
            ContactPhone = model.ContactPhone,
            Ville = model.Ville,
            TypeCient = model.TypeCient,
            ICCID = model.ICCID,
            NGSM = model.NGSM,
            CompteSIP = model.CompteSIP,
            IdConnexion = model.IdConnexion,
            Offre = model.Offre,
            Distributeur = model.Distributeur,

            GPSLatitude = model.GPSLatitude,
            GPSLongitude = model.GPSLongitude,
            Etage = model.Etage,
            Emplacement = model.Emplacement, /*(FACADE/TOIT)*/
            Immeublefibre = model.Immeublefibre,/*(OUI/NON)*/
            TelephoneBluetooth = model.TelephoneBluetooth,/*(OUI/NON)*/
            IMSI = model.IMSI,
            Speed1 = model.Speed1,
            Speed2 = model.Speed2,
            Speed3 = model.Speed3,

            eNodeBID = model.eNodeBID,
            CELLband = model.CELLband,
            RSRP = model.RSRP.Replace(".", ","),
            SINR = model.SINR.Replace(".", ","),
            BandAuto = model.BandAuto,
            PRB_BH = model.PRB_BH,
            PRB_NBH = model.PRB_NBH,
            Description = model.Description,
            StatueScreenURL =model.StatueScreen,
            SNRScreenURL =model.SNRScreen,
            SpeedScreenURL=model.SpeedScreen,
            LTEScreenURL =model.LTEScreen,
            VoipScreenURL=model.VoipScreen,
            PVPictureURL =model.PVPicture,
            CarteCINRectoURL =model.CarteCINRecto,
            CarteCINVersoURL=model.CarteCINVerso,
            PictureInsideURL=model.PictureInside,
            PictureOutsideURL=model.PictureOutside,
            PictureOutside2URL=model.PictureOutside2,

            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InstallationVM model)
        {
            //if (ModelState.IsValid)
            //{
                //try
                //{


                    var installation = _context.Installations.Find(model.ID);
                    if (installation != null)
                    {

                        installation.ClientName = model.ClientName;
                        installation.ClientAdress = model.ClientAdress;
                        installation.ContactName = model.ContactName;
                        installation.ContactPhone = model.ContactPhone;
                        installation.Ville = model.Ville;
                        installation.TypeCient = model.TypeCient;
                        installation.ICCID = model.ICCID;
                        installation.NGSM = model.NGSM;
                        installation.CompteSIP = model.CompteSIP;
                        installation.IdConnexion = model.IdConnexion;
                        installation.Offre = model.Offre;
                        installation.Distributeur = model.Distributeur;

                        installation.GPSLatitude = model.GPSLatitude;
                        installation.GPSLongitude = model.GPSLongitude;
                        installation.Etage = model.Etage;
                        installation.Emplacement = model.Emplacement; /*(FACADE/TOIT)*/
                        installation.Immeublefibre = model.Immeublefibre;/*(OUI/NON)*/
                        installation.TelephoneBluetooth = model.TelephoneBluetooth;/*(OUI/NON)*/
                        installation.IMSI = model.IMSI;
                        installation.Speed1 = model.Speed1;
                        installation.Speed2 = model.Speed2;
                        installation.Speed3 = model.Speed3;

                        installation.eNodeBID = model.eNodeBID;
                        installation.CELLband = model.CELLband;
                        installation.RSRP = model.RSRP.Replace(".", ",");
                        installation.SINR = model.SINR.Replace(".", ",");
                        installation.BandAuto = model.BandAuto;
                        installation.PRB_BH = model.PRB_BH;
                        installation.PRB_NBH = model.PRB_NBH;

                        installation.Description = model.Description;


                if (Convert.ToDouble(installation.SINR) > 4 && Convert.ToDouble(installation.RSRP) > -105)
                {
                            installation.VoipScreen = UploadFileEdit(model.VoipScreen, model.VoipScreenURL);
                            installation.PVPicture = UploadFileEdit(model.PVPicture, model.PVPictureURL) ?? string.Empty;
                            installation.CarteCINRecto = UploadFileEdit(model.CarteCINRecto, model.CarteCINRectoURL) ?? string.Empty;
                            installation.CarteCINVerso = UploadFileEdit(model.CarteCINVerso, model.CarteCINVersoURL) ?? string.Empty;
                            installation.PictureOutside2 = UploadFileEdit(model.PictureOutside2, model.PictureOutside2URL) ?? string.Empty;
                            installation.SNRNegative = "NON";
                            installation.Installed = "OUI";
                        }
                        else
                        {
                            installation.SNRNegative = "OUI";
                            installation.Installed = "NON";
                        }


                        installation.StatueScreen = UploadFileEdit(model.StatueScreen, model.StatueScreenURL);
                        installation.SNRScreen = UploadFileEdit(model.SNRScreen, model.SNRScreenURL) ?? string.Empty;
                        installation.SpeedScreen = UploadFileEdit(model.SpeedScreen, model.SpeedScreenURL) ?? string.Empty;
                        installation.LTEScreen = UploadFileEdit(model.LTEScreen, model.LTEScreenURL) ?? string.Empty;

                        installation.PictureInside = UploadFileEdit(model.PictureInside, model.PictureInsideURL) ?? string.Empty;
                        installation.PictureOutside = UploadFileEdit(model.PictureOutside, model.PictureOutsideURL) ?? string.Empty;
                       
                      
                        _installationRepository.Update(model.ID, installation);

                    };


                    return RedirectToAction(nameof(Index));
                //}
                //catch (Exception)
                //{
                //    return View();
                //}
            //}
            //ModelState.AddModelError("", "You have to fill all the required fields!");
            //return View(model);
        }
        [HttpPost]
        public ActionResult Index(string term)
        {
            var installations = _context.Installations.Include(b => b.CPE).Include(c => c.Telephone).Where(m => m.Installed == "OUI").Where(a => a.ClientName.Contains(term)).ToList();
            return View("Index", installations);

 
        }

        [HttpGet]
        public IActionResult CreateRapport(int id)
        {
            var installation = _context.Installations.Include(a => a.CPE).FirstOrDefault(a => a.ID == id);

            WordDocument document = new WordDocument();


            IWSection section = document.AddSection();
            IWTextRange textRange = section.AddParagraph().AppendText("                         Rapport TDLTE");
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.FontSize = 20;
            textRange.CharacterFormat.Bold = true;
            section.AddParagraph();


            //Adds a new table into Word document
            IWTable table = section.AddTable();
            //Specifies the total number of rows & columns
            table.ResetCells(6, 2);
            //CLIENT HEADER
            textRange = table[0, 0].AddParagraph().AppendText("Nom client:");
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.FontSize = 12;
            //Coordonnées GPS  HEADER
            textRange = table[1, 0].AddParagraph().AppendText("Coordonnées GPS:");
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.FontSize = 12;
            //Adresse
            textRange = table[2, 0].AddParagraph().AppendText("Adrèsse:");
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.FontSize = 12;


            // Contact Nom
            textRange = table[3, 0].AddParagraph().AppendText("Nom du contact:");
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.FontSize = 12;
         

            //Contact Téléphone
            textRange = table[4, 0].AddParagraph().AppendText("Téléphone:");
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.FontSize = 12;

            // Date d’installation
            textRange = table[5, 0].AddParagraph().AppendText("Date d’installation:");
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.FontSize = 12;


            //CLIENT HEADER
            textRange = table[0, 1].AddParagraph().AppendText(installation.ClientName);
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.FontSize = 10;
            //Coordonnées GPS  HEADER
            textRange = table[1, 1].AddParagraph().AppendText(installation.GPSLatitude + "," + installation.GPSLongitude);
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.FontSize = 10;
            //Adresse
            textRange = table[2, 1].AddParagraph().AppendText(installation.ClientAdress);
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.FontSize = 10;


            // Contact Nom
            textRange = table[3, 1].AddParagraph().AppendText(installation.ContactName);
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.FontSize = 10;

            //Contact Téléphone
            textRange = table[4, 1].AddParagraph().AppendText(installation.ContactPhone);
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.FontSize = 10;

            // Date d’installation
            textRange = table[5, 1].AddParagraph().AppendText(installation.DATEIntervention.ToString());
            textRange.CharacterFormat.FontName = "Arial";
            textRange.CharacterFormat.FontSize = 10;

            IWParagraph paragraph;
            //Appends paragraph.
            paragraph = section.AddParagraph();
         
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("\r\rIMEI :" + installation.CPE.IMEI.ToString() +"\r") as WTextRange;
           
            textRange = paragraph.AppendText("IMSI :" + installation.IMSI + "\r") as WTextRange;
            textRange = paragraph.AppendText("BANDEAUTO :" + installation.BandAuto + "\r \r \r") as WTextRange;
       

            //Image du routeur
            string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\uploads\"}";

            FileStream imageStream = new FileStream(path + installation.PictureInside, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWPicture picture = paragraph.AppendPicture(imageStream);
            picture.WidthScale = 55;
            picture.HeightScale = 55;
            paragraph.AppendText("\r \r \r");

            //Image d'antenne
            FileStream imageStream1 = new FileStream(path + installation.PictureOutside, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);
            IWPicture picture1 = paragraph.AppendPicture(imageStream1);
            picture1.WidthScale = 55;
            picture1.HeightScale = 55;
            paragraph.AppendText("\r \r \r");
            if (installation.SNRNegative != "OUI")
            {
                FileStream imageStream11 = new FileStream(path + installation.PictureOutside2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                IWPicture picture11 = paragraph.AppendPicture(imageStream11);
                picture1.WidthScale = 55;
                picture1.HeightScale = 55;
            }

      

            paragraph.AppendText("\r \r \r");
            //STATUE SCREEN
            FileStream imageStream2 = new FileStream(path + installation.StatueScreen, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);
            IWPicture picture2 = paragraph.AppendPicture(imageStream2);
            picture2.WidthScale = 55;
            picture2.HeightScale = 55;
            paragraph.AppendText("\r \r \r");
            // SNR SCREEN
            FileStream imageStream3 = new FileStream(path + installation.SNRScreen, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);
            IWPicture picture3 = paragraph.AppendPicture(imageStream3);
            picture3.WidthScale = 55;
            picture3.HeightScale = 55;
            paragraph.AppendText("\r \r \r");
            //SPEED SCREEN
            FileStream imageStream4 = new FileStream(path + installation.SpeedScreen, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);
            IWPicture picture4 = paragraph.AppendPicture(imageStream4);
            picture4.WidthScale = 55;
            picture4.HeightScale = 55;
            paragraph.AppendText("\r \r \r");
            //LTE SCREEN
            FileStream imageStream5 = new FileStream(path + installation.LTEScreen, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);
            IWPicture picture5 = paragraph.AppendPicture(imageStream5);
            picture5.WidthScale = 55;
            picture5.HeightScale = 55;
            paragraph.AppendText("\r \r \r");

            if (installation.SNRNegative != "OUI")
            {
                //VOIP SCREEN
                FileStream imageStream6 = new FileStream(path + installation.VoipScreen, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                IWPicture picture6 = paragraph.AppendPicture(imageStream6);

                picture6.WidthScale = 55;
                picture6.HeightScale = 55;
                paragraph.AppendText("\r \r \r");
                //PV SCREEN
                FileStream imageStream7 = new FileStream(path + installation.PVPicture, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                IWPicture picture7 = paragraph.AppendPicture(imageStream7);

                picture7.WidthScale = 55;
                picture7.HeightScale = 55;
            }

            paragraph.AppendText("\r \r \r");
            //Saves the Word document to  MemoryStream
            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);
            stream.Position = 0;


            var fileName = "Rapport TDLTE " + installation.ClientName + ".docx";

            //Download Word document in the browser
            return File(stream, "application/msword", fileName);
        }
       
        [HttpGet]
        public IActionResult CreateMatriceView()
        {
            MatriceVM matrice = new MatriceVM();
            matrice.date1= DateTime.Now.Date.AddDays(-7);
            matrice.date2= DateTime.Now.Date;
            return View(matrice);
        }

        [HttpPost]
        public IActionResult CreateMatrice(MatriceVM matrice)
        {

            string week = "week 1";
            string reportFullName = "\\Matrice " + week + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var Listinstallation = _context.Installations.Where(s=>s.DATEIntervention.Value >= matrice.date1 && s.DATEIntervention.Value <= matrice.date2).Select(s=>new
            {
               s.ClientName,s.Installed,s.CPE.SNAntenne,s.NGSM,
                s.GPSLatitude,s.GPSLongitude,s.DATEIntervention, s.Ville,
                s.TypeCient,
                s.Etage,
                s.Emplacement,
                s.Immeublefibre,
                s.TelephoneBluetooth,
                s.Offre,
                s.eNodeBID,
                s.CELLband,
                s.RSRP,
                s.SINR,
                s.PRB_BH,
                s.PRB_NBH,
                s.Speed1,
                s.Speed2,
                s.Speed3,
                s.BandAuto,

            });
          
            
            using (ExcelPackage xlPackage = new ExcelPackage())
            {
                xlPackage.Workbook.Worksheets.Add("Sheet 1").Cells[3, 3].LoadFromCollection(Listinstallation, true);

                var sheet = xlPackage.Workbook.Worksheets.First();
                sheet.Rows.Style.Border.BorderAround(ExcelBorderStyle.Thin) ;
                xlPackage.SaveAs(new FileInfo(reportFullName));

            }
            return DownloadMatrice(reportFullName);
        }





        List<CPE> FillSelectListCPE()
        {
            var cpes = _context.CPES.Where(m => m.Installed == "NON").ToList();
            cpes.Insert(0, new CPE { ID = -1, SNAntenne = "--- Please select a CPE ---" });

            return cpes;
        }

        List<Telephone> FillSelectListTelephone()
        {
            var telephones = _context.Telephones.Where(m => m.Installed == "NON").ToList();
            telephones.Insert(0, new Telephone { ID = -1, SN = "--- Please select a PHONE ---" });
            return telephones;
        }


        string UploadFile(IFormFile file, string ClientName)
        {
            
            if (file != null)
            {

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string fullPath = Path.Combine(uploads, uniqueFileName);
                if (!System.IO.File.Exists(fullPath))
                {
                    file.CopyTo(new FileStream(fullPath, FileMode.Create));

                    return uniqueFileName;
                }
            }
               

            return null;
        }

        string UploadFileEdit(IFormFile file, string imageUrl)
        {
            string uploads = Path.Combine(hosting.WebRootPath, "uploads");
            string oldPath = Path.Combine(uploads, imageUrl);
            if (file != null)
            {
          

              

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string newPath = Path.Combine(uploads, uniqueFileName);

                if (oldPath != newPath)
                {

                    System.IO.File.Delete(oldPath);
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                    return uniqueFileName;
                }

              
            }

            return oldPath;
        }

   



     public FileContentResult DownloadMatrice(string FullFilePath) {
            try {
                var data = System.IO.File.ReadAllBytes(FullFilePath); 
                var result = new FileContentResult(data, "application/octet-stream") 
                { 
                    FileDownloadName = FullFilePath
                };
                return result;
            } 
            catch (Exception ex) 
            { 
                return null;
            } 
        }
     public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }

        [HttpGet]
        public IActionResult BackUp()
        {
            ado _d = new ado();
                try
                {
                    string sourceDir = @"C:\Users\HP\Desktop\backupDB";
                string[] backups = Directory.GetFiles(sourceDir, "*.Bak");
                foreach (var fi in new DirectoryInfo(sourceDir).GetFiles().OrderByDescending(x => x.LastWriteTime).Skip(3))
                {
                    fi.OpenWrite();
                    fi.Delete();
                }
                    
                
                    
 

                    _d.connecter_bd();
                    string saveFileName = "db" + DateTime.Now.ToShortDateString().Replace('/', '-') + " time" + DateTime.Now.ToLongTimeString().Replace(':', '-');
                    _d.cmd = new SqlCommand(@"backup database hub_occary to disk='"+sourceDir+"\\" + saveFileName + ".Bak'", _d.cnx);
                    _d.cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
        
                }

              return RedirectToAction(nameof(Index),"Home","");

        }

    }
}
