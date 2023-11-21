using EAMS_ACore;
using EAMS_ACore.HelperModels;
using EAMS_ACore.IRepository;
using EAMS_DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_DAL.Repository
{
    public class EamsRepository : IEamsRepository
    {
        private readonly EamsContext _context;
        public EamsRepository(EamsContext context)
        {
            _context = context;
        }
        #region
        //StateMethods

        #endregion

        public async Task<List<StateMaster>> GetState()
        {
            var stateList = await _context.StateMaster
                .Include(d => d.DistrictMasters)
                .Select(d => new StateMaster
                {
                    StateCode = d.StateCode,
                    StateName = d.StateName,
                    StateMasterId = d.StateMasterId,
                    StateStatus = d.StateStatus
                })
                .ToListAsync();

            return stateList;
        }

        public async Task<List<CombinedMaster>> GetDistrictById(string stateMasterId)
        {


            var stateData = await _context.DistrictMaster
                .Include(d => d.StateMaster)
                .Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId)).Select(d => new CombinedMaster
                {
                    StateId = d.StateMaster.StateMasterId,
                    StateName = d.StateMaster.StateName,
                    DistrictId = d.DistrictMasterId,
                    DistrictName = d.DistrictName,
                    DistrictStatus = d.DistrictStatus,
                    DistrictCode = d.DistrictCode

                })

                .ToListAsync();

            return stateData;
        }


        public async Task<List<CombinedMaster>> GetAssemblies(string stateId, string districtId)
        {
            var innerJoin = from asemb in _context.AssemblyMaster.Where(d => d.DistrictMasterId == Convert.ToInt32(districtId)) // outer sequence
                            join dist in _context.DistrictMaster // inner sequence 
                            on asemb.DistrictMasterId equals dist.DistrictMasterId // key selector
                            join state in _context.StateMaster // additional join for StateMaster
                            on dist.StateMasterId equals state.StateMasterId // key selector for StateMaster
                            where state.StateMasterId == Convert.ToInt32(stateId) // condition for StateMasterId equal to 21
                            select new CombinedMaster
                            { // result selector 
                                StateName = state.StateName,
                                DistrictId = dist.DistrictMasterId,
                                DistrictName = dist.DistrictName,
                                DistrictCode = dist.DistrictCode,
                                AssemblyId = asemb.AssemblyMasterId,
                                AssemblyName = asemb.AssemblyName,
                                AssemblyCode = asemb.AssemblyCode
                            };

            return await innerJoin.ToListAsync();
        }
        public async Task<List<CombinedMaster>> GetSectorOfficersListById(string stateMasterId)
        {

            var solist = from so in _context.SectorOfficerMaster.Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId)) // outer sequence
                         join asem in _context.AssemblyMaster
                         on so.SoAssemblyCode equals asem.AssemblyCode
                         join dist in _context.DistrictMaster
                         on asem.DistrictMasterId equals dist.DistrictMasterId // key selector
                         join state in _context.StateMaster
                          on dist.StateMasterId equals state.StateMasterId

                         select new CombinedMaster
                         { // result selector 
                             StateName = state.StateName,
                             DistrictId = dist.DistrictMasterId,
                             DistrictName = dist.DistrictName,
                             DistrictCode = dist.DistrictCode,
                             AssemblyId = asem.AssemblyMasterId,
                             AssemblyName = asem.AssemblyName,
                             AssemblyCode = asem.AssemblyCode,
                             soName = so.SoName,
                             soMobile=so.SoMobile

                         };

            return await solist.ToListAsync();
        }


        public async Task<List<CombinedMaster>> GetBoothListById(string stateMasterId,string districtMasterId, string assemblyMasterId)
        {

            var boothlist = from bt in _context.BoothMaster.Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId) && d.DistrictMasterId== Convert.ToInt32(districtMasterId) && d.AssemblyMasterId== Convert.ToInt32(assemblyMasterId)) // outer sequenc)
                                                                                                                               //            var solist = from so in _context.SectorOfficerMaster.Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId)) // outer sequence
                         join asem in _context.AssemblyMaster
                         on bt.AssemblyMasterId equals asem.AssemblyMasterId
                         join dist in _context.DistrictMaster
                         on asem.DistrictMasterId equals dist.DistrictMasterId // key selector
                         join state in _context.StateMaster
                          on dist.StateMasterId equals state.StateMasterId

                         select new CombinedMaster
                         { // result selector 
                             StateName = state.StateName,
                             DistrictId = dist.DistrictMasterId,
                             DistrictName = dist.DistrictName,
                             DistrictCode = dist.DistrictCode,
                             AssemblyId = asem.AssemblyMasterId,
                             AssemblyName = asem.AssemblyName,
                             AssemblyCode = asem.AssemblyCode,
                                BoothMasterId=bt.Id,
                            BoothName=bt.BoothName,
                            BoothAuxy=bt.BoothNoAuxy

                         };
            var count = boothlist.Count();
            return await boothlist.ToListAsync();
        }
        public async Task<StateMaster> UpdateStateById(StateMaster stateMaster1)
        {
            var stateMaster = _context.StateMaster.Where(d => d.StateMasterId == stateMaster1.StateMasterId).FirstOrDefault();
            stateMaster.StateName = stateMaster1.StateName;


            _context.StateMaster.Update(stateMaster);
            _context.SaveChanges();
            return stateMaster;
        }
        public async Task<DistrictMaster> UpdateDistrictById(DistrictMaster districtMaster1)
        {
            var districtMaster = _context.DistrictMaster.Where(d => d.DistrictMasterId == districtMaster1.DistrictMasterId).FirstOrDefault();
            districtMaster.DistrictName = districtMaster1.DistrictName;


            _context.DistrictMaster.Update(districtMaster);
            _context.SaveChanges();
            return districtMaster;
        }
    }
}
