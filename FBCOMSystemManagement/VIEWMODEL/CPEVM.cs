using FBCOMSystemManagement.Models;
using System;

namespace FBCOMSystemManagement.VIEWMODEL
{
    public class CPEVM
    {
        public List<CPE> ListCPE { get; set; }
        public Pager pager { get; set; }
    }
}
