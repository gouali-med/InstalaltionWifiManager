using FBCOMSystemManagement.DATA;
using FBCOMSystemManagement.Models;
using FBCOMSystemManagement.VIEWMODEL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WareHouse.Models.Reposotories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace FBCOMSystemManagement.Controllers
{
    public class PlanningController : Controller
    {
        private readonly IHostingEnvironment hosting;
        private readonly IFBCOMDBContext<Installation> _installationRepository;
        private readonly DBContextFBCOM _context;

        public PlanningController(DBContextFBCOM context, IHostingEnvironment hosting, IFBCOMDBContext<Installation> installationRepository)
        {
            this.hosting = hosting;
            _installationRepository = installationRepository;
            _context = context;
        }
        [HttpGet]
        public ActionResult IndexPreinstallation()
        {
            var installations = _installationRepository.List().Where(m => m.Installed == "NON").ToList();
            return View(installations);
        }
        [HttpPost]
        public ActionResult IndexPreinstallation(string term)
        {
            var installations = _context.Installations.Include(b => b.CPE).Include(c => c.Telephone).Where(m => m.Installed == "NON").Where(a => a.ClientName.Contains(term)).ToList();
            return View("IndexPreinstallation", installations);


        }
        [HttpGet]
        public ActionResult DetailsPreinstallation(int id)
        {
            var installation = _installationRepository.Find(id);

            return View(installation);
        }
        [HttpGet]
        public ActionResult CreatePreinstallation()
        {

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createPreinstallation(PreInstallationVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                 

                    var installation = new Installation
                    {
                        ID = model.ID,
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
                        Description = model.Description,
                        Installed = "NON",

                    };

                    _installationRepository.Add(installation);

                    return RedirectToAction(nameof(IndexPreinstallation));
                }
                catch
                {
                    return View();
                }
            }


            ModelState.AddModelError("", "You have to fill all the required fields!");
            return RedirectToAction(nameof(createPreinstallation));
        }

        [HttpGet]
        public ActionResult EditPreinstallation(int id)
        {
            var preInstallation = _installationRepository.Find(id);

            var viewModel = new Installation
            {
                ID = preInstallation.ID,
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
                Description = preInstallation.Description,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPreinstallation(PreInstallationVM viewModel)
        {
            try
            {


                var installation = _context.Installations.Find(viewModel.ID);
                if (installation != null)
                {
                    installation.ID = viewModel.ID;
                    installation.ClientName = viewModel.ClientName;
                    installation.ClientAdress = viewModel.ClientAdress;
                    installation.ContactName = viewModel.ContactName;
                    installation.ContactPhone = viewModel.ContactPhone;
                    installation.Ville = viewModel.Ville;
                    installation.TypeCient = viewModel.TypeCient;
                    installation.ICCID = viewModel.ICCID;
                    installation.NGSM = viewModel.NGSM;
                    installation.CompteSIP = viewModel.CompteSIP;
                    installation.IdConnexion = viewModel.IdConnexion;
                    installation.Offre = viewModel.Offre;
                    installation.Distributeur = viewModel.Distributeur;
                    installation.Description = viewModel.Description;
                };

                _installationRepository.Update(viewModel.ID, installation);

                return RedirectToAction(nameof(IndexPreinstallation));
            }
            catch (Exception)
            {
                return View();
            }
        }




        [HttpGet]
        public ActionResult DeletePreinstallation(int id)
        {
            var preInstallation = _installationRepository.Find(id);

            var viewModel = new Installation
            {
                ID = preInstallation.ID,
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
                Description = preInstallation.Description,
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("DeletePreinstallation")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmeDeletePreinstallation(int id)
        {
            try
            {


                var installation = _context.Installations.Find(id);
                if (installation != null)
                {
                    _installationRepository.Delete(id);
                };

               

                return RedirectToAction(nameof(IndexPreinstallation));
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
