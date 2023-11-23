﻿using EAMS_ACore;
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


        #region State Master

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
        public async Task<StateMaster> UpdateStateById(StateMaster stateMaster1)
        {
            var stateMaster = _context.StateMaster.Where(d => d.StateMasterId == stateMaster1.StateMasterId).FirstOrDefault();
            stateMaster.StateName = stateMaster1.StateName;


            _context.StateMaster.Update(stateMaster);
            _context.SaveChanges();
            return stateMaster;
        }

        public string AddState(StateMaster stateMaster)
        {
            try
           {

                var stateExist = _context.StateMaster
    .Where(p => p.StateCode == stateMaster.StateCode || p.StateName == stateMaster.StateName).FirstOrDefault();


                if (stateExist == null)
                {
                    _context.StateMaster.Add(stateMaster);
                    _context.SaveChanges();
                    return "State " + stateExist.StateName + " added successfully!";
                }
                else
                {
                    return "State " + stateExist.StateName + " with the same name already exists.";
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, logging or other actions.
                return "An error occurred while processing the request.";
            }
        }
        #endregion

        #region District Master
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
        public async Task<DistrictMaster> UpdateDistrictById(DistrictMaster districtMaster1)
        {
            var districtMaster = _context.DistrictMaster.Where(d => d.DistrictMasterId == districtMaster1.DistrictMasterId).FirstOrDefault();
            districtMaster.DistrictName = districtMaster1.DistrictName;
            _context.DistrictMaster.Update(districtMaster);
            _context.SaveChanges();
            return districtMaster;
        }

        public string AddDistrict(DistrictMaster districtMaster)
        {
            try
            {
                var districtExist = _context.DistrictMaster.Where(p => p.DistrictCode == districtMaster.DistrictCode || p.DistrictName == districtMaster.DistrictName).FirstOrDefault();

                if (districtExist == null)
                {
                    _context.DistrictMaster.Add(districtMaster);
                    _context.SaveChanges();
                    return "District" + districtMaster.DistrictName + "Added Successfully !";
                }
                else
                {
                    return "District" + districtMaster.DistrictName + "Same District Already Exists !";
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately, logging or other actions.
                return "An error occurred while processing the request.";
            }
        }

        #endregion

        #region Assembly Master
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

        public async Task<AssemblyMaster> UpdateAssembliesById(AssemblyMaster assemblyMaster)
        {
            var assembliesMasterRecord = _context.AssemblyMaster.Where(d => d.AssemblyMasterId == assemblyMaster.AssemblyMasterId).FirstOrDefault();
            //assembliesMaster.AssemblyMasterId=assemblyMaster.AssemblyMasterId;
            assembliesMasterRecord.AssemblyName = assemblyMaster.AssemblyName;
            assembliesMasterRecord.AssemblyCode = assemblyMaster.AssemblyCode;
            assembliesMasterRecord.AssemblyType = assemblyMaster.AssemblyType;
            assembliesMasterRecord.AssemblyStatus = assemblyMaster.AssemblyStatus;

            var ss = _context.AssemblyMaster.Update(assembliesMasterRecord);
            _context.SaveChanges();
            return assembliesMasterRecord;
        }

    

        #endregion

        #region SO Master
        public async Task<List<CombinedMaster>> GetSectorOfficersListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {

            var solist = from so in _context.SectorOfficerMaster.Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId)) // outer sequence
                         join asem in _context.AssemblyMaster
                         on so.SoAssemblyCode equals asem.AssemblyCode
                         join dist in _context.DistrictMaster
                         on asem.DistrictMasterId equals dist.DistrictMasterId
                         where asem.DistrictMasterId == Convert.ToInt32(districtMasterId) && asem.AssemblyMasterId == Convert.ToInt32(assemblyMasterId) // key selector
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
                             soMobile = so.SoMobile

                         };

            return await solist.ToListAsync();
        }
        #endregion

        #region Booth Master
        public async Task<List<CombinedMaster>> GetBoothListById(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {

            var boothlist = from bt in _context.BoothMaster.Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId) && d.DistrictMasterId == Convert.ToInt32(districtMasterId) && d.AssemblyMasterId == Convert.ToInt32(assemblyMasterId)) // outer sequenc)
                            join asem in _context.AssemblyMaster
                            on bt.AssemblyMasterId equals asem.AssemblyMasterId
                            join dist in _context.DistrictMaster
                            on asem.DistrictMasterId equals dist.DistrictMasterId
                            join state in _context.StateMaster
                             on dist.StateMasterId equals state.StateMasterId

                            select new CombinedMaster
                            {
                                StateName = state.StateName,
                                DistrictId = dist.DistrictMasterId,
                                DistrictName = dist.DistrictName,
                                DistrictCode = dist.DistrictCode,
                                AssemblyId = asem.AssemblyMasterId,
                                AssemblyName = asem.AssemblyName,
                                AssemblyCode = asem.AssemblyCode,
                                BoothMasterId = bt.BoothMasterId,
                                BoothName = bt.BoothName,
                                BoothAuxy = bt.BoothNoAuxy

                            };
            var count = boothlist.Count();
            return await boothlist.ToListAsync();
        }

        public async Task<List<CombinedMaster>> GetBoothListByAssemblyId(string stateMasterId, string districtMasterId, string assemblyMasterId)
        {

            var boothlist = from bt in _context.BoothMaster.Where(d => d.StateMasterId == Convert.ToInt32(stateMasterId) && d.DistrictMasterId == Convert.ToInt32(districtMasterId) && d.AssemblyMasterId == Convert.ToInt32(assemblyMasterId)) // outer sequenc)
                            join asem in _context.AssemblyMaster
                            on bt.AssemblyMasterId equals asem.AssemblyMasterId
                            join dist in _context.DistrictMaster
                            on asem.DistrictMasterId equals dist.DistrictMasterId
                            join state in _context.StateMaster
                             on dist.StateMasterId equals state.StateMasterId

                            select new CombinedMaster
                            {
                                StateName = state.StateName,
                                DistrictId = dist.DistrictMasterId,
                                DistrictName = dist.DistrictName,
                                DistrictCode = dist.DistrictCode,
                                AssemblyId = asem.AssemblyMasterId,
                                AssemblyName = asem.AssemblyName,
                                AssemblyCode = asem.AssemblyCode,
                                BoothMasterId = bt.BoothMasterId,
                                BoothName = bt.BoothName,
                                BoothAuxy = bt.BoothNoAuxy

                            };
            var count = boothlist.Count();
            return await boothlist.ToListAsync();
        }

        public string AddBooth(BoothMaster boothMaster)
        {
            try
            {
                // Check for uniqueness
                var boothExist = _context.BoothMaster
                    .FirstOrDefault(p => p.BoothCode_No == boothMaster.BoothCode_No || p.BoothName == boothMaster.BoothName);

                if (boothExist == null)
                {
                    // Set UTC time directly in the model
                    boothMaster.BoothCreatedAt = DateTime.UtcNow;
                    boothMaster.BoothUpdatedAt = DateTime.UtcNow;
                    boothMaster.BoothDeletedAt = DateTime.UtcNow;

                    _context.BoothMaster.Add(boothMaster);
                    _context.SaveChanges();

                    return "Booth " + boothMaster.BoothName + " added successfully!";
                }
                else
                {
                    return "Booth " + boothMaster.BoothName + " with the same name or code already exists.";
                }
            }
            catch (Exception ex)
            {
                // Log the exception details for troubleshooting
                Console.WriteLine(ex.Message);
                return "An error occurred while processing the request.";
            }
        }

        #endregion

        #region Event Master

        public async Task<List<EventMaster>> GetEventListById(string eventMasterId) 
        {
            var eventData = await _context.EventMaster.Where(d => d.EventMasterId == Convert.ToInt32(eventMasterId)).Select(d => new EventMaster

            {
                EventMasterId = d.EventMasterId,
                EventName = d.EventName,
                EventSequence = d.EventSequence,
                Status = d.Status
            })

            .ToListAsync();
            return eventData;
      
        }

        #endregion
    }
}
