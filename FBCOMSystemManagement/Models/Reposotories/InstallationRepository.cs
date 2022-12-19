using FBCOMSystemManagement.DATA;
using FBCOMSystemManagement.Models;
using Syncfusion.DocIO.DLS;

namespace WareHouse.Models.Reposotories
{
    public class InstallationRepository: IFBCOMDBContext<Installation>
    {

        DBContextFBCOM db;

        public InstallationRepository(DBContextFBCOM _db)
        {
            db = _db;
        }
        public void Add(Installation entity)
        {
           
            db.Installations.Add(entity);
            db.SaveChanges();
        }
        public void Update(int id, Installation newInstallation)
        {
            newInstallation.DATEIntervention = DateTime.Now;
            db.Update(newInstallation);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var installation = Find(id);

            db.Installations.Remove(installation);
            db.SaveChanges();
        }

        public Installation Find(int id)
        {
            var installation = db.Installations.SingleOrDefault(a => a.ID == id);

            return installation;
        }

        public IList<Installation> List()
        {
            return db.Installations.ToList();
        }

        public List<Installation> Search(string term)
        {
            return db.Installations.Where(a => a.ClientName.Contains(term)).ToList();
        }



    }
}
